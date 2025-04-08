using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Flipper _flipper;
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

        _flipper.FlipTowardsDirection(_input.Direction);
    }
}
