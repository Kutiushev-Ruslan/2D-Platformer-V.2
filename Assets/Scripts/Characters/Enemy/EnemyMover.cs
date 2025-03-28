using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _waypointThreshold = 0.1f;
    [SerializeField] private SpriteFlipper _spriteFlipper;

    private int _currentWaypointIndex = 0;

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

        _spriteFlipper.FlipTowardsDirection(direction.x);

        if (Vector2.Distance(transform.position, target.position) < _waypointThreshold)
        {
            _currentWaypointIndex = _currentWaypointIndex++ % _waypoints.Length;
        }
    }
}