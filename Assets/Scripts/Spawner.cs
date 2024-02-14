using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private SpawnPoints _spawnPoints;
    [SerializeField] private TargetPoints _targetPoints;
    [SerializeField] private Soldier _spawningSoldier;
    [SerializeField] private float _repeatRate = 2f;
    [SerializeField] private int _poolCapacity = 10;
    [SerializeField] private int _poolMaxSize = 10;

    private ObjectPool<Soldier> _pool;
    public Transform TargetPoint => _targetPoints.RandomTargetPoint;

    private void Awake()
    {
        _pool = new ObjectPool<Soldier>(
            createFunc: () => Instantiate(_spawningSoldier),
            actionOnGet: (enemy) => ActionOnGet(enemy),
            actionOnRelease: (enemy) => enemy.gameObject.SetActive(false),
            actionOnDestroy: (enemy) => Destroy(enemy),
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
        soldier.IsCome += ObjectRelease;
        soldier.transform.position = _spawnPoints.RandomSpawnPoint.position;
        soldier.gameObject.SetActive(true);
    }

    private void GetEnemy()
    {
        _pool.Get();
    }

    private void ObjectRelease(Soldier enemy)
    {
        _pool.Release(enemy);
    }
}