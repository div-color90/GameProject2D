using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ControlEnemigoDos : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 2f;
    public Vector2 posicionFinal;
    private Vector2 posicionInicial;
    private bool movimientoHaciaLaDerecha = true;
    [SerializeField] private float tolerancia = 0.01f;


    private Rigidbody2D fisica;

    [Header("Animacion")]
    private SpriteRenderer renderizador;
    private Animator animador;
    private bool estaTocandoSuelo;
    private float umbralCarrera = 0.1f;

    void Awake()
    {
        fisica = GetComponent<Rigidbody2D>();
        renderizador = GetComponent<SpriteRenderer>();
        animador = GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        posicionInicial = fisica.position;
    }

    // Update is called once per frame
    void Update()
    {
        animarCangrejo();
    }

    private void animarCangrejo()
    {
        if (animador == null)
        {
            return;
        }

        animador.Play("AnimacionCaminarCangrejo");//como lo unico que hace el cagrejo es caminar, no hay necesidad de hacer un if para ver si esta corriendo o no, siempre va a estar corriendo
    }

    void FixedUpdate()
    {
        //Vector2 posicionObjetivo;

        if (velocidad <= 0)
        {
            return;
        }

        Vector2 posicionObjetivo = movimientoHaciaLaDerecha ? posicionFinal : posicionInicial;

        if (Vector2.Distance(fisica.position, posicionObjetivo) <= tolerancia)
        {
            movimientoHaciaLaDerecha = !movimientoHaciaLaDerecha; //si se mueve hacia un lado cambia el movimiento hacia ese lado a falso para que vaya hacia el lado contrario
        }
        fisica.MovePosition(Vector2.MoveTowards(fisica.position, posicionObjetivo, velocidad * Time.fixedDeltaTime));

        OrientarSprite();
    }

    private void OrientarSprite()
    {
        if (renderizador == null) return;

        if (movimientoHaciaLaDerecha)    // va hacia posicionFinal, ademas uso el boolean movimientoHaciaLaDerecha porque el cangrejo no usa fisica.velocity.x para moverse, sino que usa MovePosition, por lo que no puedo usar la velocidad para saber hacia donde se mueve
            renderizador.flipX = false;
        else                            // va hacia posicionInicial
            renderizador.flipX = true;
    }
}