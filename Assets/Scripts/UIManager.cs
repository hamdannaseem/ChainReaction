using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager AccessInstance;
    public int Score, Time;
    [SerializeField] private Button[] SpawnButtons;
    [SerializeField] private TextMeshProUGUI CountText, ScoreText, TimeText, ScoreTitle;
    [SerializeField] private GameObject EndUI, TouchText;
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
        Time = value;
        TimeText.text = "Time: " + value + "s";
    }
    public void End()
    {
        EndUI.SetActive(true);
        ScoreTitle.text = "Score: " + Score;
        LeaderBoard.AccessInstance.AddLeader(PlayerPrefs.GetString("CurrentPlayer"), Score, Time);
        Menu.Restart();
    }
    public void EndUIEnable(bool state)
    {
        EndUI.SetActive(state);
    }
    public void EnableTouch(bool state)
    {
        TouchText.SetActive(state);
    }
}
