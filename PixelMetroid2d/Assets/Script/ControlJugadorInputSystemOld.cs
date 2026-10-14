using UnityEngine;

public class ControlJugadorInput : MonoBehaviour
{

    //tenemos que pensar en si la variable es pública o privado

    public float velocidad;

    private Rigidbody2D fisica;

    private float entradaX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Obtengo el componente Rigibody2D  del player para modificar psoteriormente su linearVelocity.
        fisica = GetComponent<Rigidbody2D>();

    }


    // Update is called once per frame
    // void Update()
    //{
    //Sirve para cambiar de posición al jugdor en la posición x
    //    Vector2 position = transform.position;
    //    position.x = position.x + 0.1f;

    //    transform.position = position; 


    //}

    //esto sirve para capturar si ha pulsado el cursor o tecla izquieda o derecha
    private void FixedUpdate()
    {
        entradaX = Input.GetAxis("Horizontal");
        if (fisica != null)
        {
           fisica.linearVelocity = new Vector2(entradaX * velocidad, fisica.linearVelocity.y);
        }
        else
            Debug.LogError("El GameObject no tiene asignado un componente Rigibody2D que es necesario para mover.");
        
    }
}
