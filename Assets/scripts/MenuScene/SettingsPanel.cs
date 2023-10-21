using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : BasePanel {

    [SerializeField] private BasePanel _menuPanel;
    [SerializeField] private Text _audioLabel;

    protected override void InitializeContents() {
        UpdateAudioLabel();
        gameObject.SetActive(false);
        }

    private void UpdateAudioLabel() => _audioLabel.text = "ЗВУК " + (Config.AUDIO ? "ВКЛ" : "ВЫКЛ");

    public void ToMenu() => Hide(_menuPanel, Vector3.right);
    public void ChangeAudio() {
        Config.AUDIO = !Config.AUDIO;
        PlayerPrefs.SetInt("audio", Config.AUDIO ? 1 : 0);
        UpdateAudioLabel();
        }

    }