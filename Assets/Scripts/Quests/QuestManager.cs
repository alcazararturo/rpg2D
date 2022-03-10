using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : Singleton<QuestManager>
{
    [Header("Personaje")]
    [SerializeField] private Personaje personaje;

    [Header("Quests")]
    [SerializeField] private Quest[] questDisponibles;

    [Header("InspectorQuest")]
    [SerializeField] private InspectorQuestDescripcion inspectorQuestPrefab;
    [SerializeField] private Transform inspectorQuestContenedor;

    [Header("PersonajeQuest")]
    [SerializeField] private PersonajeQuestDescripcion personajeQuestPrefab;
    [SerializeField] private Transform personajeQuestContenedor;

    [Header("PanelQuestCompletado")]
    [SerializeField] private GameObject panelQuestCompletado;
    [SerializeField] private TextMeshProUGUI questNombre;
    [SerializeField] private TextMeshProUGUI questRecompensaOro;
    [SerializeField] private TextMeshProUGUI questRecompensaExt;
    [SerializeField] private TextMeshProUGUI questRecompoensaItemCantidad;
    [SerializeField] private Image questRecompensaItemIcono;

    public Quest QuestPorReclamar { get; private set; }
    
    // Start is called before the first frame update
    private void Start()
    {
        CargarQuestEnInspector();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            AnadirProgero("Mata10", 1);
            AnadirProgero("Mata25", 1);
            AnadirProgero("Mata50", 1);
        }
    }

    private void CargarQuestEnInspector()
    {
        for (int i = 0; i < questDisponibles.Length; i++)
        {
           InspectorQuestDescripcion nuevoQuest = Instantiate(inspectorQuestPrefab, inspectorQuestContenedor);
           nuevoQuest.ConfgurarQuestUI(questDisponibles[i]);
        }
    }

    private void AnadirQuestPorCompletar(Quest questPorCompletar)
    {
        PersonajeQuestDescripcion nuevoQuest = Instantiate(personajeQuestPrefab, personajeQuestContenedor);
        nuevoQuest.ConfgurarQuestUI(questPorCompletar);
    }

    public void AnadirQuest(Quest questPorCompletar)
    {
        AnadirQuestPorCompletar(questPorCompletar);
    }

    public void ReclamarRecompensa()
    {
        if (QuestPorReclamar == null) { return; }
        MonedaManager.Instance.AnadirMonedas(QuestPorReclamar.RecompensaOro);
        personaje.personajeExperiencia.AnadirExperiencia(QuestPorReclamar.RecompensaExp);
        Inventario.Instance.AnadirItem(QuestPorReclamar.RecompensaItem.Item, QuestPorReclamar.RecompensaItem.Cantidad);
        panelQuestCompletado.SetActive(false);
        QuestPorReclamar = null;
    }

    public void AnadirProgero(string questID, int cantidad)
    {
        Quest questPorActualizar = QuestExiste(questID);
        if( questPorActualizar != null )
        {
            questPorActualizar.AnadirProgreso(cantidad);
        }
    }

    private Quest QuestExiste( string questID)
    {
        for (int i = 0; i < questDisponibles.Length; i++)
        {
            if (questDisponibles[i].ID == questID) 
            {
                return questDisponibles[i];
            }
        }
        return null;
    }

    private void MostrarQuestCompletado(Quest questCompletado)
    {
        panelQuestCompletado.SetActive(true);
        questNombre.text                  = questCompletado.Nombre;
        questRecompensaOro.text           = questCompletado.RecompensaOro.ToString();
        questRecompensaExt.text           = questCompletado.RecompensaExp.ToString();
        questRecompoensaItemCantidad.text = questCompletado.RecompensaItem.Cantidad.ToString();
        questRecompensaItemIcono.sprite   = questCompletado.RecompensaItem.Item.Icono;
    }

    private void QuestCompletadoRespuesta(Quest questCompletado)
    {
        QuestPorReclamar = QuestExiste(questCompletado.ID);
        if (QuestPorReclamar != null)
        {
            MostrarQuestCompletado(QuestPorReclamar);
        }
    }

    private void OnEnable()
    {   
        //if (QuestPorCompletar.QuestCompletadoCheck) { gameObject.SetActive(false); }
        Quest.EventoQuestCompletado += QuestCompletadoRespuesta;
    }

    private void OnDisable()
    {
        Quest.EventoQuestCompletado -= QuestCompletadoRespuesta;
    }

}
