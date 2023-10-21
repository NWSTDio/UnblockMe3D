using UnityEngine;
using UnityEngine.UI;

public class LevelLabel : MonoBehaviour {

    private Text _label;

    private void Start() {
        _label = GetComponent<Text>();
        ChangeLabel();
        }

    private void ChangeLabel() {
        _label.text = "Level " + Config.Level;
        }

    }