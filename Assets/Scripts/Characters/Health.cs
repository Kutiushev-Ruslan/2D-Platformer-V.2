using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;

    private int _currentHealth;
    public bool IsDead => _currentHealth <= 0;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (IsDead)
            return;

        _currentHealth = Mathf.Max(0, _currentHealth - damage);
        Debug.Log($"HP: {_currentHealth}/{_maxHealth}");

        if (IsDead)
            Die();
    }

    public void Heal(int amount)
    {
        _currentHealth = Mathf.Min(_maxHealth, _currentHealth + amount);
        Debug.Log($"HP: {_currentHealth}/{_maxHealth}");
    }

    protected virtual void Die()
    {
        Debug.Log("Мертв");
        Destroy(gameObject);
    }
}