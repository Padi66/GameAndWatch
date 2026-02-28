using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 3;
    [SerializeField] private string _deathSceneName = "Jeu1Dead";
    [SerializeField] private AudioEventDispatcher _audioEventDispatcher;
    private int _currentHealth;

    public event Action<int> OnHealthChanged;
    public event Action OnDeath;

    public int CurrentHealth => _currentHealth;
    public int MaxHealth => _maxHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
        OnHealthChanged?.Invoke(_currentHealth);
    }

    /// <summary>Inflige des dégâts au joueur.</summary>
    public void TakeDamage(int damage)
    {
        int previousHealth = _currentHealth;
        _currentHealth -= damage;
        _currentHealth = Mathf.Max(_currentHealth, 0);

        _audioEventDispatcher?.PlayAudio(AudioType.TouchObject);
        OnHealthChanged?.Invoke(_currentHealth);

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>Soigne le joueur du montant spécifié.</summary>
    public void Heal(int amount)
    {
        _currentHealth += amount;
        _currentHealth = Mathf.Min(_currentHealth, _maxHealth);
        OnHealthChanged?.Invoke(_currentHealth);
    }

    private void Die()
    {
        _audioEventDispatcher?.PlayAudio(AudioType.Death);
        OnDeath?.Invoke();
        SceneManager.LoadScene(_deathSceneName);
    }
}