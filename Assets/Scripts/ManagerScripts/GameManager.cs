using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI menus")]
    [SerializeField] private GameObject _WinMenu;
    [SerializeField] private GameObject _GameOverMenu;
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private GameObject _SettingsButtonUI;
    [SerializeField] private Slingshot _slingshot;

    [Space]
    [SerializeField] private int _nextLevelIndex;

    [Header("sound settings")]
    [SerializeField] private AudioClip _winSound;
    [SerializeField] private AudioClip _gameOverSound;

    [Header("Birds queue")]
    [SerializeField] private Bird[] _birdsOnScene;
    private Queue<Bird> _birdsQueue = new Queue<Bird>();

    [Space]
    [SerializeField] private int _pigsCount;
    [SerializeField] private int _birdsCount;

    private AudioSource _audioSource;

    private bool _isGameWon = false;
    private bool _isGameLose = false;

    private void Awake()
    {
        EventManager.OnUpdatePigsCount.AddListener(UpdatePigsCount);
        EventManager.OnUpdateBirdsQueue.AddListener(UpdateBirdsQueue);
        AudioListener.volume = MusicSettings.MusicVolume;
        InitBirdsQueue();

        _audioSource = GetComponent<AudioSource>();
    }

    private void InitBirdsQueue()
    {
        foreach (Bird bird in _birdsOnScene)
        {
            _birdsQueue.Enqueue(bird);
        }
        _birdsCount = _birdsQueue.Count;


        _slingshot.CurrentBird = _birdsQueue.Peek();
        _slingshot.CurrentBird.gameObject.transform.position = _slingshot.CenterPosition.position;
    }

    private void UpdateBirdsQueue()
    {
        _slingshot.CurrentBird = null;
        _birdsQueue.Dequeue();
        _birdsCount--;

        if (_birdsQueue.Count == 0)
        {
            if (_isGameWon == false)
            {
                ShowGameOverMenu();
            }
            return;
        }

        _slingshot.CurrentBird = _birdsQueue.Peek();
        _slingshot.CurrentBird.gameObject.transform.position = _slingshot.CenterPosition.position;
    }

    private void UpdatePigsCount()
    {
        _pigsCount--;

        if (_pigsCount <= 0 && _isGameLose == false)
        {
            ShowWinMenu();
        }
    }

    public void ShowWinMenu()
    {
        _isGameWon = true;
        _WinMenu.SetActive(true);
        _GameOverMenu.SetActive(false);
        _SettingsButtonUI.SetActive(false);
        _slingshot.enabled = false;
        _audioSource.PlayOneShot(_winSound);
    }

    public void ShowGameOverMenu()
    {
        _isGameLose = true;
        _GameOverMenu.SetActive(true);
        _SettingsButtonUI.SetActive(false);
        _WinMenu.SetActive(false);
        _slingshot.enabled = false;
        _audioSource.PlayOneShot(_gameOverSound);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(_nextLevelIndex);
    }

    public void OpenSettingsMenu()
    {
        _settingsMenu.SetActive(true);
        _SettingsButtonUI.SetActive(false);
        _slingshot.enabled = false;
    }

    public void CloseSettingsMenu()
    {
        _settingsMenu.SetActive(false);
        _SettingsButtonUI.SetActive(true);
        _slingshot.enabled = true;
    }
}
