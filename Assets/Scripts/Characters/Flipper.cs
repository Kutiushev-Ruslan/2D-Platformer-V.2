using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Flipper : MonoBehaviour
{
    private bool _isFacingRight = true;

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

        transform.rotation = Quaternion.Euler(0f, _isFacingRight ? 0f : 180f, 0f);

        //Debug.Log($"Flipped to: {(_isFacingRight ? "right" : "left")}");
    }
}