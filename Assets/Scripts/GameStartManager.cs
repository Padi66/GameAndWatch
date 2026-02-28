using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _startPanel;
    [SerializeField] private TimeManager _timeManager;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private AudioEventDispatcher _audioEventDispatcher;

    private void Start()
    {
        _startPanel.SetActive(true);
        _spawner.enabled = false;
        _audioEventDispatcher?.PlayAudio(AudioType.BackgroundMusic);
    }

    /// <summary>Lance le jeu, cache le panel et joue le son de démarrage.</summary>
    public void StartGame()
    {
        _startPanel.SetActive(false);
        _spawner.enabled = true;
        _timeManager.StartTime();
        _audioEventDispatcher?.PlayAudio(AudioType.GameStart);
    }
}