using UnityEngine;

public class HorizontalTile : Tile {

    protected override void Start() {
        base.Start();

        _axis = Config.AXIS.HORIZONTAL;
        }

    protected override Vector3 TryMove(Vector3 position, float speed) {
        position.z += speed * Time.deltaTime * HorizontalDirection;

        return position;
        }
    protected override Vector3 FreezeAxis(Vector3 position) {
        position.x = transform.position.x;

        return position;
        }
    protected override bool IsEndAutoMove(Vector3 position) {
        if (IsLeftDirection && position.z >= _destinationPosition.z)
            return true;
        else if (!IsLeftDirection && position.z <= _destinationPosition.z)
            return true;

        return false;
        }
    protected override void SetDestinationPosition() {
        float border = transform.position.z + (IsLeftDirection ? _scale : -_scale);// ������� ������� �������

        _destinationPosition.z = (IsLeftDirection ? Mathf.Ceil(border) : Mathf.Floor(border)) + (IsLeftDirection ? -_scale : _scale);
        }

    }