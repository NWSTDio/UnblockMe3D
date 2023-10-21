using UnityEngine;
using UnityEngine.UI;

public class Lang : MonoBehaviour {

    [SerializeField] private Text _text;

    public void ChangeLangName(string name) {
        _text.text = name;
        }

    }