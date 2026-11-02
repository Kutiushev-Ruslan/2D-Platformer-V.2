using UnityEngine;

[RequireComponent(typeof(EnemyAIController), typeof(EnemyMover))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _chaseDuration = 3f;
    [SerializeField] private float _attackRange = 2.5f;
    [SerializeField] private int _attackDamage = 10;
    [SerializeField] private float _attackCooldown = 1f;
    [SerializeField] private float _attackPreparationTime = 0.7f;

    private float _nextAttackTime;
    private float _attackReadyTime;
    private bool _isPreparingAttack;
    private EnemyAIController _aiController;
    private EnemyMover _mover;
    private float _lastSeenTime = 0f;
    private bool _wasPlayerVisible;

    private void Awake()
    {
        _nextAttackTime = Time.time;
        _aiController = GetComponent<EnemyAIController>();
        _mover = GetComponent<EnemyMover>();
    }

    private void Update()
    {
        bool currentlySeeingPlayer = _aiController.IsPlayerVisible;

        Transform player = _aiController.PlayerTransform;

        if (player == null)
        {
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (_isPreparingAttack && (currentlySeeingPlayer == false || distanceToPlayer > _attackRange))
        {
            InterruptAttack();
        }

        if (currentlySeeingPlayer && distanceToPlayer <= _attackRange)
        {
            _lastSeenTime = Time.time;
            _mover.StopMoving();

            if (Time.time >= _nextAttackTime && !_isPreparingAttack)
            {
                _isPreparingAttack = true;
                _attackReadyTime = Time.time + _attackPreparationTime;
            }
            else if (_isPreparingAttack)
            {
                if (Time.time >= _attackReadyTime)
                {
                    if (player.TryGetComponent<Health>(out var playerHealth))
                    {
                        playerHealth.TakeDamage(_attackDamage);
                        _nextAttackTime = Time.time + _attackCooldown;
                        _isPreparingAttack = false;
                    }
                }
            }
        }
        else if (currentlySeeingPlayer)
        {
            _lastSeenTime = Time.time;
            _mover.ChasePlayer(player.position);
        }
        else if (Time.time - _lastSeenTime < _chaseDuration && _lastSeenTime != 0f)
        {
            _mover.ChasePlayer(player.position);
        }
        else if (_wasPlayerVisible)
        {
            _mover.ReturnToNearestWaypoint();
        }
        else
        {
            _mover.Patrol();
        }

        _wasPlayerVisible = currentlySeeingPlayer;
    }

    public void InterruptAttack()
    {
        if (_isPreparingAttack)
        {
            _isPreparingAttack = false;
            _nextAttackTime = Time.time + _attackCooldown;
        }
    }
}