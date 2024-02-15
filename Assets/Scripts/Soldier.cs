using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    [SerializeField] private Rigidbody _prefab;
    [SerializeField] private float _moveSpeed;

    private Target _target;

    public event Action<Soldier> IsCome;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _moveSpeed * Time.deltaTime);
        transform.LookAt(_target.gameObject.transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Target>(out Target reachedTarget) != false)
            IsCome?.Invoke(_prefab.GetComponent<Soldier>());
    }

    public void SetTarget(Target target)
    {
        _target = target;
    }
}