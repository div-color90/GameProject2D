using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class MovimientoJugadorSaltoMetodo2 : MonoBehaviour
{


    [Header("Movimiento")]
    public float velocity;

    //Con el serializeField hubiera desaparecido porque hemos cambiado de public a private. Quiero que acceda desde el inspector. Inpuracction refrence es para mapas de accion
    [SerializeField] private InputActionReference accionMover;

    [Header("Salto")]
    [SerializeField] private InputActionReference accionSaltar;

    public float fuerzaImpulso = 5f; //fuerza aplicada al saltar
    [SerializeField] private LayerMask capaSuelo;

    [Header("Capa de suelo")]
    [SerializeField]  private float groundDistance = 0.2f; // Radio de la esfera
    [SerializeField]  private Transform esferaPies; // Objeto vacío en los pies

    private Rigidbody2D fisica;
    private Vector2 entradaMovimiento;
    private bool estaTocandoSuelo;
    private bool saltoPendiente;

    private void Awake()
    {
        fisica = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        entradaMovimiento=accionMover.action.ReadValue<Vector2>();//para que se mueva de izquierda a derecha

        estaTocandoSuelo= Physics2D.OverlapCircle(esferaPies.position, groundDistance, capaSuelo);

        if(accionSaltar.action.WasPressedThisFrame() && estaTocandoSuelo)
        {
            saltoPendiente = true;
        }
    }

    private void FixedUpdate()
    {
        fisica.linearVelocity = new Vector2(entradaMovimiento.x * velocity, fisica.linearVelocity.y);//que se mueva hacia izquierda y derecha a una velocidad determinada

        if(saltoPendiente)
        {
            fisica.AddForce(Vector2.up * fuerzaImpulso, ForceMode2D.Impulse);
        }
        saltoPendiente = false;

    }

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



    private void OnDrawGizmosSelected()
    {
        if(esferaPies != null)
        {
            Gizmos.color = Color.red;

            Gizmos.DrawSphere(esferaPies.position, groundDistance);
        }
    }




}
