using UnityEngine;

public class VerticalTile : Tile
    {
    protected override void Start()
        {
        base.Start();
        _axis = Config.AXIS.VERTICAL;
        }
    protected override Vector3 TryMove(Vector3 position, float speed)
        {
        position.x += speed * Time.deltaTime * VerticalDirection;
        return position;
        }
    protected override Vector3 FreezeAxis(Vector3 position)
        {
        position.z = transform.position.z;
        return position;
        }
    protected override bool IsEndAutoMove(Vector3 position)
        {
        if (IsUpDirection && position.x >= _destinationPosition.x)
            return true;
        else if (!IsUpDirection && position.x <= _destinationPosition.x)
            return true;
        return false;
        }
    protected override void SetDestinationPosition()
        {
        float border = transform.position.x + (IsUpDirection ? _scale : -_scale);// текущая граница обьекта

        _destinationPosition.x = (IsUpDirection ? Mathf.Ceil(border) : Mathf.Floor(border)) + (IsUpDirection ? -_scale : _scale);
        }
    }