using UnityEngine;
using TMPro;

public class ScoreMilestoneUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private GameObject _milestonePanel;
    [SerializeField] private TextMeshProUGUI _milestoneText;
    [SerializeField] private TimeManager _timeManager;
    [SerializeField] private Spawner _spawner;

    private void Start()
    {
        if (_scoreManager == null)
            _scoreManager = FindObjectOfType<ScoreManager>();

        _scoreManager.OnMilestoneReached += ShowMilestonePanel;
        _milestonePanel.SetActive(false);
    }

    private void OnDestroy()
    {
        if (_scoreManager != null)
            _scoreManager.OnMilestoneReached -= ShowMilestonePanel;
    }

    private void ShowMilestonePanel(string message)
    {
        _milestonePanel.SetActive(true);
        _milestoneText.text = message;

        _timeManager.StopTime();
        _spawner.enabled = false;
    }

    /// <summary>Ferme le panel et reprend le jeu.</summary>
    public void CloseMilestonePanel()
    {
        _milestonePanel.SetActive(false);
        _timeManager.StartTime();
        _spawner.enabled = true;
    }
}