// TileSpawnerScipt.cs
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    public static float initialSpawnInterval = 5;
    public static float currentSpawnInterval;

    [SerializeField] private GameObject[] tilePrefabs;
    [SerializeField] private float spawnRangeX;
    [SerializeField] private float minSpawnInterval;
    [SerializeField] private float decayRate = 0.02f;
    [SerializeField] private float fluctationMagnitude = 0.3f;
    [SerializeField] private float noiseSpeed = 0.1f;

    private float noiseOffset;
    private float hordeTimer = 0f;
    private bool inHordePhase = false;
    private float hordeDuration = 5f;

    private void Start()
    {
        currentSpawnInterval = initialSpawnInterval;
        noiseOffset = Random.Range(0f, 100f);
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
        float noise = Mathf.PerlinNoise(noiseOffset, elapsedTime * noiseSpeed) * 2f - 1f;

        if (inHordePhase)
        {
            // Track how long we have been in the horde
            hordeTimer += currentSpawnInterval;
            if (hordeTimer >= hordeDuration)
            {
                inHordePhase = false;
                hordeTimer = 0f;
                Debug.Log("[TileSpawner] Exiting Horde Phase...");
            }
            // Keep spawn rate at 1s during horde
            currentSpawnInterval = 1f;
        }
        else
        {
            // Apply normal spawn rate logic
            currentSpawnInterval = Mathf.Max(minSpawnInterval, initialSpawnInterval - (elapsedTime * decayRate) + (noise * fluctationMagnitude));

            // If the interval reaches 1s, enter the horde phase ONCE
            if (currentSpawnInterval <= 1f && !inHordePhase)
            {
                inHordePhase = true;
                hordeTimer = 0f;
                hordeDuration = Random.Range(5f, 8f); // Randomize horde length
                Debug.Log("[TileSpawner] Entering Horde Phase...");
            }
        }

        Debug.Log($"[TileSpawner] Time: {elapsedTime:F2}s | Spawn Interval: {currentSpawnInterval:F2}s");

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