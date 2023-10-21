using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPanel : BasePanel {

    [SerializeField] private BasePanel _settingsPanel, _languagePanel;
    [SerializeField] private GameObject _bonusPanel;

    private void Awake() {
        Config.AUDIO = PlayerPrefs.GetInt("audio", 1) == 1;
        Config.Level = 1;
        }

    public void ToSettings() => Hide(_settingsPanel, Vector2.left);
    public void StartGame() => SceneManager.LoadScene("GameScene");
    public void ExitGame() => Application.Quit();
    public void ShowBonus() => _bonusPanel.SetActive(true);
    public void HideBonus() => _bonusPanel.SetActive(false);
    public void ToLanguagePanel() => Hide(_languagePanel, Vector2.left);

    }