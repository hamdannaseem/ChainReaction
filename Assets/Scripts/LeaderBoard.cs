using UnityEngine;

public class LeaderBoard : MonoBehaviour
{
    public static LeaderBoard AccessInstance;
    [SerializeField] private Leader[] Leaders;
    [SerializeField] private int LeaderBoardLimit;
    int LeaderCount;
    string NameText = "LeaderName_", ScoreText = "LeaderScore_", TimeText = "LeaderTime_";
    void Awake()
    {
        AccessInstance=this;
        LeaderCount = PlayerPrefs.GetInt("LeaderCount");
    }
    public void FetchData()
    {

        for (int i = 0; i < LeaderCount; i++)
        {
            Leaders[i].SetName(PlayerPrefs.GetString(NameText + i));
            Leaders[i].SetScore(PlayerPrefs.GetInt(ScoreText + i));
            Leaders[i].SetTime(PlayerPrefs.GetInt(TimeText + i));
        }
    }
    public void AddLeader(string Name, int Score, int Time)
    {
        int i;
        for (i = 0; i < LeaderCount; i++)
        {
            if (Score > PlayerPrefs.GetInt(ScoreText + i))
            {
                break;
            }
        }
        if (i >= LeaderBoardLimit) return;
        if (LeaderCount < LeaderBoardLimit)
        {
            LeaderCount++;
            PlayerPrefs.SetInt("LeaderCount", LeaderCount);
        }
        for (int j = LeaderCount-1; j > i; j--)
        {
            PlayerPrefs.SetString(NameText + j, PlayerPrefs.GetString(NameText + (j - 1)));
            PlayerPrefs.SetInt(ScoreText + j, PlayerPrefs.GetInt(ScoreText + (j - 1)));
            PlayerPrefs.SetInt(TimeText + j, PlayerPrefs.GetInt(TimeText + (j - 1)));
        }
        PlayerPrefs.SetString(NameText + i, Name);
        PlayerPrefs.SetInt(ScoreText + i, Score);
        PlayerPrefs.SetInt(TimeText + i, Time);
        PlayerPrefs.Save();
        FetchData();
        Leaders[i].HighLight();
    }
}
