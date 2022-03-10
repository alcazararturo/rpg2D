using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeAtaque : MonoBehaviour
{
    public Arma ArmaEquipada { get; private set; }

    public void EquiparArma(ItemArma armaPorEquipar)
    {
        ArmaEquipada = armaPorEquipar.Arma;        
    }
    public void RemoverArma()
    {
        if (ArmaEquipada == null) { return; }
        ArmaEquipada = null;
    }
}
