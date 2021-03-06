using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DireccionMovimiento
{
    Horizontal,
    Vertical   
}

public class WaypointMovimiento : MonoBehaviour
{
    
    [SerializeField] protected float velocidad;

    public Vector3 PuntoPorMoverse => _waypoint.ObtenerPosicionMovimiento(puntoActualIndex);

    protected Waypoint _waypoint;
    protected Animator _animator;
    protected int puntoActualIndex;
    protected Vector3 ultimaPosicion;

    // Start is called before the first frame update
    private void Start()
    {
        puntoActualIndex = 0;
        _waypoint = GetComponent<Waypoint>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        MoverPersonaje();
        RotarPersonaje();
        RotarVertical();
        if (ComprobarPuntoActualAlcanzado())
        {
            ActualizarIndexMovimiento();
        }
    }

    private void MoverPersonaje()
    {
        transform.position = Vector3.MoveTowards(transform.position, PuntoPorMoverse, velocidad * Time.deltaTime);
    }

    private bool ComprobarPuntoActualAlcanzado()
    {
        float distanciaHaciaPuntoActual = (transform.position - PuntoPorMoverse).magnitude;
        if (distanciaHaciaPuntoActual < 0.1f) 
        {
            ultimaPosicion = transform.position; 
            return true; 
        }
        return false;
    }

    private void ActualizarIndexMovimiento()
    {
        if (puntoActualIndex == _waypoint.Puntos.Length - 1)
        {
            puntoActualIndex = 0;
        }
        else 
        {
            if (puntoActualIndex < _waypoint.Puntos.Length - 1)
            {
                puntoActualIndex++;
            }
        }
    }

    protected virtual void RotarPersonaje()
    {
        
    }

    protected virtual void RotarVertical()
    {

    }
}
