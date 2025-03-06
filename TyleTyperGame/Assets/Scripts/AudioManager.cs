// AudioManager.cs
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField]
    private AudioClip typingSound;

    [SerializeField]
    private AudioClip correctWordSound;

    [SerializeField]
    private AudioClip gameOverSound;

    [SerializeField]
    private AudioClip incorrectWordSound;

    private AudioSource audioSource;

    public void PlayTypingSound()
    {
        audioSource.PlayOneShot(typingSound);
    }

    public void PlayCorrectWordSound()
    {
        audioSource.PlayOneShot(correctWordSound);
    }

    public void PlayIncorrectWordSound()
    {
        audioSource.PlayOneShot(incorrectWordSound);
    }

    public void PlayGameOverSound()
    {
        audioSource.PlayOneShot(gameOverSound);
    }

    private void Awake()
    {
        transform.SetParent(null);

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
}
