using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    Vector2 moveDirection = new Vector2(1,0);
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
    [SerializeField]
    int currentHealth;
    public int health { get { return currentHealth; } }

    //Para controllar el tiempo en el que recibes daño
    float timeInvincible = 2;
    bool isInvencible;
    [SerializeField]
    float damageCooldown;

    //Para controlar el tiempo en la zona de vida
    float timeEsperaVida = 1;
    bool addHealth;
    float healthCooldown;

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
        currentHealth = maxHealth;

        //Para la animacion del personaje
        animator = GetComponent<Animator>();
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

        //Para la animacion
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            moveDirection.Set(move.x, move.y);
            moveDirection.Normalize();
        }

        if (!Mathf.Approximately(moveWASD.x, 0.0f) || !Mathf.Approximately(moveWASD.y, 0.0f))
        {
            moveDirection.Set(moveWASD.x, moveWASD.y);
            moveDirection.Normalize();
        }

        if (!Mathf.Approximately(moveGamePad.x, 0.0f) || !Mathf.Approximately(moveGamePad.y, 0.0f))
        {
            moveDirection.Set(moveGamePad.x, moveGamePad.y);
            moveDirection.Normalize();
        }
        animator.SetFloat("Look X", moveDirection.x);
        animator.SetFloat("Look Y", moveDirection.y);
        //animator.SetFloat("Speed", move.magnitude);

        //Para poder hacerlo combinado está esta opcion
        if (moveGamePad != Vector2.zero)
        {
            activeInput = "GamePad";
            currentMove = moveGamePad;
            animator.SetFloat("Speed", moveGamePad.magnitude);
        }
        else if (moveWASD != Vector2.zero)
        {
            activeInput = "WASD";
            currentMove = moveWASD;
            animator.SetFloat("Speed", moveWASD.magnitude);
        }
        else if (move != Vector2.zero)
        {
            activeInput = "Arrows";
            currentMove = move;
            animator.SetFloat("Speed", move.magnitude);
        }
        else 
        {
            activeInput = "None";
            currentMove = Vector2.zero;
            animator.SetFloat("Speed", 0f);
        }
        Debug.Log($"Active Input: {activeInput} | Movement: {currentMove}");

        //Para controlar el cooldown para recibir daño
        if (isInvencible)
        {
            damageCooldown -=Time.deltaTime;
            if (damageCooldown < 0)
            { 
                isInvencible = false;
            }
        }
        if (addHealth == false)
        {
            healthCooldown -= Time.deltaTime;
            if (healthCooldown < 0)
            {
                addHealth = true;
            }
        }
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
        if (amount < 0)
        {
            if (isInvencible)
            {
                return;
            }
            isInvencible = true;
            damageCooldown = timeInvincible;
            animator.SetTrigger("Hit");
        }
        if (amount > 0)
        {
            if (addHealth == false)
            {
                return;
            }
            addHealth = false;
            healthCooldown = timeEsperaVida;
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);//Le da un valor maximo y minimo
        UIHandler.Instance.SetHealthValue(currentHealth/(float)maxHealth);
    }
}
