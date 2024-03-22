using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D _rb2d;
    private float _speed = 20f;
    private int _bulletDamage = 5;

    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _rb2d.velocity = transform.up * _speed;

    }
    private void OnCollisionEnter2D(Collision2D target)
    { 
        EnemyController enemy = target.gameObject.GetComponent<EnemyController>(); // можно ли сделать интерфейс или родительский класс, чтобы колизия могла вызвать другой эффект
        if (enemy != null )
        {
            enemy.Damaged(_bulletDamage);
        }
        Destroy(gameObject);
    }
}
