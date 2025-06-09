using UnityEngine;

public class PlayerItemCollector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Coin>(out Coin coin))
        {
            coin.Collect();
        }

        if (collision.TryGetComponent<MedKit>(out MedKit medKit))
        {
            medKit.Collect();
        }
    }
}