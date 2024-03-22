using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoader : MonoBehaviour
{
    public GameObject game_manager;
    public GameObject level_manager;
    public GameObject spawn_manager;
    public GameObject input_manager;

    void Awake()
    {
        if (GameManager.instance == null)
            Instantiate(game_manager);

        if (LevelManager.instance == null)
            Instantiate(level_manager);

        if (SpawnManager.instance == null)
            Instantiate(spawn_manager);

        if (InputManager.instance == null)
            Instantiate(input_manager);
    }

}
