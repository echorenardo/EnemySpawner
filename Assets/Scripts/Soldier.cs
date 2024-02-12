using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    [SerializeField] private Spawner _spawn;
    [SerializeField] private float _moveSpeed;

    private Transform _target;

    public event Action IsCome;

    private void Awake()
    {
        _target = DetermineTargetPoint();
    }

    private void Update()
    {
        GoToTargetPoint(_target);
    }

    private void GoToTargetPoint(Transform target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, _moveSpeed * Time.deltaTime);
        transform.LookAt(target);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Target>() != false)
            IsCome?.Invoke();
    }

    private Transform DetermineTargetPoint()
    {
        return _spawn.TargetPoint.transform;
    }
}