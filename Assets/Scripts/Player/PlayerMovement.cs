using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;

    [SerializeField] private Transform[] _endMapPoints= new Transform[2];

    private float _minZEndMapPoint;
    private float _maxZEndMapPoint;
    private float _minXEndMapPoint;
    private float _maxXEndMapPoint;

    private PlayerInput _input;
    private Vector2 _direction;
    private Vector2 _rotate;
    private Vector2 _rotation;

    private void OnEnable()
    {
        _input = new PlayerInput();
        _input.Enable();

        _minXEndMapPoint = _endMapPoints[0].position.x;
        _maxXEndMapPoint = _endMapPoints[1].position.x;

        _minZEndMapPoint = _endMapPoints[1].position.z;
        _maxZEndMapPoint = _endMapPoints[0].position.z;
    }

    private void Update()
    {
          _rotate = _input.Player.Look.ReadValue<Vector2>();
        _direction = _input.Player.Move.ReadValue<Vector2>();

        Look(_rotate);
        Move(_direction);
    }

    private void Move(Vector2 direction)
    {
        float scaleMoveSpeed = _moveSpeed * Time.deltaTime;
        Vector3 move = Quaternion.Euler(0, transform.eulerAngles.y, 0) * new Vector3(direction.x, 0,direction.y);

        Vector3 targetPosition = transform.position + move * scaleMoveSpeed;

        if(targetPosition.x>_minXEndMapPoint & targetPosition.x < _maxXEndMapPoint)
        {
            if (targetPosition.z > _minZEndMapPoint & targetPosition.z < _maxZEndMapPoint)
            {
                transform.position = targetPosition;
            }
        }
    }

    private void Look(Vector2 rotate)
    {
        float scaleRotateSpeed = _rotateSpeed * Time.deltaTime;
        _rotation.y += rotate.x * scaleRotateSpeed;
        _rotation.x = 0;
        transform.localEulerAngles = _rotation;
    }
}
