using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private string _scene;
    [SerializeField] private int _changeSceneCooldown;
    private bool _isScene = false;
    private bool _isBackground = false;
    private Color backgroundColor;

    private void Start()
    {
        backgroundColor = _backgroundImage.color;
    }
    void Update()
    {
        if (_isScene)
        {
            SceneManager.LoadScene(_scene);
        }
        if (_isBackground)
        {
            _backgroundImage.enabled = true;
            backgroundColor.a += 1f / _changeSceneCooldown * Time.deltaTime;
            _backgroundImage.color = backgroundColor;
        }
    }

    public void GameScene()
    {
        StartCoroutine(ChangeScene());
    }

    IEnumerator ChangeScene()
    {
        _isBackground = true;
        yield return new WaitForSeconds(_changeSceneCooldown);
        _isScene = true;
    }
}
