using UnityEngine;

public class BasePanel : MonoBehaviour
    {
    private BasePanel _showPanel;
    private RectTransform _position;
    private bool _isShow, _moved;
    private Vector2 _direction;

    private int PanelWidth => Screen.width;
    private float Speed => PanelWidth * 2.5f;
    protected virtual void InitializeContents() { }

    private void Start()
        {
        _position = GetComponent<RectTransform>();
        InitializeContents();
        }

    protected virtual void Update()
        {
        if (!_moved)
            return;

        Vector2 pos = TryMove(_position.anchoredPosition);

        if (_isShow)
            {
            if ((pos.x < 0 && _direction == Vector2.left) || (pos.x > 0 && _direction == Vector2.right))
                pos = StopMove(pos, 0);
            }
        else
            {
            if (pos.x > PanelWidth && _direction == Vector2.right)
                pos = StopMove(pos, PanelWidth);
            else if (pos.x < -PanelWidth && _direction == Vector2.left)
                pos = StopMove(pos, -PanelWidth);
            }

        _position.anchoredPosition = pos;
        }

    public void Show(Vector2 direction)
        {
        _isShow = true;
        _moved = true;
        _direction = direction;
        }
    public void Hide(BasePanel panel, Vector2 direction)
        {
        _showPanel = panel;

        if (_showPanel != null)
            _showPanel.gameObject.SetActive(true);

        _isShow = false;
        _moved = true;
        _direction = direction;
        }

    private Vector3 StopMove(Vector3 position, float pos)
        {
        _moved = false;

        if (!_isShow)
            HidePanel();

        position.x = pos;

        return position;
        }
    private void HidePanel()
        {
        if (_showPanel != null)
            _showPanel.Show(_direction);

        gameObject.SetActive(false);
        }
    private Vector3 TryMove(Vector3 position) => new Vector2(position.x + (_direction == Vector2.right ? Speed : -Speed) * Time.deltaTime, 0);
    }