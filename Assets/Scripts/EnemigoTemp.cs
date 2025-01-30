using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemigoTemp : MonoBehaviour
{
    Animator animator;

    [SerializeField] float enemieSpeed;
    Vector2 playerPos;

    //Para el RigidBody
    Rigidbody2D rigidbody2d;

    bool broken = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.instance.enemigosPersiguen == true)
        {
            rigidbody2d.MovePosition(Vector2.MoveTowards(transform.position, (Vector2)PlayerController.instance.transform.position, enemieSpeed * Time.deltaTime));
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }
    public void Fix()
    {
        broken = false;
        rigidbody2d.simulated = false;
        animator.SetTrigger("Fixed");
    }
}
