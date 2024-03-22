using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public int EnemyDamage = 5;
    public static event Action<int> Death; // нужен ли static?

    private Transform _target;
    private int _health = 35;
    private CircleCollider2D _collider2d;
    private Rigidbody2D _rb2d;
    private NavMeshAgent _agent;
    private int _pointsForEnemy = 5;

    private void Awake()
    {
        _collider2d = GetComponent<CircleCollider2D>();
        _rb2d = GetComponent<Rigidbody2D>();
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        
    }

    private void FixedUpdate()
    {
        MovingToTarget();
    }

    private void MovingToTarget()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        _agent.SetDestination(_target.position);
    }

    public void Damaged(int damage)
    {
        _health -= damage;
        CheckDeath();
    }

    private void CheckDeath()
    {
        if (_health <= 0)
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Death?.Invoke(_pointsForEnemy);
    }
}
