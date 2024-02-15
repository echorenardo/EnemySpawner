using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    [SerializeField] private List<Spawn> _spawnPoints;

    public Spawn RandomSpawnPoint => GetRandomSpawnPoint();

    private Spawn GetRandomSpawnPoint()
    {
        return _spawnPoints[Random.Range(0, _spawnPoints.Count)];
    }
}