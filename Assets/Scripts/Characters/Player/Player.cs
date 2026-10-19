using UnityEngine;

[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    [SerializeField] private Flipper _flipper;
    [SerializeField] private PlayerInput _input;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private int _playerDamage = 10;

    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void FixedUpdate()
    {
        if (_input.Direction != 0)
            _playerMover.Move(_input.Direction);

        if (_input.GetIsJump() && _groundDetector.IsGround)
        {
            _playerMover.Jump();
        }

        _flipper.FlipTowardsDirection(_input.Direction);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out var enemy))
        {
            foreach (var contact in collision.contacts)
            {
                if (contact.normal.y > 0.7f && _playerMover.CurrentVerticalSpeed < 0)
                {
                    if (enemy.TryGetComponent<Health>(out var enemyHealth))
                    {
                        enemyHealth.TakeDamage(_playerDamage);
                        _playerMover.Bounce();

                        break;
                    }
                }
            }
        }
    }
}