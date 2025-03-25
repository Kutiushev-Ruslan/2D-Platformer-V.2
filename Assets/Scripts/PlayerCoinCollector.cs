using UnityEngine;

public class PlayerCoinCollector : MonoBehaviour
{
    [SerializeField] private CoinSpawner _coinSpawner;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Coin>(out Coin coin))
        {
            _coinSpawner.CoinCollected(collision.gameObject);
            Destroy(collision.gameObject);
        }
    }
}