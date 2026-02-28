using UnityEngine;
using System.Collections;
using System;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private float _timeStepDuration = 5.0f;
    [SerializeField] private float _minTimeStepDuration = 0.5f;
    [SerializeField] private bool _autoStart = false;

    public event Action OnTimePassed;
    public float CurrentTimeStep => _timeStepDuration;

    private Coroutine _timeCoroutine;

    private void Start()
    {
        if (_autoStart)
        {
            StartTime();
        }
    }

    public void StartTime()
    {
        if (_timeCoroutine != null)
        {
            StopCoroutine(_timeCoroutine);
        }

        _timeCoroutine = StartCoroutine(SpendingTime());
    }

    public void StopTime()
    {
        if (_timeCoroutine != null)
        {
            StopCoroutine(_timeCoroutine);
            _timeCoroutine = null;
        }
    }

    /// <summary>Applique un nouveau délai entre chaque tick et redémarre le timer.</summary>
    public void SetTimeStep(float newDuration)
    {
        _timeStepDuration = Mathf.Max(newDuration, _minTimeStepDuration);
        StartTime();
    }

    private IEnumerator SpendingTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timeStepDuration);
            OnTimePassed?.Invoke();
        }
    }
}