using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] float _spawnTimer = 1f;
    [SerializeField] int objectPoolSize = 5;
    GameObject[] objectPool;

    private void Awake()
    {
        PopulatePool();
    }

    private void PopulatePool()
    {
        objectPool = new GameObject[objectPoolSize];

        for (int i = 0; i < objectPool.Length; i++)
        {
            objectPool[i] = Instantiate(_enemyPrefab, transform);
            objectPool[i].SetActive(false);
        }
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while(true)
        {
            EnableObjectPool();
            yield return new WaitForSeconds(_spawnTimer);
        }

    }

    private void EnableObjectPool()
    {
        foreach (GameObject obj in objectPool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return;
            }
            else
            {
                continue;
            }
        }
    }
}
