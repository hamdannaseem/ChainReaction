using TMPro;
using UnityEngine;

public class Leader : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Name, Score, Time;
    public void SetName(string name)
    {
        Name.text = name;
    }
    public void SetScore(int score)
    {
        Score.text = score+"";
    }
    public void SetTime(int time)
    {
        Time.text = time+"";
    }
}
