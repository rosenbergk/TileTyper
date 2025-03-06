// CollisionDetector.cs
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("DeathCollider"))
        {
            AudioManager.Instance.PlayGameOverSound();
            if (GameManager.Instance != null)
            {
                GameManager.Instance.GameOver();
            }
        }
    }
}
