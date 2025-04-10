using UnityEngine;
using System;

public class MedKit : MonoBehaviour
{
    public event Action<MedKit> Collected;

    public void Collect()
    {
        Collected?.Invoke(this);
    }

    private void OnDestroy()
    {
        Collected = null;
    }
}