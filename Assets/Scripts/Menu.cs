using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public static void Restart()
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
        UIManager.AccessInstance.EndUIEnable(false);
        Restart();
        SceneManager.LoadScene("Menu Scene");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
