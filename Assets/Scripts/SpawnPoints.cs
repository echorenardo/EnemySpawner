using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    [SerializeField] private List<GameObject> _spawnPoints = new List<GameObject>();

    public Transform RandomSpawnPoint => GetRandomTargetPoint();

    private Transform GetRandomTargetPoint()
    {
        return _spawnPoints[Random.Range(0, _spawnPoints.Count)].transform;
    }
}