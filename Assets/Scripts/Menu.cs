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
        
        UIManager UM = UIManager.AccessInstance;
        UM.SetInteractable(true);
        ReactionManager.AccessInstance.StopReaction(false);
        int Length = PoolManager.AccessInstance.ActiveCount();
        for (int i = 0; i < Length; i--)
        {
            PoolManager.AccessInstance.Pop();
        }
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
