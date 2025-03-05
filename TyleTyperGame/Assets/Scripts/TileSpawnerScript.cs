// TileSpawnerScipt.cs
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    public static float initialSpawnInterval = 5f;
    public static float currentSpawnInterval;

    public enum SpawnState
    {
        Calm,
        Horde,
    }

    private SpawnState currentState;

    [SerializeField]
    private GameObject[] tilePrefabs;

    [SerializeField]
    private float spawnRangeX = 10f;

    [SerializeField]
    private float calmMinSpawnInterval = 3f;

    [SerializeField]
    private float calmMaxSpawnInterval = 5f;

    [SerializeField]
    private float hordeMinSpawnInterval = 1f;

    [SerializeField]
    private float hordeMaxSpawnInterval = 1.5f;

    [SerializeField]
    private float calmPhaseDurationMin = 8f;

    [SerializeField]
    private float calmPhaseDurationMax = 12f;

    [SerializeField]
    private float hordePhaseDurationMin = 4f;

    [SerializeField]
    private float hordePhaseDurationMax = 7f;

    private float phaseTimer;
    private float currentPhaseDuration;

    private void Start()
    {
        SetCalmPhase();
        currentSpawnInterval = initialSpawnInterval;
        Invoke(nameof(SpawnTile), GetNextSpawnInterval());
    }

    private void SpawnTile()
    {
        GameObject randomTilePrefab = GetRandomTile();
        string tileName = randomTilePrefab.name;
        TileScript tileScript = TilePool.Instance.GetTile(tileName);

        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        tileScript.transform.position = new Vector2(randomX, transform.position.y);

        tileScript.SetTileWord(tileName.Replace("Tile", ""));

        float spawnInterval = GetNextSpawnInterval();
        currentSpawnInterval = spawnInterval;

        phaseTimer += spawnInterval;

        if (phaseTimer >= currentPhaseDuration)
        {
            SwitchPhase();
        }

        Invoke(nameof(SpawnTile), spawnInterval);
    }

    private void SwitchPhase()
    {
        phaseTimer = 0;
        if (currentState == SpawnState.Calm)
        {
            SetHordePhase();
        }
        else
        {
            SetCalmPhase();
        }
    }

    private void SetCalmPhase()
    {
        currentState = SpawnState.Calm;
        currentPhaseDuration = Random.Range(calmPhaseDurationMin, calmPhaseDurationMax);
    }

    private void SetHordePhase()
    {
        currentState = SpawnState.Horde;
        currentPhaseDuration = Random.Range(hordePhaseDurationMin, hordePhaseDurationMax);
    }

    private float GetNextSpawnInterval()
    {
        return currentState == SpawnState.Calm
            ? Random.Range(calmMinSpawnInterval, calmMaxSpawnInterval)
            : Random.Range(hordeMinSpawnInterval, hordeMaxSpawnInterval);
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
        Gizmos.DrawWireSphere(
            new Vector3(transform.position.x - spawnRangeX, transform.position.y, 0),
            0.2f
        );
        Gizmos.DrawWireSphere(
            new Vector3(transform.position.x + spawnRangeX, transform.position.y, 0),
            0.2f
        );
    }
}
