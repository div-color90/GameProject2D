using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

[RequireComponent (typeof(Rigidbody2D))]
public class MovimientoJugador : MonoBehaviour
{

    [Header("Movimiento")]
    public float velocity;

    //Con el serializeField hubiera desaparecido porque hemos cambiado de public a private. Quiero que acceda desde el inspector. Inpuracction refrence es para mapas de accion
    [SerializeField] private InputActionReference accionMover;

    [Header("Salto")]
    [SerializeField] private InputActionReference accionSaltar;
    [SerializeField] private LayerMask capaSuelo;
public float fuerzaImpulso = 5f; //fuerza aplicada al saltar

    private Rigidbody2D fisica;
    private Vector2 entradaX;
    private bool saltoPendiente;

    private bool estaTocandoSuelo;
    private float longitudRayCast = 1.6f;

    //se va a ejecutar aunque el script no esté activo.Los métodos Awake se ejecutan antes que el Start.
    private void Awake()
    {
        fisica = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame. aqui leeo el valor y en el fixedupdate aplico la física
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
    }
    //se ejecute cada vez que yo active el juego 
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

    private void OnDisable()
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
    private void FixedUpdate()
    {
        fisica.linearVelocity = new Vector2(entradaX.x * velocity, fisica.linearVelocity.y);

        if (saltoPendiente)
        {
            fisica.AddForce(Vector2.up * fuerzaImpulso, ForceMode2D.Impulse);
        }
        saltoPendiente = false;
    }



}
