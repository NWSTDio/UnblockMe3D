using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent<Tile>(out _)) {
            Config.NextLevel();
            SceneManager.LoadScene("GameScene");
            }
        }

    }