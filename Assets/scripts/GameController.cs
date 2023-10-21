using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    [SerializeField] private GameObject _pausePanel;

    private void Update() {
        if (Input.GetKeyUp(KeyCode.Space))
            SceneManager.LoadScene("GameScene");
        }

    public void ClickPause() {
        Config.RUNNING = false;
        _pausePanel.SetActive(true);
        }
    public void ClickResume() {
        Config.RUNNING = true;
        _pausePanel.SetActive(false);
        }
    public void ClickMenu() => SceneManager.LoadScene("MenuScene");
    public void ClickRestart() => SceneManager.LoadScene("GameScene");

    }