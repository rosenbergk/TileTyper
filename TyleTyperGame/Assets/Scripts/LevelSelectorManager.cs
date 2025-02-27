// LevelSelectorManager.cs
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectorManager : MonoBehaviour
{
    public void StartColorLevel()
    {
        GameManager.Instance.StartGame(false);
        SceneManager.LoadScene("ColorLevel");
    }

    public void StartTutorialLevel()
    {
        GameManager.Instance.StartGame(true);
        SceneManager.LoadScene("TutorialLevel");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void StartShapeLevel() {
        GameManager.Instance.StartGame(true);
        SceneManager.LoadScene("ShapeLevel");
    }
}