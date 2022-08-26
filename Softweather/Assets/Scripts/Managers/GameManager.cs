using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _spawner;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _player;

    private void Awake()
    {
        EventManager.OnGameOver.AddListener(GameOver);
    }

    private void Start()
    {
        EventManager.OnEnemyDead.AddListener(EnemyDead);
        SpawnStartEnemy();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) EventManager.OnGameOver.Invoke();
    }

    private void EnemyDead() => SpawnEnemy(2);
    
    private void SpawnStartEnemy() => SpawnEnemy(1);

    private void SpawnEnemy(int rangeArr)
    {
        for (var i = 0; i < rangeArr; i++)
        {
            var x = Random.Range(-22, 22);
            var z = Random.Range(-22, 22);
            var vector3 = new Vector3(x, 0, z);
            var enemy = Instantiate(_enemyPrefab, vector3, Quaternion.identity, _spawner.transform);
            enemy.GetComponent<Enemy>().Init(_player);
        }
    }
    
    private void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}