using System.Collections;
using UnityEngine;

public class AppleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject applePrefab;
    [SerializeField] private GameObject rottenApplePrefab;
    [SerializeField] private GameObject branchPrefab;

    private float spawnInterval = 2f; // Base spawn time for apples
    private float rottenAppleChance = 1.5f; 
    private float branchSpawnChance = 1.0f; 
    
    private float screenMinX, screenMaxX; // Keeps track of screen boundaries

    private void Start()
    {
        CalculateScreenBounds(); // Dynamically calculate screen width

        // Regular apples spawn at a fixed interval
        InvokeRepeating(nameof(SpawnApple), spawnInterval, spawnInterval);

        // Start coroutines for rotten apples and branches
        StartCoroutine(SpawnRottenAppleCoroutine());
        StartCoroutine(SpawnBranchCoroutine());
    }

    private void CalculateScreenBounds()
    {
        if (Camera.main != null)
        {
            float halfScreenWidth = Camera.main.orthographicSize * Screen.width / Screen.height;
            screenMinX = -halfScreenWidth + 1.0f; // Adding a small margin
            screenMaxX = halfScreenWidth - 1.0f;
        }
        else
        {
            Debug.LogError("Main Camera not found! Ensure there is a camera in the scene.");
        }
    }

    private void SpawnApple()
    {
        float halfScreenWidth = Camera.main.orthographicSize * Screen.width / Screen.height;
        float spawnOffset = Random.Range(-3.0f, 3.0f);
        float spawnX = transform.position.x + spawnOffset;

        spawnX = Mathf.Clamp(spawnX, -halfScreenWidth + 0.5f, halfScreenWidth - 0.5f); 
        Vector3 spawnPosition = new Vector3(spawnX, transform.position.y, transform.position.z);
        Instantiate(applePrefab, spawnPosition, Quaternion.identity);
    }

    private IEnumerator SpawnRottenAppleCoroutine()
    {
        while (true)
        {
            float waitTime = Random.Range(spawnInterval * 1.5f, spawnInterval * 3f);
            yield return new WaitForSeconds(waitTime);

            // 30% chance to spawn a rotten apple
            if (Random.value < rottenAppleChance)
            {
                float spawnOffset = Random.Range(-1.0f, 1.0f);
                float spawnX = Random.Range(screenMinX, screenMaxX);
                Vector3 spawnPosition = new Vector3(spawnX, transform.position.y, transform.position.z);
                Instantiate(rottenApplePrefab, spawnPosition, Quaternion.identity);
            }
        }
    }

    private IEnumerator SpawnBranchCoroutine()
    {
        while (true)
        {
            float waitTime = Random.Range(spawnInterval * 4f, spawnInterval * 6f); // More rare
            yield return new WaitForSeconds(waitTime);

            // 10% chance to spawn a branch
            if (Random.value < branchSpawnChance)
            {
                float spawnOffset = Random.Range(-1.0f, 1.0f);
                float spawnX = Random.Range(screenMinX, screenMaxX);
                Vector3 spawnPosition = new Vector3(spawnX, transform.position.y, transform.position.z);
                Instantiate(branchPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }
}
