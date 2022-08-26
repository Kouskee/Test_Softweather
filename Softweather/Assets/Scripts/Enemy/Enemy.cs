using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageController
{
    [SerializeField] private float _health;
    [Space] 
    [SerializeField] private float _delayBeforeHit;

    private NavMeshAgent _agent;
    private Player _player;
    private Transform _playerTransform;

    private float _timeLeft;

    public void Init(GameObject player)
    {
        _player = player.GetComponent<Player>();
        _playerTransform = player.transform;
    }

    private void Awake()
    {
        TryGetComponent(out _agent);
    }

    private void Update()
    {
        if(_player == null) return;
        
        _agent.SetDestination(_playerTransform.position);
        var distance = Vector3.Distance(_playerTransform.position, transform.position);

        if (_timeLeft > 0)
            _timeLeft -= Time.deltaTime;
        else
        {
            if (distance > 1.3f) return;
            _timeLeft = _delayBeforeHit;
            _player.DealDamage();
        }
    }

    public void DealDamage(float damage, float points)
    {
        _health = Mathf.Clamp(_health - damage, 0, 100);

        if (_health == 0)
        {
            EventManager.OnEnemyDead.Invoke();
            Destroy(gameObject);
        }

        EventManager.OnEnemyHit.Invoke(points);
    }
}