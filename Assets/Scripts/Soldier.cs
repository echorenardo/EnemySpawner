using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    [SerializeField] private Spawner _spawn;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Animator _animator;

    public event Action IsCome;

    private void Update()
    {
        GoToTargetPoint(_spawn.TargetPoint.transform);
    }

    private void GoToTargetPoint(Transform target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, _moveSpeed * Time.deltaTime);
        transform.LookAt(target);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Target>() != false)
        {
            IsCome?.Invoke();
        }
    }
}