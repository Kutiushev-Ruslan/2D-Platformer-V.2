using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float Horizontal { get; private set; }
    public bool JumpPressed { get; private set; }

    private void Update()
    {
        Horizontal = Input.GetAxis("Horizontal");
        JumpPressed = Input.GetKeyDown(KeyCode.Space);
    }
}