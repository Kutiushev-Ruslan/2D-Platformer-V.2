using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5.5f;
    [SerializeField] private float _jumpForce = 14f;
    [SerializeField] private PlayerInput _input;
    [SerializeField] private SpriteFlipper _spriteFlipper;
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private PlayerAnimatorController _animatorController;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleMovement();
        HandleJump();
        UpdateAnimations();
    }

    private void HandleMovement()
    {
        Vector2 velocity = _rigidbody.velocity;
        velocity.x = _input.Horizontal * _moveSpeed;
        _rigidbody.velocity = velocity;

        if (_input.Horizontal != 0)
        {
            _spriteFlipper.FlipTowardsDirection(new Vector2(_input.Horizontal, 0));
        }
    }

    private void HandleJump()
    {
        if (_input.JumpPressed && _groundDetector.IsGrounded)
        {
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }

    private void UpdateAnimations()
    {
        _animatorController.UpdateAnimations(_rigidbody.velocity.x);
    }
}