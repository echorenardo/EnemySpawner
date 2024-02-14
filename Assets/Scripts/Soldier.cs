using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private Spawner _spawn;
    [SerializeField] private float _moveSpeed;

    private Transform _target;

    public event Action<Soldier> IsCome;

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
            IsCome?.Invoke(_enemy.GetComponent<Soldier>());
    }

    private Transform DetermineTargetPoint()
    {
        return _spawn.TargetPoint.transform;
    }
}