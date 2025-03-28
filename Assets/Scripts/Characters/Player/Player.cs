using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private SpriteFlipper _spriteFlipper;
    [SerializeField] private PlayerInput _input;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private GroundDetector _groundDetector;

    private void FixedUpdate()
    {
        if (_input.Direction != 0)
            _playerMover.Move(_input.Direction);

        if (_input.GetIsJump() && _groundDetector.IsGround)
        {
            _playerMover.Jump();
        }

        _spriteFlipper.FlipTowardsDirection(_input.Direction);
    }
}
