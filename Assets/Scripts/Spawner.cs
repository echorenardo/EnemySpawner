using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEngine.GraphicsBuffer;

public class Spawner : MonoBehaviour
{
    [SerializeField] private SpawnPoints _spawnPoints;
    [SerializeField] private TargetPoints _targetPoints;
    [SerializeField] private Soldier _spawningSoldier;
    [SerializeField] private float _repeatRate = 2f;
    [SerializeField] private int _poolCapacity = 10;
    [SerializeField] private int _poolMaxSize = 10;

    private ObjectPool<Soldier> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Soldier>(
            createFunc: () => Instantiate(_spawningSoldier),
            actionOnGet: (soldier) => ActionOnGet(soldier),
            actionOnRelease: (soldier) => soldier.gameObject.SetActive(false),
            actionOnDestroy: (soldier) => Destroy(soldier),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetEnemy), 0f, _repeatRate);
    }

    private void ActionOnGet(Soldier soldier)
    {
        Target currentTarget = DetermineTarget();
        soldier.IsCome += ObjectRelease;
        soldier.transform.position = _spawnPoints.RandomSpawnPoint.transform.position;
        soldier.SetTarget(currentTarget);
        soldier.gameObject.SetActive(true);
    }

    private void GetEnemy()
    {
        _pool.Get();
    }

    private void ObjectRelease(Soldier soldier)
    {
        _pool.Release(soldier);
    }

    private Target DetermineTarget()
    {
        return _targetPoints.RandomTargetPoint;
    }
}