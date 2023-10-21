using UnityEngine;
using UnityEngine.UI;

public class StepLabel : MonoBehaviour {

    private Text _label;
    private int _step;

    private void OnEnable() {
        Events.OnStepLabelChange += ChangeLabel;
        }
    private void Start() {
        _label = GetComponent<Text>();
        }
    private void OnDisable() {
        Events.OnStepLabelChange -= ChangeLabel;
        }

    private void ChangeLabel() {
        _label.text = "шагов: " + ++_step;
        }

    }