using System;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private int _trapDamage = 10;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.rigidbody == null) return;

        if (collision.rigidbody.TryGetComponent<Health>(out var health))
        {
            health.TakeDamage(_trapDamage);
        }
    }
}

