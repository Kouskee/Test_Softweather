using UnityEngine;

public class TakingDamage : MonoBehaviour
{
    [SerializeField] private float _amountOfDamage;
    [SerializeField] private float _points;

    private IDamageController _controller;

    private void Awake()
    {
        _controller = GetComponentInParent<IDamageController>();
    }
    
    public void DealDamage() => _controller.DealDamage(_amountOfDamage, _points);
    
}
