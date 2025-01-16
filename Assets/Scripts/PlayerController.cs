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

    //Para el RigidBody
    Rigidbody2D rigidbody2d;
    Vector2 move;
    Vector2 moveWASD;
    Vector2 moveGamePad;
    Vector2 currentMove;

    string activeInput = "";

    //Variables para la salud
    public int maxHealth = 5;
    int currentHealth = 1;
    public int health { get { return currentHealth; } }
    // Start is called before the first frame update
    void Start()
    {
        //LeftAction.Enable();
        MoveAction.Enable();
        WASDAction.Enable();
        GamePadAction.Enable();

        //Para el movimiento con rigidbody
        rigidbody2d = GetComponent<Rigidbody2D>();

        //Para la salud del personaje
        //currentHealth = maxHealth;
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
        move = MoveAction.ReadValue<Vector2>();
        //Debug.Log(move);
        /*Vector2 position=(Vector2)transform.position + move*0.1f*velJuagador*Time.deltaTime;
        transform.position = position;*///Se usa con el transform pero ahora vamo a usar el rigidbody

        //Moverse con WASD
        moveWASD = WASDAction.ReadValue<Vector2>();
        //Debug.Log(moveWASD);
        /*Vector2 positionWASD = (Vector2)transform.position + moveWASD * 0.1f * velJuagador*Time.deltaTime;
        transform.position = positionWASD;*/

        //Moverse con el GamePad
        moveGamePad = GamePadAction.ReadValue<Vector2>();
        //Debug.Log(moveGamePad);
        /*Vector2 positionGamePad = (Vector2)transform.position + moveGamePad * 0.1f * velJuagador*Time.deltaTime;
        transform.position = positionGamePad;*/

        //Para poder hacerlo combinado esta esta opcion
        if (moveGamePad != Vector2.zero)
        {
            activeInput = "GamePad";
            currentMove = moveGamePad;
        }
        else if (moveWASD != Vector2.zero)
        {
            activeInput = "WASD";
            currentMove = moveWASD;
        }
        else if (move != Vector2.zero)
        {
            activeInput = "Arrows";
            currentMove = move;
        }
        else 
        {
            activeInput = "None";
            currentMove = Vector2.zero;
        }
        //Debug.Log($"Active Input: {activeInput} | Movement: {currentMove}");
    }
    private void FixedUpdate()
    {
        //Con las flechas
        //Vector2 position=(Vector2)rigidbody2d.position + move * 0.1f * velJuagador * Time.deltaTime;
        //rigidbody2d.MovePosition(position);

        //Solo me deja hacer el movimiento con uno. Ejmplo si pondo flechas + WASD solo me hace el WASD. Entiendo que es por el orden de ejecucion pero no se arreglarlo.

        //Moverse con WASD
        //Vector2 positionWASD = (Vector2)rigidbody2d.position + moveWASD * 0.1f * velJuagador * Time.deltaTime;
        //rigidbody2d.MovePosition(positionWASD);

        //Moverse con el GamePad
        //Vector2 positionGamePad = (Vector2)rigidbody2d.position + moveGamePad * 0.1f * velJuagador * Time.deltaTime;
        //rigidbody2d.MovePosition(positionGamePad);

        //Para hacerlo combinado
        Vector2 newposition = (Vector2)rigidbody2d.position + currentMove * 0.1f * velJuagador * Time.deltaTime;
        rigidbody2d.MovePosition(newposition);
    }
    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);//Le da un valor maximo y minimo
        Debug.Log(currentHealth + "/" + maxHealth);
    }
}
