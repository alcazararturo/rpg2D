using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("Stats")]
    [SerializeField] private PersonajeStats stats;

    [Header("Paneles")]
    [SerializeField] private GameObject panelStats;
    [SerializeField] private GameObject panelInventario;
    [SerializeField] private GameObject panelInspectorQuests;
    [SerializeField] private GameObject panelPersonajeQuests;
    
    [Header("Barra")]
    [SerializeField] private Image vidaPlayer;
    [SerializeField] private Image manaPlayer;
    [SerializeField] private Image expPlayer;

    [Header("Texto")]
    [SerializeField] private TextMeshProUGUI vidaTMP;
    [SerializeField] private TextMeshProUGUI manaTMP;
    [SerializeField] private TextMeshProUGUI expTMP;
    [SerializeField] private TextMeshProUGUI nivelTMP;
    [SerializeField] private TextMeshProUGUI monedasTMP;


    [Header("Stats")]
    [SerializeField] private TextMeshProUGUI statsDanioTMP;
    [SerializeField] private TextMeshProUGUI statsDefensaTMP;
    [SerializeField] private TextMeshProUGUI statsCriticoTMP;
    [SerializeField] private TextMeshProUGUI statsBloqueoTMP;
    [SerializeField] private TextMeshProUGUI statsVelocidadTMP;
    [SerializeField] private TextMeshProUGUI statsNivelTMP;
    [SerializeField] private TextMeshProUGUI statsExpTMP;
    [SerializeField] private TextMeshProUGUI statsExpRequeridaTMP;
    [SerializeField] private TextMeshProUGUI atributoFuerzaTMP;
    [SerializeField] private TextMeshProUGUI atributoInteligenciaTMP;
    [SerializeField] private TextMeshProUGUI atributoDestrezaTMP;
    [SerializeField] private TextMeshProUGUI atributosDisponiblesTMP;

    private float vidaActual;
    private float vidaMax;
    private float manaActual;
    private float manaMax;

    private float expActual;
    private float expRequeridaNuevoNivel;
    
    // Update is called once per frame
    void Update()
    {
        ActualizarUIPersonaje();
        ActualizarPanelStats();
    }

    private void ActualizarUIPersonaje()
    {
        vidaPlayer.fillAmount = Mathf.Lerp(vidaPlayer.fillAmount, vidaActual / vidaMax, 10f * Time.deltaTime);
        manaPlayer.fillAmount = Mathf.Lerp(manaPlayer.fillAmount, manaActual / manaMax, 10f * Time.deltaTime);
        expPlayer.fillAmount  = Mathf.Lerp(expPlayer.fillAmount, expActual / expRequeridaNuevoNivel, 10f * Time.deltaTime);
        vidaTMP.text          = $"{vidaActual} / {vidaMax}";  
        manaTMP.text          = $"{manaActual} / {manaMax}";
        expTMP.text           = $"{((expActual/expRequeridaNuevoNivel) * 100):F2}%";
        nivelTMP.text         = $"Nivel {stats.Nivel}";
        monedasTMP.text       = MonedaManager.Instance.MonedasTotales.ToString();
    }

    private void ActualizarPanelStats()
    {
        if (panelStats.activeSelf == false) 
        {
            return;
        }
        statsDanioTMP.text        = stats.Danio.ToString();
        statsDefensaTMP.text      = stats.Defensa.ToString();
        statsCriticoTMP.text      = $"{stats.PorcentajeCritico}%";
        statsBloqueoTMP.text      = $"{stats.PorcentajeBloqueo}%";
        statsVelocidadTMP.text    = stats.Velocidad.ToString();
        statsNivelTMP.text        = stats.Nivel.ToString();
        statsExpTMP.text          = stats.ExpActual.ToString();
        statsExpRequeridaTMP.text = stats.ExpRequeridaSiguienteNivel.ToString();

        atributoFuerzaTMP.text      = stats.Fuerza.ToString();
        atributoInteligenciaTMP.text= stats.Inteligencia.ToString();
        atributoDestrezaTMP.text    = stats.Destreza.ToString();
        atributosDisponiblesTMP.text= $"Puntos: { stats.PuntosDisponibles }";
    }

    public void ActualizarVidaPersonaje(float pVidaActual, float pVidaMax)
    {
        vidaActual = pVidaActual;
        vidaMax    = pVidaMax;
    }
    public void ActualizarManaPersonaje(float pManaActual, float pManaMax)
    {
        manaActual = pManaActual;
        manaMax    = pManaMax;
    }

    public void ActualizarExpPersonaje(float pExpActual, float pExpRequerida)
    {
        expActual              = pExpActual;
        expRequeridaNuevoNivel = pExpRequerida;
    }

    #region Paneles
    public void AbrirCerrarPanelStats()
    {
        panelStats.SetActive(!panelStats.activeSelf);
    }

    public void AbrirCerrarPanelInventario()
    {
        panelInventario.SetActive(!panelInventario.activeSelf);
    }

    public void AbrirCerrarPanelPersonajeQuest()
    {
        panelPersonajeQuests.SetActive(!panelPersonajeQuests.activeSelf);
    }

    public void AbrirCerrarPanelInspectorQuests()
    {
        panelInspectorQuests.SetActive(!panelInspectorQuests.activeSelf);
    }

    public void AbrirPanelInteraccion(InteraccionExtraNPC tipoInteraccion)
    {
        switch (tipoInteraccion)
        {
            case InteraccionExtraNPC.Quests:
                AbrirCerrarPanelInspectorQuests();
                break;
            
            case InteraccionExtraNPC.Tienda:
                break;
            
            case InteraccionExtraNPC.Crafting:
                break;

        }
    }

    #endregion
}
