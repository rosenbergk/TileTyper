using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioClip typingSound;
    public AudioClip correctWordSound;
    public AudioClip gameOverSound;
    private AudioSource audioSource;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void PlayTypingSound()
    {
        audioSource.PlayOneShot(typingSound);
    }

    public void PlayCorrectWordSound()
    {
        audioSource.PlayOneShot(correctWordSound);
    }

    public void PlayGameOverSound()
    {
        audioSource.PlayOneShot(gameOverSound);
    }
}
