using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyController : MonoBehaviour
{
    Animator animator;
    
    public float velEnemy;
    public float changeTime;
    float timer;
    int direccion = 1;
    /*public float tiempoParaCambiar;
    public float cambiaEn=2;*/

    //Para el RigidBody
    Rigidbody2D rigidbody2d;

    //Booleanos
    public bool vertical;
    /*[SerializeField]
    bool couldChange;*/
    bool positivo;

    // Start is called before the first frame update
    void Start()
    {
        //Para el movimiento con rigidbody
        rigidbody2d = GetComponent<Rigidbody2D>();

        timer = changeTime;
        //couldChange = false;

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            direccion = -direccion;
            timer = changeTime;
        }
        /*tiempoParaCambiar -= Time.deltaTime;
        if (tiempoParaCambiar <= 0)
        {
            couldChange = true;
            tiempoParaCambiar = cambiaEn;
        }*/
        if (direccion == -1)
        {
            positivo = false;
        }
        else 
        {
            positivo = true;
        }
    }
    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        if (vertical)
        {
            position.y = position.y + velEnemy* direccion * 0.1f * Time.deltaTime;
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", direccion);
        }
        else
        {
            position.x = position.x + velEnemy * direccion * 0.1f * Time.deltaTime;
            animator.SetFloat("MoveX", direccion);
            animator.SetFloat("MoveY", 0);
        }
        rigidbody2d.MovePosition(position);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*if (couldChange)
        {
            int randomDirection = Random.Range(0, 4);
            if (randomDirection == 0)
            {
                vertical = true;
                direccion = 1;
            }
            if (randomDirection == 1)
            {
                vertical = true;
                direccion = -1;
            }
            if (randomDirection == 2)
            {
                vertical = false;
                direccion = 1;
            }
            if (randomDirection == 3)
            {
                vertical = false;
                direccion = -1;
            }
        Vector2 offset = vertical ? Vector2.up * -0.1f * direccion : Vector2.right * -0.1f * direccion;
        rigidbody2d.position += offset;*/
        vertical = !vertical;
        positivo = !positivo;
        if (positivo == false)
        {
            direccion = -1;
        }
        else
        {
            direccion = 1;
        }
        timer = changeTime;

        /*tiempoParaCambiar = cambiaEn;
        couldChange = false;
        }*/
    }
}
