using UnityEngine;

public class appleSpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject applePrefab;
    public GameObject branchPrefab;
    public float spawnInterval = 2f;
    public float spawnRangeX = 3f;
    
    
    void Start()
    {
        InvokeRepeating(nameof(SpawnApple), spawnInterval, spawnInterval);
        InvokeRepeating(nameof(SpawnBranch), spawnInterval, spawnInterval);
    }

    private void SpawnApple()
    {
        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        Vector3 spawnPosition = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z);
        GameObject apple = Instantiate(applePrefab, spawnPosition, Quaternion.identity);
        apple.transform.SetParent(transform);
    }

    private void SpawnBranch()
    {
        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        Vector3 spawnPosition = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z);
        GameObject branch = Instantiate(branchPrefab, spawnPosition, Quaternion.identity);
        branch.transform.SetParent(transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
