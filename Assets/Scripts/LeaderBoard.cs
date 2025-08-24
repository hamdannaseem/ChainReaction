using UnityEngine;

public class LeaderBoard : MonoBehaviour
{
    [SerializeField]private Leader[] Leaders;
    int LeaderCount;
    void Start()
    {
        LeaderCount = PlayerPrefs.GetInt("LeaderCount");
    }
}
