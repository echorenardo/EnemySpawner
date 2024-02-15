using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;

    public event Action<Soldier> IsCome;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Target>() != false)
            IsCome?.Invoke(_enemy.GetComponent<Soldier>());
    }
}