using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.AI;
using NavMeshPlus.Extensions;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance = null;

    public GameObject GroundInstance {  get; private set; }
    public List<Vector2> availableGridPositions = new();

    [SerializeField] private int _height = 100;
    [SerializeField] private int _width = 100;
    [SerializeField] private int _wallBaseCount;
    [SerializeField] private GameObject _wallPrefab;
    [SerializeField] private GameObject _groundPrefab;
    private NavMeshSurface _surface;

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        _surface = GetComponent<NavMeshSurface> ();
        InitialiseList();
        SetupMap();
    }

    private void Start()
    {
        _surface.BuildNavMesh(); // почему не работает если сунуть в Awake
    }
    private void InitialiseList()
    {
        availableGridPositions.Clear();

        for (int x = 1; x < _height - 1; x++)
        {
            for (int y = 1; y < _width - 1; y++)
            {
                availableGridPositions.Add(new Vector2(x, y));
            }
        }
    }
    private void SetupMap()
    {
        GameObject map = new ("Map");
        CreateGround(_width, _height, map);
        CreateBorders(map);
        CreateObstacles(_wallBaseCount, map);
    }
    
    private void CreateGround(int heigth, int width, GameObject map)
    {
        GroundInstance = Instantiate(_groundPrefab, new Vector2( width / 2, heigth / 2), Quaternion.identity, map.transform);
        GroundInstance.transform.localScale = new Vector2(heigth, width);
    }
    private void CreateBorders(GameObject parent)
    {
        GameObject borders = new ("Borders");
        borders.transform.SetParent(parent.transform, true);
        for (float x = 0f; x <= _height; x++)
        {
            for (float y = 0f; y <= _width; y++)
            {
                if (x == 0 || x == _width || y == 0 || y == _height)
                {
                    Create(_wallPrefab, new Vector2(x, y), borders);
                }
            }
        }
    }

    private void CreateObstacles(int Count, GameObject parent)
    {
        for (int i = 0; i < Count; i++)
        {
            Create(_wallPrefab, RandomPosition(), parent);
        }
    }
    private void Create(GameObject prefab, Vector2 position, GameObject parent)
    {
        GameObject clone = Instantiate(prefab, position, Quaternion.identity);
        clone.transform.SetParent(parent.transform, true);
    }

    public Vector2 RandomPosition()
    {
        int randomIndex = Random.Range(0, availableGridPositions.Count);
        Vector2 randomPosition = availableGridPositions[randomIndex];
        availableGridPositions.RemoveAt(randomIndex);  // как сделать метод общим с частным случаем удаления из листа
        return randomPosition;
    }
}