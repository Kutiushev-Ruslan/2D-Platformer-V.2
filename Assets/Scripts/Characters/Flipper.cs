using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Flipper : MonoBehaviour
{
    private bool _isFacingRight = true;
    private Quaternion _facingRightRotation;
    private Quaternion _facingLeftRotation;

    private void Awake()
    {
        _facingRightRotation = transform.rotation;

        _facingLeftRotation = _facingRightRotation * Quaternion.Euler(0, 180, 0);
    }

    public void FlipTowardsDirection(float direction)
    {
        if (direction > 0 && !_isFacingRight)
        {
            Flip();
        }
        else if (direction < 0 && _isFacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        transform.rotation = _isFacingRight ? _facingRightRotation : _facingLeftRotation;
    }
}