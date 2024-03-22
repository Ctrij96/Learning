using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance = null;

    private PlayerInput _playerInput;
 
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        _playerInput = new PlayerInput();
        _playerInput.Enable();
    }

    public Vector2 GetMovementVector()
    {
        Vector2 inputVector = _playerInput.Player.Move.ReadValue<Vector2>();

        return inputVector;
    }
    public Vector2 GetMousePosition()
    {
        Vector2 mousePos = _playerInput.Player.Look.ReadValue<Vector2>();

        return mousePos;
    }
    
    public bool LeftMousePressed()
    {
        bool leftMousePressed = _playerInput.Player.Fire.IsPressed();

        return leftMousePressed;
    }
}
