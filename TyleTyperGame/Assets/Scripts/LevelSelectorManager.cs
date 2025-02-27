// LevelSelectorManager.cs
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectorManager : MonoBehaviour
{
    public void StartNormalGame()
    {
        GameManager.Instance.StartGame(false);
        SceneManager.LoadScene("ColorLevel");
    }

    public void StartTutorialGame()
    {
        GameManager.Instance.StartGame(true);
        SceneManager.LoadScene("TutorialLevel");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}