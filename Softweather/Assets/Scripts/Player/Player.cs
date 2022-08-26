using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _health;

    private float partOfTheCake;

    private void Start() => partOfTheCake = _health / 4;

    public void DealDamage()
    {
        if (_health <= 0)
        {
            EventManager.OnGameOver.Invoke();
        }

        _health = Mathf.Clamp(_health - partOfTheCake, 0, 100);
        EventManager.OnPlayerHit.Invoke(_health);
    }
}