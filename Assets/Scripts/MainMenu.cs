using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_InputField Name;
    [SerializeField] private GameObject NameError, SettingsPanel, CreditsPanel;
    [SerializeField] private Slider SpawnLimit, ReactionTime;
    [SerializeField] private TextMeshProUGUI SpawnLimitText, ReactionTimeText;
    [SerializeField] private Settings settings;
    void Start()
    {
        SpawnLimit.value = settings.SpawnLimit;
        SpawnLimitText.text = settings.SpawnLimit + "";
        ReactionTime.value = settings.ReactionTime;
        ReactionTimeText.text = settings.ReactionTime + "s";
    }
    public void StartGame()
    {
        if (string.IsNullOrWhiteSpace(Name.text))
        {
            ErrorName();
            return;
        }
        else
        {
            PlayerPrefs.SetString("CurrentPlayer", Name.text);
        }
        Debug.Log(Name.text);
        SceneManager.LoadScene("Game Scene");
    }
    async void ErrorName()
    {
        NameError.SetActive(true);
        await Task.Delay(3000);
        NameError.SetActive(false);
    }
    public void Settings()
    {
        if (!SettingsPanel.activeSelf)
        {
            CreditsPanel.SetActive(false);
            SettingsPanel.SetActive(true);
        }
        else
        {
            SettingsPanel.SetActive(false);
        }
    }
    public void Credits()
    {
        if (!CreditsPanel.activeSelf)
        {
            SettingsPanel.SetActive(false);
            CreditsPanel.SetActive(true);
        }
        else
        {
            CreditsPanel.SetActive(false);
        }
    }
    public void SetValues()
    {
        settings.SpawnLimit = (int)SpawnLimit.value;
        SpawnLimitText.text = SpawnLimit.value + "";
        settings.ReactionTime = (int)ReactionTime.value;
        ReactionTimeText.text = ReactionTime.value + "s";
    }
    public void Quit()
    {
        Application.Quit();
    }

}
