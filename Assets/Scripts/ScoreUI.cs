using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private string _prefix = "Score: ";

    private void Start()
    {
        if (_scoreManager == null)
        {
            _scoreManager = FindObjectOfType<ScoreManager>();
        }

        if (_scoreManager != null)
        {
            _scoreManager.OnScoreChanged += UpdateScoreDisplay;
            UpdateScoreDisplay(_scoreManager.CurrentScore);
        }
        else
        {
            Debug.LogWarning("ScoreManager non trouvé !");
        }
    }

    private void OnDestroy()
    {
        if (_scoreManager != null)
        {
            _scoreManager.OnScoreChanged -= UpdateScoreDisplay;
        }
    }

    private void UpdateScoreDisplay(int score)
    {
        if (_scoreText != null)
        {
            _scoreText.text = _prefix + score.ToString();
        }
    }
}