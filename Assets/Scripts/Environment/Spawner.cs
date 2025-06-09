using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private GameObject _medKitPrefab;
    [SerializeField] private Transform[] _coinSpawnPoints;
    [SerializeField] private Transform[] _medKitSpawnPoints;
    [SerializeField] private int _initialCoinCount = 5;
    [SerializeField] private int _initialMedKitCount = 5;

    private Queue<Transform> _availableCoinSpawnPoints = new Queue<Transform>();
    private Queue<Transform> _availableMedKitSpawnPoints = new Queue<Transform>();

    private void Start()
    {
        InitializeSpawnPoints();
        SpawnInitialObjects();
    }

    private void InitializeSpawnPoints()
    {
        foreach (Transform point in _coinSpawnPoints)
        {
            _availableCoinSpawnPoints.Enqueue(point);
        }

        foreach (Transform point in _medKitSpawnPoints)
        {
            _availableMedKitSpawnPoints.Enqueue(point);
        }
    }

    private void SpawnInitialObjects()
    {
        for (int i = 0; i < _initialCoinCount; i++)
        {
            SpawnCoin();
        }

        for (int i = 0; i < _initialMedKitCount; i++)
        {
            SpawnMedKit();
        }
    }

    private void CoinCollected(Coin coin)
    {
        Transform spawnPoint = coin.transform.parent;
        Destroy(coin.gameObject);
        _availableCoinSpawnPoints.Enqueue(spawnPoint);

        SpawnCoin();
    }

    private void MedKitCollected(MedKit medKit)
    {
        Transform spawnPoint = medKit.transform.parent;
        Destroy(medKit.gameObject);
        //_availableMedKitSpawnPoints.Enqueue(spawnPoint);

        //SpawnMedKit();
    }
    

    private void SpawnCoin()
    {
        if (_availableCoinSpawnPoints.Count > 0)
        {
            Transform spawnPoint = _availableCoinSpawnPoints.Dequeue();
            SpawnCoinAtPoint(spawnPoint);
        }
    }

    private void SpawnMedKit()
    {
        if (_availableMedKitSpawnPoints.Count > 0)
        {
            Transform spawnPoint = _availableMedKitSpawnPoints.Dequeue();
            SpawnMedKitAtPoint(spawnPoint);
        }
    }

    private void SpawnCoinAtPoint(Transform spawnPoint)
    {
        GameObject coinCopy = Instantiate(_coinPrefab, spawnPoint.position, Quaternion.identity);
        coinCopy.transform.SetParent(spawnPoint);

        Coin coin = coinCopy.GetComponent<Coin>();
        coin.Collected += CoinCollected;
    }

    private void SpawnMedKitAtPoint(Transform spawnPoint)
    {
        GameObject medKitCopy = Instantiate(_medKitPrefab, spawnPoint.position, Quaternion.identity);
        medKitCopy.transform.SetParent(spawnPoint);

        MedKit medKit = medKitCopy.GetComponent<MedKit>();
        medKit.Collected += MedKitCollected;
    }
}