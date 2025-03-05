// TilePool.cs
using System.Collections.Generic;
using UnityEngine;

public class TilePool : MonoBehaviour
{
    public static TilePool Instance { get; private set; }

    [SerializeField] private GameObject[] tilePrefabs;
    [SerializeField] private int poolSize = 20;

    private Dictionary<string, Queue<TileScript>> tilePools = new Dictionary<string, Queue<TileScript>>();

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

        InitializePools();
    }

    private void InitializePools() {
        foreach (GameObject prefab in tilePrefabs) {
            Queue<TileScript> pool = new Queue<TileScript>();

            for (int i = 0; i < poolSize; i++) {
                GameObject obj = Instantiate(prefab);
                obj.name = prefab.name;
                obj.SetActive(false);
                pool.Enqueue(obj.GetComponent<TileScript>());
            }

            tilePools[prefab.name] = pool;
        }
    }

    public TileScript GetTile(string tileName) {
        if (tilePools.ContainsKey(tileName) && tilePools[tileName].Count > 0) {
            TileScript tile = tilePools[tileName].Dequeue();
            tile.gameObject.SetActive(true);
            return tile;
        }

        foreach (GameObject prefab in tilePrefabs) {
            if (prefab.name == tileName) {
                GameObject obj = Instantiate(prefab);
                obj.name = prefab.name;
                return obj.GetComponent<TileScript>();
            }
        }

        Debug.LogError($"No pool found for tile: {tileName}");
        return null;
    }

    public void ReturnTile(TileScript tile) {
        tile.gameObject.SetActive(false);
        if (!tilePools.ContainsKey(tile.name)) {
            tilePools[tile.name] = new Queue<TileScript>();
        }
        tilePools[tile.name].Enqueue(tile);
    }
}