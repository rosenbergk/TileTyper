// TileSpawnerScipt.cs
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    public static float initialSpawnInterval = 5;
    public static float currentSpawnInterval;

    [SerializeField] private GameObject[] tilePrefabs;
    [SerializeField] private float spawnRangeX;
    [SerializeField] private float minSpawnInterval;
    [SerializeField] private float spawnIntervalAcceleration;
    

    private void Start()
    {
        currentSpawnInterval = initialSpawnInterval;
        Invoke(nameof(SpawnTile), initialSpawnInterval);
    }

    private void SpawnTile()
    {
        GameObject randomTilePrefab = GetRandomTile();
        string tileName = randomTilePrefab.name;

        TileScript tileScript = TilePool.Instance.GetTile(tileName);

        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        Vector2 spawnPosition = new Vector2(randomX, transform.position.y);
        tileScript.transform.position = spawnPosition;

        tileScript.SetTileWord(tileName.Replace("Tile", ""));

        float elapsedTime = GameManager.Instance.GetElapsedTime();
        currentSpawnInterval = Mathf.Max(minSpawnInterval, initialSpawnInterval - (elapsedTime * spawnIntervalAcceleration));
        Invoke(nameof(SpawnTile), currentSpawnInterval);
    }

    private GameObject GetRandomTile()
    {
        int randomIndex = Random.Range(0, tilePrefabs.Length);
        return tilePrefabs[randomIndex];
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