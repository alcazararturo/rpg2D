using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeExperiencia : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private PersonajeStats stats;

    [Header("Config")]
    [SerializeField] private int nivelMax; // nivel maximo que el usuario puede alcanzar
    [SerializeField] private int expBase; // experiencia necesaria para pasar a otro nivel
    [SerializeField] private int valorIncremental; // el valor que incrementa cada vez que aumenta la experiencia

    private float expActual;
    private float expActualTemp;
    private float expRequeridaSiguienteNivel;
    // Start is called before the first frame update
    void Start()
    {
        stats.Nivel = 1;
        expRequeridaSiguienteNivel       = expBase;
        stats.ExpRequeridaSiguienteNivel = expRequeridaSiguienteNivel;
        ActualizarBarraExp();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            AnadirExperiencia(2f);
        }
    }

    public void AnadirExperiencia(float expObtenida)
    {
        if ( expObtenida > 0f )
        {
            float expRestanteNuevoNivel = expRequeridaSiguienteNivel - expActualTemp;
            if ( expObtenida >= expRestanteNuevoNivel)
            {
                expObtenida -= expRestanteNuevoNivel;
                expActual   += expObtenida;
                ActualizarNivel();
                AnadirExperiencia(expObtenida);
            } else
            {
                expActual     += expObtenida;
                expActualTemp += expObtenida;
                if (expActualTemp == expRestanteNuevoNivel )
                {
                    ActualizarNivel();
                }
            }
        }
        stats.ExpActual = expActual;
        ActualizarBarraExp();
    }

    private void ActualizarNivel()
    {
        if ( stats.Nivel < nivelMax )
        {
            stats.Nivel ++;
            expActualTemp = 0f;
            expRequeridaSiguienteNivel *= valorIncremental;
            stats.ExpRequeridaSiguienteNivel = expRequeridaSiguienteNivel;
            stats.PuntosDisponibles += 3;
        }
    }

    private void ActualizarBarraExp()
    {
        UIManager.Instance.ActualizarExpPersonaje(expActualTemp, expRequeridaSiguienteNivel);
    }

}
