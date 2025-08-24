using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager AccessInstance;
    public int Score;
    [SerializeField] private Button[] SpawnButtons;
    public TextMeshProUGUI CountText, ScoreText, TimeText;
    void Start()
    {
        AccessInstance = this;
        Score = 0;
        SetScore(Score);
        SetTime(ReactionManager.AccessInstance.ReactionTime);
    }
    public void SetInteractable(bool state)
    {
        foreach (Button button in SpawnButtons)
        {
            if (button.name == "Remove" && !ReactionManager.AccessInstance.Reacting) { continue; }
            button.interactable = state;
        }
    }
    public void SetCount(int Count, int Limit)
    {
        CountText.text = $"{Count}/{Limit}";
    }
    public void SetScore(int value)
    {
        Score += value;
        ScoreText.text = "Score: " + Score;
    }
    public void SetTime(int value)
    {
        TimeText.text = "Time: " + value + "s";
    }
}
