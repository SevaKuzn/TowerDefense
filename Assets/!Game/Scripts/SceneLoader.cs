using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private float _changeSceneCooldown;

    private bool _isNextScene = false;
    private bool _isPerviousScene = false;
    private int currentSceneIndex;

    private Color backgroundColor;
    private Scene currentScene;

    private void Start()
    {
        backgroundColor = _backgroundImage.color;
        currentScene = SceneManager.GetActiveScene();
        StartCoroutine(StartChangeLoadScreen());
    }

    private void Update()
    {
        if (_isNextScene)
        {

            _backgroundImage.enabled = true;
            backgroundColor.a += 1f / _changeSceneCooldown * Time.deltaTime;
            _backgroundImage.color = backgroundColor;
            
        }
        if (_isPerviousScene)
        {

           _backgroundImage.enabled = true;
           backgroundColor.a -= 1f / _changeSceneCooldown * Time.deltaTime;
           _backgroundImage.color = backgroundColor;
            
        }
    }
    public void NextScene()
    {
        currentSceneIndex = currentScene.buildIndex;
        StartCoroutine(ChangeBackground(true));
    }

    public void PerviousScene()
    {
        currentSceneIndex = currentScene.buildIndex;
        StartCoroutine(ChangeBackground(false));
    }

    public void LeaveGame()
    {
        Application.Quit();
    }

    IEnumerator ChangeBackground(bool isChange)
    {
        if (isChange)
        {
            _isNextScene = true;
            yield return new WaitForSeconds(_changeSceneCooldown);
            _isNextScene = false;
            yield return new WaitForSeconds(0.1f);
            SceneManager.LoadScene(currentSceneIndex + 1);

        }
        if (!isChange)
        {
            _isNextScene = true;
            yield return new WaitForSeconds(_changeSceneCooldown);
            _isNextScene = false;
            yield return new WaitForSeconds(0.1f);
            if (currentSceneIndex != 0)
            {
                SceneManager.LoadScene(currentSceneIndex - 1);
            }
        }
    }
    IEnumerator StartChangeLoadScreen()
    {
        _isPerviousScene = true;
        yield return new WaitForSeconds(_changeSceneCooldown);
        _isPerviousScene = false;
        _backgroundImage.enabled = false;
    }
}
