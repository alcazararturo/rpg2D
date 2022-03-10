using System.Collections;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public enum TiposDeAtaque 
{
    Melee,
    Embestida
}

public class IAController : MonoBehaviour
{
    public static Action<float> EventoDanioRealizado;

    [Header("Stats")]
    [SerializeField] private PersonajeStats stats;

    [Header("Estados")]
    [SerializeField] private IAEstado estadoInicial;
    [SerializeField] private IAEstado estadoDefault;

    [Header("Config")]
    [SerializeField] private float rangoDeteccion;
    [SerializeField] private float rangoDeAtaque;
    [SerializeField] private float rangoDeEmbestida;
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float velocidadDeEmbestida;
    [SerializeField] private LayerMask personajeLayerMask;

    [Header("Ataque")]
    [SerializeField] private float danio;
    [SerializeField] private float tiempoEntreAtaques;
    [SerializeField] private TiposDeAtaque tipoAtaque;

    [Header("Debug")]
    [SerializeField] private bool mostrarDeteccion;
    [SerializeField] private bool mostrarRangoDeAtaque;
    [SerializeField] private bool mostrarRangoDeEmbestida;

    private float tiempoParaSiguienteAtaque;
    private BoxCollider2D boxCollider2D;

    public Transform PersonajeReferencia { get; set; }
    public IAEstado EstadoActual { get; set; } 
    public EnemigoMovimiento EnemyMovimiento { get; set; }
    public float RangoDeteccion => rangoDeteccion;
    public float Danio => danio;
    public TiposDeAtaque TipoAtaque => tipoAtaque;
    public float VelocidadMovimiento => velocidadMovimiento;
    public LayerMask PersonajeLayerMask => personajeLayerMask;
    public float RangoDeAtaqueDeterminado => tipoAtaque == TiposDeAtaque.Embestida ? rangoDeEmbestida : rangoDeAtaque;
    
    private void Start()
    {
        boxCollider2D   = GetComponent<BoxCollider2D>();
        EstadoActual    = estadoInicial;
        EnemyMovimiento = GetComponent<EnemigoMovimiento>();
    }

    private void Update()
    {
        EstadoActual.EjecutarEstado(this);
    }

    public void CambiarEstado(IAEstado nuevoEstado)
    {
        if (nuevoEstado != estadoDefault)
        {
            EstadoActual = nuevoEstado;
        }
    }

    public void AtaqueMelee(float cantidad)
    {
        if (PersonajeReferencia != null)
        {
            AplicarDanioAlPersonaje(cantidad);
        }
    }

    public void AtaqueEmbestida(float cantidad)
    {
        StartCoroutine(IEEmbestida(cantidad));
    }

    private IEnumerator IEEmbestida(float cantidad)
    {
        Vector3 personajePosicion       = PersonajeReferencia.position;
        Vector3 posicionInicial         = transform.position;
        Vector3 direccionHaciaPersonaje = (personajePosicion - posicionInicial).normalized;
        Vector3 posicionDeAtaque        = personajePosicion - direccionHaciaPersonaje * 0.5f;
        boxCollider2D.enabled           = false;
        float transicionDeAtaque        = 0f;

        while (transicionDeAtaque <= 1f)
        {
            transicionDeAtaque += Time.deltaTime * velocidadMovimiento;
            float interpolacion = (-Mathf.Pow(transicionDeAtaque, 2) + transicionDeAtaque) * 4;
            transform.position  = Vector3.Lerp(posicionInicial, posicionDeAtaque, interpolacion);
            yield return null;
        }
        if (PersonajeReferencia != null)
        {
            AplicarDanioAlPersonaje(cantidad);
        }
        boxCollider2D.enabled = true;
    }

    public void AplicarDanioAlPersonaje(float cantidad)
    {
        float danioPorRealizar = 0;
        if (Random.value < (stats.PorcentajeBloqueo/100) ) { return; }
        danioPorRealizar = Mathf.Max(cantidad - stats.Defensa, 1f);
        PersonajeReferencia.GetComponent<PersonajeVida>().RecibirDanio(danioPorRealizar);
        EventoDanioRealizado?.Invoke(danioPorRealizar);
    }

    public bool PersonajeEnRangoDeAtaque(float rango)
    {
        float distanciaHaciaPersonaje = (PersonajeReferencia.position - transform.position).sqrMagnitude;
        if (distanciaHaciaPersonaje < Mathf.Pow(rango,2)) { return true; }
        return false;
    }

    public bool EsTiempoDeAtacar()
    {
        if (Time.time > tiempoParaSiguienteAtaque) { return true; }
        return false;
    }

    public void ActualizarTiempoEntreAtaques()
    {
        tiempoParaSiguienteAtaque = Time.time + tiempoEntreAtaques;
    }

    private void OnDrawGizmos()
    {
        if (mostrarDeteccion)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, rangoDeteccion);
        }

        if (mostrarRangoDeAtaque)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, rangoDeAtaque);
        }

        if (mostrarRangoDeEmbestida)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, rangoDeEmbestida);
        }
    }
}
