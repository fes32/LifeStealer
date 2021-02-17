using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _stopDistanceBeforeTarget;
    [SerializeField] private float _speed;

    private Player _target;

    private void Update()
    {
        if (_target != null)
        {
            transform.LookAt(_target.transform);

            if (Vector3.Distance(transform.position, _target.transform.position) > _stopDistanceBeforeTarget)
            {
                Vector3 targetPosition = transform.position + transform.forward * _speed * Time.deltaTime;

                transform.position = targetPosition;

                GetComponent<EnemyAttack>().enabled = false;
            }
            else
            {
                GetComponent<EnemyAttack>().enabled = true;
            }
        }
    }

    public void Init(Player target)
    {
        _target = target;
    }
}
