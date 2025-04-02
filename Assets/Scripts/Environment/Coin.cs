using UnityEngine;
using System;

public class Coin : MonoBehaviour
{
    public event Action<Coin> OnCollected;

    public void Collect()
    {
        OnCollected?.Invoke(this);
    }

    private void OnDestroy()
    {
        OnCollected = null;
    }
}