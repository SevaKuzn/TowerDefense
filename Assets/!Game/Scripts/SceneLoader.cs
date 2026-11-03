using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private int _changeSceneCooldown;

    private string _gameScene = "Game";
    private string _mainMenuScene = "MainMenu";

    private bool _isBackgroundMain = false;
    private bool _isBackgroundGame = false;

    private bool _isSceneMain = false;
    private bool _isSceneGame = false;

    private Color backgroundColor;
    private Scene currentScene;

    private void Start()
    {
        backgroundColor = _backgroundImage.color;
        currentScene = SceneManager.GetActiveScene();
    }

    void Update()
    {
        Debug.Log(currentScene.name);
        if (_isBackgroundMain)
        {
            _backgroundImage.enabled = true;
            backgroundColor.a += 1f / _changeSceneCooldown * Time.deltaTime;
            _backgroundImage.color = backgroundColor;
        }
        if (_isBackgroundGame)
        {
            _backgroundImage.enabled = true;
            backgroundColor.a -= 1f / _changeSceneCooldown * Time.deltaTime;
            _backgroundImage.color = backgroundColor;
        }
    }

    public void GameScene(string sceneName)
    {
        StartCoroutine(ChangeScene(sceneName));
    }

    IEnumerator ChangeScene(string sceneName)
    {
       _isBackgroundMain = true;
       _isBackgroundGame = false;
       yield return new WaitForSeconds(_changeSceneCooldown);
       SceneManager.LoadScene(1);
    }
}
