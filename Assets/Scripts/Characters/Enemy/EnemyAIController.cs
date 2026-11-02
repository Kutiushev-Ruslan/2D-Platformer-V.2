using UnityEngine;

public class EnemyAIController : MonoBehaviour
{
    [SerializeField] private float _viewRadius = 5f;
    [SerializeField] private float _viewAngle = 90f;
    [SerializeField] private Transform _viewPoint;
    [SerializeField] private LayerMask _obstacleMask;

    private Transform _playerTransform;
    private bool _isPlayerVisible;

    public bool IsPlayerVisible => _isPlayerVisible;
    public Transform PlayerTransform => _playerTransform;

    private void Start()
    {
        _playerTransform = FindObjectOfType<Player>().transform;

        if (_viewPoint == null)
        {
            _viewPoint = transform;
        }
    }

    private void Update()
    {
        _isPlayerVisible = CheckPlayerVisibility();
    }

    private bool CheckPlayerVisibility()
    {
        if (_playerTransform == null)
            return false;

        Vector2 directionToPlayer = (_playerTransform.position - _viewPoint.position).normalized;
        float distanceToPlayer = Vector2.Distance(_viewPoint.position, _playerTransform.position);

        if (distanceToPlayer > _viewRadius)
            return false;

        float angleToPlayer = Vector2.Angle(_viewPoint.right, directionToPlayer);

        if (angleToPlayer > _viewAngle * 0.5f)
            return false;

        RaycastHit2D hit = Physics2D.Raycast(_viewPoint.position, directionToPlayer, distanceToPlayer, _obstacleMask);

        return hit.collider == null;
    }

    private void OnDrawGizmosSelected()
    {
        if (_viewPoint == null)
            return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_viewPoint.position, _viewRadius);

        Vector3 rightDir = _viewPoint.TransformDirection(Quaternion.Euler(0, 0, _viewAngle * 0.5f) * Vector3.right) * _viewRadius;
        Vector3 leftDir = _viewPoint.TransformDirection(Quaternion.Euler(0, 0, -_viewAngle * 0.5f) * Vector3.right) * _viewRadius;

        Gizmos.DrawLine(_viewPoint.position, _viewPoint.position + rightDir);
        Gizmos.DrawLine(_viewPoint.position, _viewPoint.position + leftDir);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(_viewPoint.position, _viewPoint.position + _viewPoint.right * _viewRadius);
    }
}