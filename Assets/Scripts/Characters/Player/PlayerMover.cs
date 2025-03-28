using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5.5f;
    [SerializeField] private float _jumpForce = 14f;

    private Rigidbody2D _rigidbody;

    public float CurrentSpeed => _rigidbody.velocity.x;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Jump()
    {
        _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }

    public void Move(float direction)
    {
        Vector2 velocity = _rigidbody.velocity;
        velocity.x = direction * _moveSpeed;
        _rigidbody.velocity = velocity;
    }
}