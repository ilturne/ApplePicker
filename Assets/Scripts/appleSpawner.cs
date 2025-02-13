using System.Collections;
using UnityEngine;

public class AppleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject applePrefab;
    [SerializeField] private GameObject rottenApplePrefab;  // renamed from "badApplePrefab" for clarity
    [SerializeField] private GameObject branchPrefab;
    private float spawnInterval = 2f;
    private float spawnRangeX = 3f;

    private void Start()
    {
        // Regular apples spawn repeatedly at a fixed interval.
        InvokeRepeating(nameof(SpawnApple), spawnInterval, spawnInterval);
        // Rotten apples and branches spawn at random intervals.
        StartCoroutine(SpawnRottenAppleCoroutine());
        StartCoroutine(SpawnBranchCoroutine());
    }

    private void SpawnApple()
    {
        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        Vector3 spawnPosition = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z);
        GameObject apple = Instantiate(applePrefab, spawnPosition, Quaternion.identity);
        apple.transform.SetParent(transform);
    }

    private IEnumerator SpawnRottenAppleCoroutine()
    {
        while (true)
        {
            float waitTime = Random.Range(spawnInterval * 1.5f, spawnInterval * 3f);
            yield return new WaitForSeconds(waitTime);

            float randomX = Random.Range(-spawnRangeX, spawnRangeX);
            Vector3 spawnPosition = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z);
            GameObject rottenApple = Instantiate(rottenApplePrefab, spawnPosition, Quaternion.identity);
            rottenApple.transform.SetParent(transform);
        }
    }

    private IEnumerator SpawnBranchCoroutine()
    {
        while (true)
        {
            float waitTime = Random.Range(spawnInterval * 1.5f, spawnInterval * 3f);
            yield return new WaitForSeconds(waitTime);

            float randomX = Random.Range(-spawnRangeX, spawnRangeX);
            Vector3 spawnPosition = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z);
            GameObject branch = Instantiate(branchPrefab, spawnPosition, Quaternion.identity);
            branch.transform.SetParent(transform);
        }
    }
}
