using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float _playerMaxHealth = 100f;
    [SerializeField] private float _playerHealth;
    [SerializeField] private float _speed = 5f;
    private float _offset = -90f;
    private float _recoveryTime = 0f;
    private PlayerUI _playerUI;
    private Rigidbody2D _rb2d;
    private Collider2D _collider2d;

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _collider2d = GetComponent<Collider2D>();
        _playerUI = GetComponentInChildren<PlayerUI>();
        _playerHealth = _playerMaxHealth;
    }

    private void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
        DamageRecoveryCheck();
    }

    private void MovePlayer()
    {
        Vector2 inputVector = InputManager.instance.GetMovementVector().normalized;
        _rb2d.MovePosition(_rb2d.position + inputVector * (_speed * Time.fixedDeltaTime));
    }

    private void RotatePlayer()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(InputManager.instance.GetMousePosition()) - transform.position;
        float rotateZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.Find("PlayerAxis").rotation = Quaternion.Euler(0, 0, rotateZ + _offset);
    }

    private void CheckDeath()
    {
        if (_playerHealth < 0)
        {
            Destroy(gameObject);
        }
    }

    private void DamageRecoveryCheck()
    {
        if (_recoveryTime > 0f)
        {
            _recoveryTime -= Time.fixedDeltaTime;
        }
    }

    private void OnCollisionStay2D(Collision2D hit)
    {
        EnemyController enemy = hit.gameObject.GetComponent<EnemyController>();
        if (enemy != null)
        {
            if (_recoveryTime <= 0)
            {
                _playerHealth -= enemy.EnemyDamage;
                _playerUI.UpdateHealthBar(_playerHealth,_playerMaxHealth);
                CheckDeath();
                _recoveryTime = 1f;
            }
        }
    }

}
