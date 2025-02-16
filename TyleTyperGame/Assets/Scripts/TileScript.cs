// TileScript.cs
using UnityEngine;

public class TileScript : MonoBehaviour
{
    [SerializeField] private float initialSpeed = 0.5f;
    [SerializeField] private float maximumSpeed = 5f;
    [SerializeField] private float spawnIntervalAcceleration = 0.05f;
    private float currentSpeed;

    private void Start() {
        float elapsedTime = GameManager.Instance.GetElapsedTime();
        currentSpeed = Mathf.Min(maximumSpeed, initialSpeed + spawnIntervalAcceleration * elapsedTime);
    }
    
    private void Update()
    {
        transform.Translate(Vector2.down * currentSpeed * Time.deltaTime);
        if (transform.position.y < -8) {
            Destroy(gameObject);
        }
    }
}
