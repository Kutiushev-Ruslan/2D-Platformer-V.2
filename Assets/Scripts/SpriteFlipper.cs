using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteFlipper : MonoBehaviour
{
    private bool _isFacingRight = true;

    public void FlipTowardsDirection(Vector2 moveDirection)
    {
        if (moveDirection.x > 0 && !_isFacingRight)
        {
            Flip();
        }
        else if (moveDirection.x < 0 && _isFacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        transform.Rotate(0, 180, 0);
    }
}