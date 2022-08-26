using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private LayerMask _layers;
    [SerializeField] private GameObject _effect;
    
    private void Awake()
    {
        StartCoroutine(TemporaryBullet());
    }

    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out var hit, 2, _layers))
            DealDamage(hit.collider);
    }

    private void DealDamage(Component other)
    {
        if (!other.TryGetComponent(out TakingDamage damageController)) return;

        Instantiate(_effect, transform.position, transform.rotation);
        damageController.DealDamage();
        Destroy(gameObject);
    }

    private IEnumerator TemporaryBullet()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
