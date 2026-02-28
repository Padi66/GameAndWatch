using UnityEngine;
using System.Collections;

public enum FallingObjectType
{
    Bad,
    Good,
    Bonus
}

public class FallingObject : MonoBehaviour
{
    [Header("Object Settings")]
    [SerializeField] private FallingObjectType _objectType = FallingObjectType.Bad;
    [SerializeField] private int _damage = 1;
    [SerializeField] private int _scoreValue = 10;
    [SerializeField] private float _autoDestroyDelay = 6f;

    private Transform[] _movementPath;
    private int _currentIndex = 0;
    private TimeManager _timeManager;

    public void Init(Transform[] path, TimeManager timeManager)
    {
        _movementPath = path;
        _timeManager = timeManager;
        _currentIndex = 0;

        if (_movementPath != null && _movementPath.Length > 0)
        {
            transform.position = _movementPath[0].position;
        }

        if (_timeManager != null)
        {
            _timeManager.OnTimePassed += MoveToNextPosition;
        }

        StartCoroutine(AutoDestroyAfterDelay());
    }

    private void MoveToNextPosition()
    {
        if (_movementPath == null || _movementPath.Length == 0)
            return;

        _currentIndex++;

        if (_currentIndex < _movementPath.Length)
        {
            transform.position = _movementPath[_currentIndex].position;
        }
        else
        {
            DestroyObject();
        }
    }

    private IEnumerator AutoDestroyAfterDelay()
    {
        yield return new WaitForSeconds(_autoDestroyDelay);
        
        if (gameObject != null)
        {
            DestroyObject();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();

            switch (_objectType)
            {
                case FallingObjectType.Bad:
                    if (playerHealth != null)
                    {
                        playerHealth.TakeDamage(_damage);
                    }
                    break;

                case FallingObjectType.Good:
                    if (scoreManager != null)
                    {
                        scoreManager.AddScore(_scoreValue);
                    }
                    break;

                case FallingObjectType.Bonus:
                    if (scoreManager != null)
                    {
                        scoreManager.AddScore(_scoreValue);
                    }
                    if (playerHealth != null)
                    {
                        playerHealth.Heal(1);
                    }
                    break;
            }

            DestroyObject();
        }
    }

    private void DestroyObject()
    {
        if (_timeManager != null)
        {
            _timeManager.OnTimePassed -= MoveToNextPosition;
        }

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (_timeManager != null)
        {
            _timeManager.OnTimePassed -= MoveToNextPosition;
        }
    }
}
