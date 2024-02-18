using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [SerializeField] private SpawnPoints _spawnPoints;
    [SerializeField] private TargetPoints _targetPoints;
    [SerializeField] private Soldier _spawningSoldier;
    [SerializeField] private float _repeatRate = 2f;
    [SerializeField] private int _poolMaxSize = 10;

    private List<Soldier> _pool = new List<Soldier>();

    private void Awake()
    {
        for (int i = 0; i < _poolMaxSize; i++)
        {
            Soldier soldier = Instantiate(_spawningSoldier);
            soldier.ChangeState(false);
            _pool.Add(soldier);
        }
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetEnemy), 0f, _repeatRate);
    }

    private void GetEnemy()
    {
        Soldier soldier = _pool.FirstOrDefault(currentSoldier => currentSoldier.gameObject.activeSelf == false);

        if (soldier != null)
        {
            soldier.transform.position = _spawnPoints.RandomSpawnPoint.transform.position;
            soldier.SetTarget(DetermineTarget());
        }
    }

    private Target DetermineTarget()
    {
        return _targetPoints.RandomTargetPoint;
    }
}