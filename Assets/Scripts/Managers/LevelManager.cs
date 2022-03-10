using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Personaje personaje;
    [SerializeField] private Transform puntoReaparicion;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (personaje.personajeVida.Derrotado)
        {
            personaje.transform.localPosition = puntoReaparicion.position;
            personaje.RestaurarPersonaje();
        }
        }
    }
}
