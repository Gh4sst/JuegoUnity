using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruir : MonoBehaviour
{
    public GameObject objectToDestroy; // Objeto que se destruirá al entrar en contacto

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica si el objeto que ha tocado el personaje es igual al objeto que se debe destruir
        if (collision.collider.gameObject == objectToDestroy)
        {
            // Destruye al objeto
            Destroy(objectToDestroy);
            Debug.Log("El enemigo te ha comido");
        }
    }
}