using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "MainMenuSystem", menuName = "btg-game-jam-2019/MainMenuSystem", order = 0)]
public class MainMenuSystem : ScriptableObject
{
    public void LoadMainLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
