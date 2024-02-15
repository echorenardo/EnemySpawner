using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPoints : MonoBehaviour
{
    [SerializeField] private List<Target> _targetPoints;

    public Target RandomTargetPoint => GetRandomTargetPoint();

    private Target GetRandomTargetPoint()
    {
        Target currentTarget = _targetPoints[Random.Range(0, _targetPoints.Count)];
        Debug.Log(currentTarget.transform.position);
        return currentTarget;
    }
}