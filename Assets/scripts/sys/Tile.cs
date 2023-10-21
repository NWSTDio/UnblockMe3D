using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Tile : MonoBehaviour {

    [SerializeField] private LayerMask _objectSelectionMask;

    protected Config.AXIS _axis;
    protected Vector3 _destinationPosition;
    protected float _scale;

    private readonly float _autoMoveSpeed = 4f, _moveSpeed = 16f;
    private readonly float _moveDeadZone = .5f;

    private Camera _camera;
    private Config.DIRECTION _moveDirection;
    private Vector3 _prevMouseDownClick, _mouseOffset;
    private bool _active, _isMoved = false;
    private float _mouseZCoord;
    private float _distanceRay;

    private Vector3 _startTilePosition;

    public float HorizontalDirection => IsLeftDirection ? 1 : -1;
    public float VerticalDirection => IsUpDirection ? 1 : -1;
    public bool IsHorizontalAxis => _axis == Config.AXIS.HORIZONTAL;
    public bool IsLeftDirection => _moveDirection == Config.DIRECTION.LEFT;
    public bool IsUpDirection => _moveDirection == Config.DIRECTION.UP;

    protected virtual Vector3 TryMove(Vector3 position, float speed) => position;
    protected virtual Vector3 FreezeAxis(Vector3 position) => position;
    protected virtual bool IsEndAutoMove(Vector3 position) => false;
    protected virtual void SetDestinationPosition() { }

    protected virtual void Start() {
        _camera = Camera.main;
        }
    private void Update() {
        if (_active && _isMoved)
            Move();
        else if (_isMoved)
            AutoMove();
        }
    private void OnMouseDown() {
        if (_isMoved)
            return;

        _active = true;

        _startTilePosition = transform.position;

        _prevMouseDownClick = Input.mousePosition;

        _mouseZCoord = _camera.WorldToScreenPoint(transform.position).z;

        _mouseOffset = transform.position - GetMouseWorldPosition();
        }
    private void OnMouseDrag() {
        if (!_active)
            return;

        if (IsNotSwipe())
            return;

        _isMoved = true;

        _destinationPosition = GetPositionAxis();

        _moveDirection = Config.Direction(_camera.WorldToScreenPoint(_destinationPosition) - _camera.WorldToScreenPoint(transform.position));
        }
    private void OnMouseUp() {
        if (!_active)
            return;

        _active = false;

        if (!_isMoved)
            return;

        SetDestinationPosition();
        }

    public void ChangeScale(float scale) {
        transform.localScale = new Vector3(transform.localScale.x - .05f, transform.localScale.y, scale - .05f);

        _scale = Mathf.Ceil(scale) / 2;

        _distanceRay = _scale + .05f;
        }

    private void Move() {
        Vector3 position = transform.position;

        position = TryMove(position, _moveSpeed);

        if (RayCollision(position) || IsDeadZone(position))
            position = transform.position;

        transform.position = position;
        }
    private void AutoMove() {
        Vector3 position = transform.position;

        position = TryMove(position, _autoMoveSpeed);

        if (IsEndAutoMove(position)) {
            _isMoved = false;
            position = _destinationPosition;
            if (_startTilePosition != position) {
                Events.OnPlaySoundMoveTile.Invoke();
                Events.OnStepLabelChange.Invoke();
                }
            }

        transform.position = position;
        }
    private bool RayCollision(Vector3 pos) {
        Vector3 direction = transform.forward * (IsHorizontalAxis ? HorizontalDirection : VerticalDirection);

        pos.y = transform.position.y - transform.localScale.y / 2;

        var ray = new Ray(pos, direction);
        Debug.DrawRay(ray.origin, ray.direction * _distanceRay);

        if (Physics.Raycast(ray, out _, _distanceRay, _objectSelectionMask))
            return true;

        return false;
        }
    private bool IsDeadZone(Vector3 position) => (_destinationPosition - position).magnitude < _moveDeadZone;
    private bool IsNotSwipe() {
        Vector3 _currentMousePosition = Input.mousePosition;

        if (_currentMousePosition == _prevMouseDownClick)
            return true;

        _prevMouseDownClick = _currentMousePosition;

        return false;
        }
    private Vector3 GetMouseWorldPosition() {
        Vector3 pos = Input.mousePosition;

        return _camera.ScreenToWorldPoint(new Vector3(pos.x, pos.y, _mouseZCoord));
        }
    private Vector3 GetPositionAxis() => FreezePositionAxis(GetMouseWorldPosition() + _mouseOffset);
    private Vector3 FreezePositionAxis(Vector3 position) {
        position = FreezeAxis(position);

        return new Vector3(position.x, transform.position.y, position.z);
        }

    }