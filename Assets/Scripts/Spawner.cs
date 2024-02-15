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
    [SerializeField] private float _soldierMoveSpeed;
    [SerializeField] private float _repeatRate = 2f;
    [SerializeField] private int _poolCapacity = 10;
    [SerializeField] private int _poolMaxSize = 10;

    private ObjectPool<Soldier> _pool;

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
        Target currentTarget = DetermineTarget();
        soldier.IsCome += ObjectRelease;
        soldier.transform.position = _spawnPoints.RandomSpawnPoint.transform.position;
        soldier.gameObject.SetActive(true);
        soldier.transform.position = Vector3.MoveTowards(transform.position, currentTarget.transform.position, _soldierMoveSpeed * Time.deltaTime);
        soldier.transform.LookAt(currentTarget.gameObject.transform);
    }

    private void GetEnemy()
    {
        _pool.Get();
    }

    private void ObjectRelease(Soldier enemy)
    {
        _pool.Release(enemy);
    }

    private Target DetermineTarget()
    {
        return _targetPoints.RandomTargetPoint;
    }
}