using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float velJuagador;

    public InputAction LeftAction;
    public InputAction MoveAction;
    public InputAction WASDAction;
    public InputAction GamePadAction;
    // Start is called before the first frame update
    void Start()
    {
        //LeftAction.Enable();
        MoveAction.Enable();
        WASDAction.Enable();
        GamePadAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        /*float horizontal = 0.0f;
        if (LeftAction.IsPressed())
        {
            horizontal = -1.0f;
        }
        else if (Keyboard.current.rightArrowKey.isPressed)
        {
            horizontal = 1.0f;
        }
        float vertical = 0.0f;
        if (Keyboard.current.downArrowKey.isPressed)
        {
            vertical = -1.0f;
        }
        else if (Keyboard.current.upArrowKey.isPressed)
        {
            vertical = 1.0f;
        }
        Debug.Log(horizontal);
        Vector2 position = transform.position;//Le otorga el movimiento en las coordenadas X e Y
        position.x = position.x + 0.1f*velJuagador*horizontal;//Al multiplicar por horizontal estas actualizando la posicion en base al moevimiento de las flechas
        position.y = position.y + 0.1f*velJuagador*vertical;//Con estas dos lineas se mueve en vertical a una velocidad reducida
        transform.position = position;*///Para cambiar al input system vamos a reescribir el código
        
        //Moverse Con las felchas
        Vector2 move = MoveAction.ReadValue<Vector2>();
        Debug.Log(move);
        Vector2 position=(Vector2)transform.position + move*0.1f*velJuagador*Time.deltaTime;
        transform.position = position;

        //Moverse con WASD
        Vector2 moveWASD = WASDAction.ReadValue<Vector2>();
        Debug.Log(moveWASD);
        Vector2 positionWASD = (Vector2)transform.position + moveWASD * 0.1f * velJuagador*Time.deltaTime;
        transform.position = positionWASD;

        //Moverse con el GamePad
        Vector2 moveGamePad = GamePadAction.ReadValue<Vector2>();
        Debug.Log(moveGamePad);
        Vector2 positionGamePad = (Vector2)transform.position + moveGamePad * 0.1f * velJuagador*Time.deltaTime;
        transform.position = positionGamePad;
    }
}
