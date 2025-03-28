using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] private PlayerMover _playerMover;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        UpdateAnimations();
    }

    private void UpdateAnimations()
    {
        if (_animator != null && _playerMover != null)
        {
            _animator.SetFloat(PlayerAnimatorData.Params.Speed, Mathf.Abs(_playerMover.CurrentSpeed));
        }
    }
}