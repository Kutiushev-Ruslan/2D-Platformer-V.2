using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _waypointThreshold = 0.1f;
    [SerializeField] private Flipper _flipper;

    private int _currentWaypointIndex = 0;
    private float _sqrThreshold;

    private void Awake()
    {
        _sqrThreshold = _waypointThreshold * _waypointThreshold;
    }

    private void FixedUpdate()
    {
        if (_waypoints.Length == 0)
            return;

        Transform target = _waypoints[_currentWaypointIndex];
        Vector2 direction = (target.position - transform.position).normalized;

        transform.position = Vector2.MoveTowards(
            transform.position,
            target.position,
            _moveSpeed * Time.deltaTime
        );

        _flipper.FlipTowardsDirection(direction.x);

        if ((transform.position - target.position).sqrMagnitude < _sqrThreshold)
        {
            _currentWaypointIndex = ++_currentWaypointIndex % _waypoints.Length;
        }
    }
}