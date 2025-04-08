using UnityEngine;
using System.Collections.Generic;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private int _initialCoinCount = 5;

    private Queue<Transform> _availableSpawnPoints = new Queue<Transform>();

    private void Start()
    {
        InitializeSpawnPoints();
        SpawnInitialCoins();
    }

    private void InitializeSpawnPoints()
    {
        foreach (Transform point in _spawnPoints)
        {
            _availableSpawnPoints.Enqueue(point);
        }
    }

    private void SpawnInitialCoins()
    {
        for (int i = 0; i < _initialCoinCount; i++)
        {
            SpawnCoin();
        }
    }

    private void OnCoinCollected(Coin coin)
    {
        Transform spawnPoint = coin.transform.parent;
        Destroy(coin.gameObject);
        _availableSpawnPoints.Enqueue(spawnPoint);

        SpawnCoin();
    }

    private void SpawnCoin()
    {
        if (_availableSpawnPoints.Count > 0)
        {
            Transform spawnPoint = _availableSpawnPoints.Dequeue();
            SpawnCoinAtPoint(spawnPoint);
        }
    }

    private void SpawnCoinAtPoint(Transform spawnPoint)
    {
        GameObject coinCopy = Instantiate(_coinPrefab, spawnPoint.position, Quaternion.identity);
        coinCopy.transform.SetParent(spawnPoint);

        Coin coin = coinCopy.GetComponent<Coin>();
        coin.Collected += CoinCollected;
    }
}