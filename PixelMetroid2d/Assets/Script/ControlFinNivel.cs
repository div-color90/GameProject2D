using System;
using TMPro;
using UnityEngine;

public class ControlFinNivel : MonoBehaviour
{
    [SerializeField] private TMP_Text mensajeFinal;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     if(mensajeFinal == null)
        {
            Debug.LogError("Asigna el TextMeshPro a Mensaje Final");
            return;
        }
        ControlDatosJuegos datosJuego = ControlDatosJuegos.Instancia;

        mensajeFinal.text = datosJuego.HaGanado ? "¡Has ganado!\n Puntuación obtenido: " + datosJuego.Puntuacion
            : "Lo siento. Has perdido.";


    }

     
    
}
