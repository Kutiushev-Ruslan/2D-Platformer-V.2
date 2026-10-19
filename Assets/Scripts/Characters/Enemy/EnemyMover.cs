using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _patrolSpeed = 2f;
    [SerializeField] private float _chaseSpeed = 4f;
    [SerializeField] private Transform[] _waypoints;

    private Rigidbody2D _rigidbody;
    private Flipper _flipper;
    private int _currentWaypointIndex;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _flipper = GetComponent<Flipper>();
    }

    public void ReturnToNearestWaypoint()
    {
        if (_waypoints.Length == 0) return;

        float minDistance = float.MaxValue;
        int nearestIndex = 0;

        for (int i = 0; i < _waypoints.Length; i++)
        {
            float distance = Vector2.Distance(transform.position, _waypoints[i].position);

            if (distance < minDistance)
            {
                minDistance = distance;
                nearestIndex = i;
            }
        }

        _currentWaypointIndex = nearestIndex;
        Patrol();
    }

    public void Patrol()
    {
        if (_waypoints.Length == 0) return;

        Vector2 direction = (_waypoints[_currentWaypointIndex].position - transform.position).normalized;
        _rigidbody.velocity = new Vector2(direction.x * _patrolSpeed, _rigidbody.velocity.y);
        _flipper.FlipTowardsDirection(direction.x);

        if (Vector2.Distance(transform.position, _waypoints[_currentWaypointIndex].position) < 0.5f)
        {
            _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
        }
    }

    public void ChasePlayer(Vector3 targetPosition)
    {
        Vector2 direction = (targetPosition - transform.position).normalized;
        _rigidbody.velocity = new Vector2(direction.x * _chaseSpeed, _rigidbody.velocity.y);
        _flipper.FlipTowardsDirection(direction.x);
        // когда дистанция подходящая атаковать
    }
}