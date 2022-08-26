using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Rigidbody _bullet;
    [SerializeField] private float _bulletVelocity;

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        
        var bullet = Instantiate(_bullet, transform.position, transform.rotation);
        bullet.velocity = transform.forward * _bulletVelocity;
    }
}
