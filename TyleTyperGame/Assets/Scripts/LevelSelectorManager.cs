// LevelSelectorManager.cs
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectorManager : MonoBehaviour
{
    public void LoadTutorialLevel()
    {
        SceneManager.LoadScene("TutorialLevel");
    }

    public void LoadColorLevel()
    {
        SceneManager.LoadScene("ColorLevel");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}