using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager AccessInstance;
    [SerializeField] private Button[] SpawnButtons;
    void Start()
    {
        AccessInstance = this;
    }
    public void SetInteractable(bool state)
    {
        foreach (Button button in SpawnButtons)
        {
            if (button.name == "Remove" && !ReactionManager.AccessInstance.ReactionStarted){ continue; }
            button.interactable = state;
        }
    }
}
