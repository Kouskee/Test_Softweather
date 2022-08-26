using UnityEngine.Events;

public class EventManager
{
    public static readonly UnityEvent OnGameOver = new UnityEvent();
    public static readonly UnityEvent<float> OnEnemyHit = new UnityEvent<float>();
    public static readonly UnityEvent OnEnemyDead = new UnityEvent();
    public static readonly UnityEvent<float> OnPlayerHit = new UnityEvent<float>();
}
