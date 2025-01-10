using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float velJuagador;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;//Le otorga el movimiento en las coordenadas X e Y
        position.x = position.x + 0.1f*velJuagador;
        position.y = position.y + 0.1f*velJuagador;//Con estas dos lineas se mueve en vertical a una velocidad reducida
        transform.position = position;
    }
}
