using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogoManager : Singleton<DialogoManager>
{
    [SerializeField] private GameObject paneldialogo;
    [SerializeField] private Image npcIcono;
    [SerializeField] private TextMeshProUGUI npcNombreTMP;
    [SerializeField] private TextMeshProUGUI npcConversacionTMP;

    public NPCInteraccion NPCDisponible { get; set; }

    private Queue<string> dialogoSecuencia;
    private bool dialogoAnimado;
    private bool despedidaMostrada;

    private void Start()
    {
        dialogoSecuencia = new Queue<string>();
    }

    private void Update()
    {
        if ( NPCDisponible == null ) { return; }
        if ( Input.GetKeyDown(KeyCode.E))
        {
            ConfigurarPanel(NPCDisponible.Dialogo);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (despedidaMostrada)
            {
                AbrirCerrarPanelDialogo(false);
                despedidaMostrada = false;
                return;
            }
            if (NPCDisponible.Dialogo.ContieneInteraccionExtra)
            {
                UIManager.Instance.AbrirPanelInteraccion(NPCDisponible.Dialogo.InteraccionExtra);
                AbrirCerrarPanelDialogo(false);
                return;
            }
            if (dialogoAnimado)
            {
                Continuardialogo();
            }
        }
    }

    public void AbrirCerrarPanelDialogo(bool estado)
    {
        paneldialogo.SetActive(estado);
    }
    private void ConfigurarPanel(NPCDialogo npcDialogo)
    {
        AbrirCerrarPanelDialogo(true);
        CargarDialogosSecuencia(npcDialogo);
        npcIcono.sprite   = npcDialogo.Icono;
        npcNombreTMP.text = $"{npcDialogo.Nombre}";
        MostrarTextoConAnimacion(npcDialogo.Saludo);

    }

    private void CargarDialogosSecuencia(NPCDialogo npcDialogo)
    {
        if (npcDialogo.Conversacion == null || npcDialogo.Conversacion.Length <= 0 ) { return; }
        for (int i = 0; i < npcDialogo.Conversacion.Length; i++)
        {
            dialogoSecuencia.Enqueue(npcDialogo.Conversacion[i].Oracion);
        }
    }

    private void Continuardialogo()
    {
        if (NPCDisponible == null) { return; }
        if (despedidaMostrada) { return; }
        if (dialogoSecuencia.Count == 0)
        {
            string despedida = NPCDisponible.Dialogo.Despedida;
            MostrarTextoConAnimacion(despedida);
            despedidaMostrada = true;
            return;
        }
        string siguienteDialogo = dialogoSecuencia.Dequeue();
        MostrarTextoConAnimacion(siguienteDialogo);
    }

    private IEnumerator AnimarTexto(string oracion)
    {
        dialogoAnimado = false;
        npcConversacionTMP.text = "";
        char[] letras = oracion.ToCharArray();
        for (int i = 0; i < letras.Length; i++)
        {
            npcConversacionTMP.text += letras[i];
            yield return new WaitForSeconds(0.03f); 
        }
        dialogoAnimado = true;
    }

    private void MostrarTextoConAnimacion(string oracion)
    {
        StartCoroutine(AnimarTexto(oracion));
    }

}

