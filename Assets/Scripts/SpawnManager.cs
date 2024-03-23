using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance = null;

    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private CinemachineVirtualCamera _cameraPrefab;
    [SerializeField] private Vector2 _spawnPoint;
    [SerializeField] private float _spawnDelay = 2f;
    [SerializeField] private float _spawnTimer = 0f;
    private GameObject _playerOnLevel;
    private CinemachineVirtualCamera _activeCamera;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        SpawnPlayer();
    }

    private void Update()
    {
        if (_spawnTimer > 0f)
        {
            _spawnTimer -= Time.deltaTime;
        }
        if (_spawnTimer <= 0f)
        {
            SpawnEnemy();
            _spawnTimer += _spawnDelay;
        }         
    }

    private void SpawnPlayer()
    {
        Vector2 mapCenter = LevelManager.instance.GroundInstance.transform.position;
        _playerOnLevel = Instantiate(_playerPrefab, mapCenter, Quaternion.identity);
        CinemachineVirtualCamera _activeCamera = Instantiate(_cameraPrefab, _playerPrefab.transform.position, Quaternion.identity);
        _activeCamera.m_Follow = _playerOnLevel.transform;
    }

    private void SpawnEnemy()
    { 
        _spawnPoint = RandomSpawnPosition();
        if (Vector2.Distance(_spawnPoint, _playerOnLevel.transform.position) > 10)
         {
               Instantiate(_enemyPrefab, _spawnPoint, Quaternion.identity);   // можно ли в else повторить этот же метод, если условие не сработает
         }
    }

    public Vector2 RandomSpawnPosition() // можно ли сделать общий метод с частным случаем удаления из листа
    {
        int randomIndex = Random.Range(0, LevelManager.instance.availableGridPositions.Count);
        Vector2 randomPosition = LevelManager.instance.availableGridPositions[randomIndex];
        return randomPosition;
    }
}
