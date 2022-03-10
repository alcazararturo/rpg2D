using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaBase : MonoBehaviour
{
    [SerializeField] protected float saludInicial;
    [SerializeField] protected float saludMax;

    public float Salud { get; protected set; }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Salud = saludInicial;
    }

    public void RecibirDanio(float cantidad)
    {
        if (cantidad <= 0) 
        {
            return;
        }
        if( saludInicial > 0f) 
        {
            Salud -= cantidad;
            ActualizarBarraVida(Salud, saludMax);
            if (Salud <= 0f)
            {
                Salud = 0f;
                ActualizarBarraVida(Salud, saludMax);
                PersonajeDerrotado();
            }
        }
    }

    protected virtual void ActualizarBarraVida(float vidaActual, float vidaMax)
    {

    }

    protected virtual void PersonajeDerrotado()
    {

    }


}
