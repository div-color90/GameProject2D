using System;
using UnityEngine;
using UnityEngine.InputSystem; ////inputActionReference
using UnityEngine.Rendering;

//unity le añade automáticamente un Rigibody2D

[RequireComponent (typeof(Rigidbody2D))]
public class MovimientoJugador : MonoBehaviour // permite usar el script como componente y que Unity llame a Awake, update...
{
    //Header pone un titulo en el inspector para ordenar los campos y public hace que la variable se vea y se edite en el Inspector.
    [Header("Movimiento")]
    public float velocity;

    //Con el serializeField hubiera desaparecido porque hemos cambiado de public a private. Quiero que acceda desde el inspector. 
    //Con serializefield le decimos a Unity aunque sea privada muestrala. Otros scripts no pueden tocarla pero si en el editor. 
    //InputactionRefrence es para mapas de accion. Hace referencia a una accion concreta como Jugador/mover

    [SerializeField] private InputActionReference accionMover;

    [Header("Salto")]
    [SerializeField] private InputActionReference accionSaltar;
    //LayerMask es un selector de capas. Sirve para decirle al raycast que solo considere como suelos los objetos de esta capa.
    [SerializeField] private LayerMask capaSuelo;
public float fuerzaImpulso = 5f; //fuerza aplicada al saltar

    private Rigidbody2D fisica;
    private SpriteRenderer renderizador;
    private Animator animador;
    private Vector2 entradaX;
    private bool saltoPendiente;

    private bool estaTocandoSuelo;
    private float longitudRayCast = 1.6f;

    private float umbralCarrera = 0.1f;

    //se va a ejecutar aunque el script no esté activo.Los métodos Awake se ejecutan antes que el Start. 
    //Se ejecuta una vez al cargarse el objeto/script. Sirve para inicializar componentes
    //Se ejcuta aunque el componenete esté desactivado y siempre que el GameObject esté activado.
    private void Awake()
    {
        fisica = GetComponent<Rigidbody2D>();
        renderizador = GetComponent<SpriteRenderer>();
        animador = GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Se ejecuta antes del primer Update una sola vez y solo si el script está activado y el GameObject también. Inicializa cosas que dependan de otros objetos.
    void Start()
    {
        
    }

    // Update is called once per frame. aqui leeo el valor y en el fixedupdate aplico la física
    //Se ejecuta cada frame, muchas veces, el intervalo es variable. Se usa para leer input, logica y temporizadores, mover cosas sin física, animaciones...
    // usamos Time.deltaTime si necesitamos el tiempo transcurrido.
    // Si el juego vz a 60 FPS e llama 60fps.
    void Update()
    {
        entradaX = accionMover.action.ReadValue<Vector2>();

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, longitudRayCast, capaSuelo);
        estaTocandoSuelo = hit.collider != null;

        Color colorRayo = estaTocandoSuelo ? Color.green : Color.red;

        Debug.DrawRay(transform.position, Vector2.down * longitudRayCast, colorRayo);

        if (accionSaltar.action.WasPressedThisFrame() && estaTocandoSuelo)
        {
            saltoPendiente = true;
        }


        AnimarJugador();
    }

    private void AnimarJugador()
    {
        if (animador == null)
            return;
        if (!estaTocandoSuelo)
        {
            animador.Play("JugadorSaltando");
        }
        else if (Mathf.Abs(fisica.linearVelocity.x) > umbralCarrera)
        {
            animador.Play("JugadorCorriendo");
        }
        else
            animador.Play("JugadorParado");
    }

    //se ejecute cada vez que yo active el juego 
    //se ejecute cada vez que el script o su objeto pasa de desactivado a activado. es decir cada vez que este componente se activa.
    private void OnEnable()
    {
        if ((accionMover == null) || accionMover.action == null)
        {
            Debug.Log("Asigna Mapa de acción Jugador/Mover en Acción Mover");
            enabled = false;
            return;
        }
        accionMover.action.Enable();


        // estamos habilitando
        if ((accionSaltar == null) || accionSaltar.action == null)
        {
            Debug.Log("Asigna Mapa de acción Jugador/Saltar en Acción Saltar");
            enabled = false;
            return;
        }
        accionSaltar.action.Enable();
        
    }

    private void OnDisable()//deshabilita las acciones al desactivar el script. Así no quedan esuchando entrada cuando y ano deberían.
    {
        if (accionMover != null && accionMover.action != null)
        {
            accionMover.action.Disable();
        }


        if (accionSaltar != null && accionSaltar.action != null)
        {
            accionSaltar.action.Disable();
        }
    }

    //Se ejecuta a intervalos fijos de tiempo, muchas veces. Se usa para aplicar física, fuerza, velocidad del rigibody.
    //es el sitio para todo lo que toque rigidbody: addforce, linearVelocity..
    // si necesitamos tiempo usamos Time.fixedDeltaTime.
    private void FixedUpdate()
    {
        fisica.linearVelocity = new Vector2(entradaX.x * velocity, fisica.linearVelocity.y);

        if (saltoPendiente)
        {
            fisica.AddForce(Vector2.up * fuerzaImpulso, ForceMode2D.Impulse);
        }
        saltoPendiente = false;


        OrientarSprite();
    }

    private void OrientarSprite()
    {
        if (renderizador == null)
            return;

        if(fisica.linearVelocity.x > 0)
        {
            // La X está desactivada es decir el jugador está mirando hacia la derecha. Solo volteamos la imagen.
            renderizador.flipX = false;

        }else if(fisica.linearVelocity.x < 0)
        {  // La X está activa el jugador está mirando hacia la izquierda. Solo volteamos la imagen.
            renderizador.flipX = true;
        }

    }


    //Ciclo de vida en Unity: Awake, OnEnable, Start, FixedUpdate / Update y OnDisable
}
