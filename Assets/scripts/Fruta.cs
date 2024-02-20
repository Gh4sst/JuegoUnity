using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] private float cantidadPuntos;
    [SerializeField] private GameObject efecto;

    [SerializeField] private Puntaje puntaje; 


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("main character")) // Verifica si el objeto que tocó la fruta es el jugador
        {
            puntaje.SumarPuntos(cantidadPuntos);
            Instantiate(efecto,transform.position,Quaternion.identity);
            // Si el jugador toca la fruta, la destruye
            Destroy(gameObject);
        }
    }
}