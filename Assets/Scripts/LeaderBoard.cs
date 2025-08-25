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

        for (int i = 1; i <= LeaderCount; i++)
        {
            int j = i - 1;
            Leaders[j].SetName(PlayerPrefs.GetString(NameText + i));
            Leaders[j].SetScore(PlayerPrefs.GetInt(ScoreText + i));
            Leaders[j].SetTime(PlayerPrefs.GetInt(TimeText + i));
        }
    }
    public void AddLeader(string Name, int Score, int Time)
    {
        int i;
        for (i = 1; i < LeaderCount; i++)
        {
            if (Score > PlayerPrefs.GetInt(ScoreText + i, 0))
            {
                break;
            }
        }
        if (i > LeaderBoardLimit) return;
        if (LeaderCount < LeaderBoardLimit)
        {
            LeaderCount++;
            PlayerPrefs.SetInt("LeaderCount", LeaderCount);
        }
        for (int j = LeaderCount; j > i; j--)
        {
            PlayerPrefs.SetString(NameText + j, PlayerPrefs.GetString(NameText + (j - 1)));
            PlayerPrefs.SetInt(ScoreText + j, PlayerPrefs.GetInt(ScoreText + (j - 1)));
            PlayerPrefs.SetInt(TimeText + j, PlayerPrefs.GetInt(TimeText + (j - 1)));
        }
        PlayerPrefs.SetString(NameText + i, Name);
        PlayerPrefs.SetInt(ScoreText + i, Score);
        PlayerPrefs.SetInt(TimeText + i, Time);
        FetchData();
        Leaders[i].HighLight();
    }
}
