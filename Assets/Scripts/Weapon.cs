using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    private Transform _firePoint;
    private float _fireDelay = 0.2f;
    private float _firingTimer;
    private void Start()
    {
         _firePoint = GetComponent<Transform>();
    }

    private void Update() // можно ли сделать через корутину?
    {
        if (_firingTimer > 0)
        {
            _firingTimer -= Time.deltaTime;
        }
        if (InputManager.instance.LeftMousePressed())
        {
            if (_firingTimer <= 0)
            {
                Shoot();
                _firingTimer += _fireDelay;
            }
        }
    }

    private void Shoot()
    {
        Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
    }
}
