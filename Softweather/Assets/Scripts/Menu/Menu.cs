using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject _menuCanvas;
    [SerializeField] private GameObject _scoreCanvas;
    [SerializeField] private TMP_Text _score;

    private ISaveSystem _saveSystem;

    private void Start()
    {
        Cursor.visible = true;
    }

    #region Menu

    public void StartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    public void SwitchSound(float switchSound) => AudioListener.volume = switchSound;
    
    public void CheckBestScore()
    {
        _saveSystem = new JsonSaveSystem();
        var data = _saveSystem.Load();
        _score.text = "Лучший счёт: " + data.Points;
        EnableMenu(false);
    }

    public void ExitGame() => Application.Quit();

    #endregion

    #region Score

    public void BackToMenu() => EnableMenu(true);

    #endregion

    private void EnableMenu(bool enable)
    {
        _menuCanvas.SetActive(enable);
        _scoreCanvas.SetActive(!enable);
    }
}
