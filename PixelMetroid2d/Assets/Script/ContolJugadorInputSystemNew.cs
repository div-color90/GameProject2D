using UnityEngine;
using UnityEngine.InputSystem;

public class ContolJugadorInputSystemNew : MonoBehaviour
{
    // hemos definido como se  mueva el jugador
    public InputAction moveAction;
    public float velocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    { //le estamos diciendo que se habilite está acción
        moveAction.Enable();


        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 10; //estamos haciendo 10fps 
    }

    // Update is called once per frame
    void Update()
    {
        //Quiero que en cada frame me diga si se ha pulsado esa tecla del curso izquierda y derecha y se ha pulsado guardame en move los movimientos.
        Vector2 move = moveAction.ReadValue<Vector2>();
       
        //estamos sumando x e y. Hemos convertido transform position en un vector2 para que la suma sea del mismo. Usamos Time.deltaTime y multiplicamos el movimiento por la velocidad.
        Vector2 position =(Vector2)transform.position + move*velocity*Time.deltaTime;
        
        //Le digo que estamos volviendo a la posición nueva y se guardar con su componente composición.
        transform.position = position;

    }
}
