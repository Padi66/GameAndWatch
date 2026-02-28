using UnityEngine;
using System;
using System.Collections.Generic;

[System.Serializable]
public class ScoreMilestone
{
    public int scoreThreshold;
    public string message;
}

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int _currentScore = 0;
    [SerializeField] private List<ScoreMilestone> _milestones = new List<ScoreMilestone>();
    [SerializeField] private AudioEventDispatcher _audioEventDispatcher; // 🔊 AJOUT

    public event Action<int> OnScoreChanged;
    public event Action<string> OnMilestoneReached;

    public int CurrentScore => _currentScore;

    private readonly HashSet<int> _triggeredMilestones = new HashSet<int>();

    private void Start()
    {
        _currentScore = 0;
        OnScoreChanged?.Invoke(_currentScore);
    }
    
    public void AddScore(int points)
    {
        _currentScore += points;
        Debug.Log($"Score ajouté: +{points}. Score total: {_currentScore}");
        _audioEventDispatcher?.PlayAudio(AudioType.TouchObject);

        OnScoreChanged?.Invoke(_currentScore);
        CheckMilestones();
    }
    
    public void ResetScore()
    {
        _currentScore = 0;
        _triggeredMilestones.Clear();
        OnScoreChanged?.Invoke(_currentScore);
    }

    private void CheckMilestones()
    {
        foreach (ScoreMilestone milestone in _milestones)
        {
            if (_currentScore >= milestone.scoreThreshold && !_triggeredMilestones.Contains(milestone.scoreThreshold))
            {
                _triggeredMilestones.Add(milestone.scoreThreshold);
                

                OnMilestoneReached?.Invoke(milestone.message);
            }
        }
    }
}
