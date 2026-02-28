using UnityEngine;
using TMPro;

public class VieUI : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private string _prefix = "Vie: ";
    [SerializeField] private bool _showMaxHealth = true;

    private void Start()
    {
        if (_playerHealth == null)
        {
            _playerHealth = FindObjectOfType<PlayerHealth>();
        }

        if (_playerHealth != null)
        {
            _playerHealth.OnHealthChanged += UpdateHealthDisplay;
            UpdateHealthDisplay(_playerHealth.CurrentHealth);
        }
        else
        {
            Debug.LogWarning("PlayerHealth non trouvé !");
        }
    }

    private void OnDestroy()
    {
        if (_playerHealth != null)
        {
            _playerHealth.OnHealthChanged -= UpdateHealthDisplay;
        }
    }

    private void UpdateHealthDisplay(int health)
    {
        if (_healthText != null)
        {
            if (_showMaxHealth)
            {
                _healthText.text = _prefix + health.ToString() + "/" + _playerHealth.MaxHealth.ToString();
            }
            else
            {
                _healthText.text = _prefix + health.ToString();
            }
        }
    }
}