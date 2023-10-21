using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SplashScreenPanel : MonoBehaviour {

    [SerializeField] private GameObject _splashScreenPanel;
    [SerializeField] private BasePanel _menuPanel;

    private readonly float _maxAlpha = 1;

    private Text _splashScreenLabel;
    private float _scale = 0, _alpha = 0;
    private float _direction;
    private float _speed = 1f;

    private void Start() {
        if (Config.SPLASH)
            StartCoroutine(HideAnimation());
        else {
            _splashScreenLabel = _splashScreenPanel.GetComponent<Text>();

            ChangeScale();
            ChangeAlpha();

            _direction = 1;
            }
        }
    private void ChangeAlpha() {
        Color color = _splashScreenLabel.color;

        _splashScreenLabel.color = new Color(color.r, color.g, color.b, _alpha);
        }
    private void ChangeScale() => transform.localScale = new Vector3(_scale, _scale, transform.localScale.z);
    private void Update() {
        if (Config.SPLASH)
            return;
        if (_alpha < 0)
            return;

        _alpha += Time.deltaTime * _speed * _direction;

        if (_alpha > _maxAlpha && _direction == 1)
            _direction = -1;
        else if (_alpha < 0 && _direction == -1)
            StartCoroutine(HideAnimation());

        ChangeAlpha();

        if (_scale > 1f)
            return;

        _scale += Time.deltaTime * _speed;

        ChangeScale();
        }
    private IEnumerator HideAnimation() {
        Config.SPLASH = true;

        yield return new WaitForSeconds(.1f);

        _menuPanel.Show(Vector2.right);

        gameObject.SetActive(false);
        }

    }