using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    private const string Speed = "Speed";

    [SerializeField] private Rigidbody _prefab;
    [SerializeField, Range(0f, 4f)] private float _maxMoveSpeed;
    [SerializeField] private Animator _animator;

    private Target _target;
    private float _triggerDistance = 1f;

    public void ChangeState(bool state) => gameObject.SetActive(state);

    public void SetTarget(Target target)
    {
        _target = target;
        ChangeState(true);
    }

    private void OnEnable()
    {
        StartCoroutine(SpeedUp());
    }

    private void Update()
    {
        if ((_target.transform.position - transform.position).magnitude < _triggerDistance)
        {
            ChangeState(false);
            Clear();
        }
    }

    private IEnumerator SpeedUp()
    {
        Vector3 targetPosition = _target.transform.position;
        float moveSpeed;

        while (transform.position != targetPosition)
        {
            moveSpeed = (targetPosition - transform.position).magnitude;

            if (moveSpeed > _maxMoveSpeed)
                moveSpeed = _maxMoveSpeed;

            _animator.SetFloat(Speed, moveSpeed);

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            transform.LookAt(targetPosition);
            yield return null;
        }
    }

    private void Clear() => _target = null;
}
