using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje : MonoBehaviour
{
    [SerializeField] private PersonajeStats stats;

    public PersonajeAtaque personajeAtaque { get; private set; }
    public PersonajeExperiencia personajeExperiencia { get; set; }
    public PersonajeVida personajeVida { get; private set; }
    public PersonajeAnimaciones personajeAnimaciones { get; private set; }
    public PersonajeMana personajeMana { get; private set; }
    
    private void Awake()
    {
        personajeVida        = GetComponent<PersonajeVida>();
        personajeAnimaciones = GetComponent<PersonajeAnimaciones>();
        personajeMana        = GetComponent<PersonajeMana>();
        personajeExperiencia = GetComponent<PersonajeExperiencia>();
        personajeAtaque      = GetComponent<PersonajeAtaque>();
    }

    public void RestaurarPersonaje()
    {
        personajeVida.RestaurarPersonaje();
        personajeAnimaciones.RevivirPersonaje();
        personajeMana.RestableceMana();
    }

    private void AtributoRespuesta(TipoAtributo tipo)
    {
        if ( stats.PuntosDisponibles <= 0)
        {
            return;
        }
        switch (tipo)
        {
            case TipoAtributo.Fuerza:
            stats.Fuerza++;
            stats.AnadirBonusPorAtributoFuerza();
            break;
            case TipoAtributo.Inteligencia:
            stats.Inteligencia++;
            stats.AnadirBonusPorAtributoInteligencia();
            break;
            case TipoAtributo.Destreza:
            stats.Destreza++;
            stats.AnadirBonusPorAtributoDestreza();
            break;
        }
        stats.PuntosDisponibles -= 1;
    }

    private void OnEnable()
    {
        AtributoButton.EventoAgregarAtributo += AtributoRespuesta;
    }

    private void OnDisable()
    {
        AtributoButton.EventoAgregarAtributo -= AtributoRespuesta;
    }
}
