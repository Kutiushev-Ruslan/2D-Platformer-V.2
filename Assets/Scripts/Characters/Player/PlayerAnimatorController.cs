using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimatorController : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetSpeed(float speed)
    {
        if (_animator != null)
        {
            _animator.SetFloat(PlayerAnimatorData.Params.Speed, speed);
        }
    }