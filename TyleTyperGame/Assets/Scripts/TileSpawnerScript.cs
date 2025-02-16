// TileSpawnerScipt.cs
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    public GameObject tilePrefab;
    [SerializeField] private float spawnRangeX = 9.5f;
    [SerializeField] private float initialSpawnInterval = 2f;
    [SerializeField] private float minSpawnInterval = 0.5f;
    [SerializeField] private float spawnIntervalAcceleration = 0.05f;

    private void Start()
    {
        Invoke(nameof(SpawnTile), initialSpawnInterval);
    }

    private void SpawnTile()
    {
        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        Vector2 spawnPosition = new Vector2(randomX, transform.position.y);
        Instantiate(tilePrefab, spawnPosition, Quaternion.identity);

        float elapsedTime = GameManager.Instance.GetElapsedTime();
        float currentSpawnInterval = Mathf.Max(minSpawnInterval, initialSpawnInterval - (elapsedTime * spawnIntervalAcceleration));
        Invoke(nameof(SpawnTile), currentSpawnInterval);
    }

    // This is just to show the spawner width on scene view. This does not actually do anything and
    // is not seen on the game view
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(
            new Vector3(transform.position.x - spawnRangeX, transform.position.y, 0),
            new Vector3(transform.position.x + spawnRangeX, transform.position.y, 0)
        );
        Gizmos.DrawWireSphere(new Vector3(transform.position.x - spawnRangeX, transform.position.y, 0), 0.2f);
        Gizmos.DrawWireSphere(new Vector3(transform.position.x + spawnRangeX, transform.position.y, 0), 0.2f);
    }
}