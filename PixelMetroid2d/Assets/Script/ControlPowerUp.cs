using UnityEngine;

public class ControlPowerUp : MonoBehaviour
{
    [SerializeField, Range(1, 10)] private int cantidad = 2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        Debug.Log("El jugador ha recogido el orbe");

        if (collision.gameObject.TryGetComponent(out MovimientoJugador jugador))
        {
            //LLamamos al metodo IncrementarPuntos del script MovimientoJugador para aumentar la puntuación del jugador
            jugador.IncrementarPuntos(cantidad);
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("El objeto player necesita un componente MovimientoJugador)");
        }
    }
}

