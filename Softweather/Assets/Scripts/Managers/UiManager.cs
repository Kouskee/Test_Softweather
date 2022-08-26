using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _pointsTxt;
    [SerializeField] private TMP_Text _healthTxt;
    
    private float _points;
    private ISaveSystem _saveSystem;

    private void Awake()
    {
        EventManager.OnEnemyHit.AddListener(points =>
        {
            _points += points;
            _pointsTxt.text = _points.ToString();
        });
        
        EventManager.OnPlayerHit.AddListener(health =>
        {
            _healthTxt.text = health.ToString();
        });
        
        EventManager.OnGameOver.AddListener(GameOver);
    }

    private void GameOver()
    {
        _saveSystem = new JsonSaveSystem();
        var bestData = _saveSystem.Load();
        if (bestData.Points >= _points) return;
        
        var myData = new SaveData() { Points = _points};
        _saveSystem.Save(myData);

    }
}
