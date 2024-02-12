using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private SpawnPoints _spawnPoints;
    [SerializeField] private TargetPoints _targetPoints;
    [SerializeField] private GameObject _spawningEnemy;
    [SerializeField] private float _repeatRate = 2f;
    [SerializeField] private int _poolCapacity = 10;
    [SerializeField] private int _poolMaxSize = 10;

    private ObjectPool<GameObject> _pool;
    private GameObject _currentObject;
    private List<GameObject> _activeEnemies = new List<GameObject>();
    public Transform TargetPoint => _targetPoints.RandomTargetPoint;

    private void Awake()
    {
        _pool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(_spawningEnemy),
            actionOnGet: (enemy) => ActionOnGet(enemy),
            actionOnRelease: (enemy) => enemy.SetActive(false),
            actionOnDestroy: (enemy) => Destroy(enemy),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetEnemy), 0f, _repeatRate);
    }

    private void ActionOnGet(GameObject obj)
    {
        if (obj.TryGetComponent<Soldier>(out var soldier) == false)
            return;

        soldier.IsCome += ObjectRelease;
        obj.transform.position = _spawnPoints.RandomSpawnPoint.position;
        obj.SetActive(true);
        _activeEnemies.Add(obj);
    }

    private void GetEnemy()
    {
        _pool.Get();
    }

    private void ObjectRelease()
    {
        _pool.Release(_activeEnemies[0]);
        _activeEnemies.RemoveAt(0);
    }
}