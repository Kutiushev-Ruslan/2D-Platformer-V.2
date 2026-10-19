using UnityEngine;
using System;

public class MedKit : MonoBehaviour
{
    public event Action<MedKit> Collected;

    [SerializeField] private int _healthRestoration;
    public int HealthRestoration => _healthRestoration;

    public void Collect()
    {
        Collected?.Invoke(this);
    }

    private void OnDestroy()
    {
        Collected = null;
    }
}