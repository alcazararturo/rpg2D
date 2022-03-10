using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextoAnimacion : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI danioTexto;

    public void EstablecerTexto(float cantidad)
    {
        danioTexto.text = cantidad.ToString();
    }
}
