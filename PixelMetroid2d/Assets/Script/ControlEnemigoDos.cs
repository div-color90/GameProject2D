using UnityEngine;

public class ControlEnemigoDos : MonoBehaviour
{
    [Header("ControlEnemidoDos")]
    private SpriteRenderer renderizador;
    private Animator animador;



    private void Awake()
{
    renderizador = GetComponent<SpriteRenderer>();
    animador = GetComponent<Animator>();
}
// Start is called once before the first execution of Update after the MonoBehaviour is created
void Start()
    {
        
    }

    // Update is called once per frame
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

        if (fisica.linearVelocity.x > 0)
        {
            // La X está desactivada es decir el jugador está mirando hacia la derecha. Solo volteamos la imagen.
            renderizador.flipX = false;

        }
        else if (fisica.linearVelocity.x < 0)
        {  // La X está activa el jugador está mirando hacia la izquierda. Solo volteamos la imagen.
            renderizador.flipX = true;
        }

    }
}
