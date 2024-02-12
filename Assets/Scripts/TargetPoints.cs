using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPoints : MonoBehaviour
{
    [SerializeField] private List<GameObject> _targetPoints = new List<GameObject>();

    public Transform RandomTargetPoint => GetRandomSpawnPoint();

    private Transform GetRandomSpawnPoint()
    {
        return _targetPoints[Random.Range(0, _targetPoints.Count)].transform;
    }
}