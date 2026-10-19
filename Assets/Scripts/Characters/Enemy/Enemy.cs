using UnityEngine;

[RequireComponent(typeof(EnemyAIController), typeof(EnemyMover))]
public class Enemy : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _chaseDuration = 3f;

    private EnemyAIController _aiController;
    private EnemyMover _mover;
    private float _lastSeenTime = 0f;
    private bool _wasPlayerVisible;

    private void Awake()
    {
        _aiController = GetComponent<EnemyAIController>();
        _mover = GetComponent<EnemyMover>();
    }

    private void Update()
    {
        bool currentlySeeingPlayer = _aiController.IsPlayerVisible;

        if (currentlySeeingPlayer)
        {
            _lastSeenTime = Time.time;
            _mover.ChasePlayer(_aiController.PlayerTransform.position);
        }
        else if (Time.time - _lastSeenTime < _chaseDuration && _lastSeenTime != 0f)
        {
            _mover.ChasePlayer(_aiController.PlayerTransform.position);
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
}