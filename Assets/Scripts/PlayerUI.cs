using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Slider _sliderHP;
    [SerializeField] private Text _score;
    private int _scoreCount = 0;

    private void Awake()
    {
        EnemyController.Death += AddPoints;
    }
    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        _sliderHP.value = currentHealth / maxHealth;
    }

    public void AddPoints(int points)
    {
        _scoreCount += points;
    }

    public void Update()
    {
        _score.text = "Score:" + Mathf.Round(_scoreCount);
    }
}
