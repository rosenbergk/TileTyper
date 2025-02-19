using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionDetector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("DeathCollider"))
        {
            AudioManager.Instance.PlayGameOverSound();
            Debug.Log("Tile hit DeathCollider! Restarting scene.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}