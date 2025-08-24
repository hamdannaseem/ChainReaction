using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Game Scene");
    }
    public void Restart()
    {
        ReactionManager.AccessInstance.StopReaction();
        int Length = PoolManager.AccessInstance.ActiveCount();
        for (int i = 0; i < Length; i--)
        {
            PoolManager.AccessInstance.Pop();
        }
        UIManager UM = UIManager.AccessInstance;
        UM.SetInteractable(true);
        UM.SetScore(-UIManager.AccessInstance.Score);
        UM.SetTime(0);
    }
    public void ToMenu()
    {
        Restart();
        SceneManager.LoadScene("Main Menu");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
