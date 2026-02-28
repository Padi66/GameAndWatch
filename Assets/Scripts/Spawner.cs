using UnityEngine;

[System.Serializable]
public class SpawnableObject
{
    public GameObject prefab;
    [Range(0f, 100f)]
    public float spawnChance = 50f;
}

public class Spawner : MonoBehaviour
{
    [SerializeField] private TimeManager _timeManager;
    [SerializeField] private SpawnableObject[] _spawnableObjects;
    [SerializeField] private ObjectMovement[] _fallingLines;

    [SerializeField] private int _spawnTimer = 0;
    [SerializeField] private int _spawnDelayDuration = 3;

    public int SpawnDelayDuration
    {
        get => _spawnDelayDuration;
        set => _spawnDelayDuration = Mathf.Max(1, value);
    }

    private void OnEnable()
    {
        if (_timeManager != null)
        {
            _timeManager.OnTimePassed += TimeGestion;
        }
    }

    private void OnDisable()
    {
        if (_timeManager != null)
        {
            _timeManager.OnTimePassed -= TimeGestion;
        }
    }

    private void TimeGestion()
    {
        _spawnTimer++;

        if (_spawnTimer >= _spawnDelayDuration)
        {
            _spawnTimer = 0;

            int randomLineIndex = Random.Range(0, _fallingLines.Length);
            GameObject objectToSpawn = SelectRandomObject();

            if (objectToSpawn != null)
            {
                GameObject spawnedObject = Instantiate(objectToSpawn);
                _fallingLines[randomLineIndex].Init(spawnedObject);
            }
        }
    }

    private GameObject SelectRandomObject()
    {
        if (_spawnableObjects.Length == 0)
            return null;

        float totalChance = 0f;
        foreach (var obj in _spawnableObjects)
        {
            totalChance += obj.spawnChance;
        }

        float randomValue = Random.Range(0f, totalChance);
        float currentChance = 0f;

        foreach (var obj in _spawnableObjects)
        {
            currentChance += obj.spawnChance;
            if (randomValue <= currentChance)
            {
                return obj.prefab;
            }
        }

        return _spawnableObjects[0].prefab;
    }
}
