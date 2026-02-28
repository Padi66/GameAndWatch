using UnityEngine;

public class DificultyManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TimeManager _timeManager;
    [SerializeField] private Spawner _spawner;

    [Header("Time Step Settings")]
    [SerializeField] private float _initialTimeStep = 5f;
    [SerializeField] private float _minTimeStep = 0.5f;
    [SerializeField] private float _timeStepDecreaseAmount = 0.2f;

    [Header("Spawn Delay Settings")]
    [SerializeField] private int _initialSpawnDelay = 3;
    [SerializeField] private int _minSpawnDelay = 1;

    [Header("Difficulty Ramp")]
    [SerializeField] private float _difficultyInterval = 10f;

    private float _difficultyTimer = 0f;
    private int _difficultyLevel = 0;

    private void Start()
    {
        _timeManager.SetTimeStep(_initialTimeStep);
        _spawner.SpawnDelayDuration = _initialSpawnDelay;
    }

    private void Update()
    {
        _difficultyTimer += Time.deltaTime;

        if (_difficultyTimer >= _difficultyInterval)
        {
            _difficultyTimer = 0f;
            IncreaseDifficulty();
        }
    }

    private void IncreaseDifficulty()
    {
        _difficultyLevel++;

        float newTimeStep = _initialTimeStep - (_timeStepDecreaseAmount * _difficultyLevel);
        _timeManager.SetTimeStep(newTimeStep);

        int newSpawnDelay = _initialSpawnDelay - (_difficultyLevel / 2);
        _spawner.SpawnDelayDuration = newSpawnDelay;

        Debug.Log($"Niveau de difficulté: {_difficultyLevel} | TimeStep: {_timeManager.CurrentTimeStep:F2}s | SpawnDelay: {_spawner.SpawnDelayDuration}");
    }
}