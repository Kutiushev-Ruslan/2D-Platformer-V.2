using UnityEngine;

public class PlayerItemCollector : MonoBehaviour
{
    [SerializeField] private CoinSpawner _coinSpawner;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Coin>(out _))
        {
            _coinSpawner.CoinCollected(collision.gameObject);
            Destroy(collision.gameObject);
        }
    }
}