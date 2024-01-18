using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("Stats")] 
    [SerializeField] private PersonajeStats stats;
    
    [Header("Paneles")] 
    [SerializeField] private GameObject panelStats;
    [SerializeField] private GameObject panelTienda;
    [SerializeField] private GameObject panelCrafting;
    [SerializeField] private GameObject panelCraftingInfo;
    [SerializeField] private GameObject panelInventario;
    [SerializeField] private GameObject panelInspectorQuests;
    [SerializeField] private GameObject panelPersonajeQuests;
    [SerializeField] private GameObject panelDialogo;

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
    [SerializeField] private TextMeshProUGUI pocionesVidaTMP;
    [SerializeField] private TextMeshProUGUI pocionesManaTMP;

    [Header("Stats")] 
    [SerializeField] private TextMeshProUGUI statDañoTMP;
    [SerializeField] private TextMeshProUGUI statDefensaTMP;
    [SerializeField] private TextMeshProUGUI statCriticoTMP;
    [SerializeField] private TextMeshProUGUI statBloqueoTMP;
    [SerializeField] private TextMeshProUGUI statVelocidadTMP;
    [SerializeField] private TextMeshProUGUI statNivelTMP;
    [SerializeField] private TextMeshProUGUI statExpTMP;
    [SerializeField] private TextMeshProUGUI statExpRequeridaTMP;
    [SerializeField] private TextMeshProUGUI statExpTotalTMP;
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

    private void Update()
    {
        ActualizarUIPersonaje(null, null);
        ActualizarPanelStats();
        AbrirCerrarPanelInventarioTecla();
    }

    public void ActualizarUIPersonaje(int? cantidad, string? itemReferencia)
    {
        vidaPlayer.fillAmount = Mathf.Lerp(vidaPlayer.fillAmount, 
            vidaActual / vidaMax, 10f * Time.deltaTime);
        manaPlayer.fillAmount = Mathf.Lerp(manaPlayer.fillAmount, 
            manaActual / manaMax, 10f * Time.deltaTime);
        expPlayer.fillAmount = Mathf.Lerp(expPlayer.fillAmount, 
            expActual / expRequeridaNuevoNivel, 10f * Time.deltaTime);

        vidaTMP.text = $"{vidaActual}/{vidaMax}";
        manaTMP.text = $"{manaActual}/{manaMax}";
        expTMP.text = $"{((expActual/expRequeridaNuevoNivel) * 100):F2}%";
        nivelTMP.text = $"Nivel {stats.Nivel}";
        monedasTMP.text = MonedasManager.Instance.MonedasTotales.ToString();

        if (itemReferencia == "Pocion de Vida")
        {
            pocionesVidaTMP.text = (int.Parse(pocionesVidaTMP.text) + cantidad).ToString();
        }
        else if (itemReferencia == "Pocion de Mana")
        {
            pocionesManaTMP.text = (int.Parse(pocionesManaTMP.text) + cantidad).ToString();
        }
    }

    private void ActualizarPanelStats()
    {
        if (panelStats.activeSelf == false)
        {
            return;
        }

        statDañoTMP.text = stats.Daño.ToString();
        statDefensaTMP.text = stats.Defensa.ToString();
        statCriticoTMP.text = $"{stats.PorcentajeCritico}%";
        statBloqueoTMP.text = $"{stats.PorcentajeBloqueo}%";
        statVelocidadTMP.text = stats.Velocidad.ToString();
        statNivelTMP.text = stats.Nivel.ToString();
        statExpTMP.text = stats.ExpActual.ToString();
        statExpRequeridaTMP.text = stats.ExpRequeridaSiguienteNivel.ToString();
        statExpTotalTMP.text = stats.ExpTotal.ToString();
        
        atributoFuerzaTMP.text = stats.Fuerza.ToString();
        atributoInteligenciaTMP.text = stats.Inteligencia.ToString();
        atributoDestrezaTMP.text = stats.Destreza.ToString();
        atributosDisponiblesTMP.text = $"Puntos: {stats.PuntosDisponibles}";
    }
    
    public void ActualizarVidaPersonaje(float pVidaActual, float pVidaMax)
    {
        vidaActual = pVidaActual;
        vidaMax = pVidaMax;
    }
    
    public void ActualizarManaPersonaje(float pManaActual, float pManaMax)
    {
        manaActual = pManaActual;
        manaMax = pManaMax;
    }
    
    public void ActualizarExpPersonaje(float pExpActual, float pExpRequerida)
    {
        expActual = pExpActual;
        expRequeridaNuevoNivel = pExpRequerida;
    }

    #region Paneles

    public void AbrirCerrarPanelStats()
    {
        OpenMenuController("Stats");
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
    }

    public void AbrirCerrarPanelTienda()
    {
        panelTienda.SetActive(!panelTienda.activeSelf);
    }

    public void AbriPanelCrafting()
    {
        panelCrafting.SetActive(true);
    }
    
    public void CerrarPanelCrafting()
    {
        panelCrafting.SetActive(false);
        AbrirCerrarPanelCraftingInformacion(false);
    }

    public void AbrirCerrarPanelCraftingInformacion(bool estado)
    {
        panelCraftingInfo.SetActive(estado);
    }

    public void AbrirCerrarPanelInventarioTecla()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            OpenMenuController("Inventario");
        }

    }

    public void AbrirCerrarPanelInventarioClick()
    {
        OpenMenuController("Inventario");
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);

    }
    public void AbrirCerrarPanelPersonajeQuests()
    {
        OpenMenuController("Quest");
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
    }

    public void AbrirCerrarPanelInspectorQuests()
    {
        panelInspectorQuests.SetActive(!panelInspectorQuests.activeSelf);
    }

    public void CerrarPanelDialogo()
    {
        panelDialogo.SetActive(false);
    }

    public void AbrirPanelInteraccion(InteraccionExtraNPC tipoInteraccion)
    {
        switch (tipoInteraccion)
        {
            case InteraccionExtraNPC.Quests:
                AbrirCerrarPanelInspectorQuests();
                break;
            case InteraccionExtraNPC.Tienda:
                AbrirCerrarPanelTienda();
                break;
            case InteraccionExtraNPC.Crafting:
                AbriPanelCrafting();
                break;
        }
    }

    public void OpenMenuController(String nombreBoton)
    {
        if (nombreBoton == "Inventario")
        {
            panelInventario.SetActive(!panelInventario.activeSelf);
            panelStats.SetActive(false);
            panelPersonajeQuests.SetActive(false);
        }
        if (nombreBoton == "Stats")
        {
            panelStats.SetActive(!panelStats.activeSelf);
            panelInventario.SetActive(false);
            panelPersonajeQuests.SetActive(false);
        }
        if (nombreBoton == "Quest")
        {
            panelPersonajeQuests.SetActive(!panelPersonajeQuests.activeSelf);
            panelInventario.SetActive(false);
            panelStats.SetActive(false);
        }


    }

    #endregion
}
