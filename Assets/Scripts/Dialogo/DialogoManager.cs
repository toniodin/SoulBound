using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogoManager : Singleton<DialogoManager>
{
    [SerializeField] private GameObject panelDialogo;
    [SerializeField] private Image npcIcono;
    [SerializeField] private TextMeshProUGUI npcNombreTMP;
    [SerializeField] private TextMeshProUGUI npcConversacionTMP;

    public NPCInteraccion NPCDisponible { get; set; }

    private Queue<string> dialogosSecuencia;
    private bool dialogoAnimado;
    private bool despedidaMostrada;

    private float velocidadAnimacion = 0.03f;
    private void Start()
    {
        dialogosSecuencia = new Queue<string>();
    }

    private void Update()
    {
        if (NPCDisponible == null)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            IniciarNuevaConversacion(NPCDisponible.Dialogo);
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
                ContinuarDialogo();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            AjustarVelocidadAnimacion(true);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            AjustarVelocidadAnimacion(false);
        }
    }

    private void IniciarNuevaConversacion(NPCDialogo npcDialogo)
    {
        LimpiarConversacionAnterior();
        ConfigurarPanel(npcDialogo);
    }

    private void LimpiarConversacionAnterior()
    {
        dialogosSecuencia.Clear();
    }

    private void AjustarVelocidadAnimacion(bool aumentar)
    {
        velocidadAnimacion = aumentar ? 0.0004f : 0.03f;
    }

    public void AbrirCerrarPanelDialogo(bool estado)
    {
        panelDialogo.SetActive(estado);
    }

    private void ConfigurarPanel(NPCDialogo npcDialogo)
    {
        AbrirCerrarPanelDialogo(true);
        CargarDialogosSencuencia(npcDialogo);

        npcIcono.sprite = npcDialogo.Icono;
        npcNombreTMP.text = $"{npcDialogo.Nombre}:";
        MostrarTextoConAnimacion(npcDialogo.Saludo);
    }

    private void CargarDialogosSencuencia(NPCDialogo npcDialogo)
    {
        if (npcDialogo.Conversacion == null || npcDialogo.Conversacion.Length <= 0)
        {
            return;
        }

        for (int i = 0; i < npcDialogo.Conversacion.Length; i++)
        {
            dialogosSecuencia.Enqueue(npcDialogo.Conversacion[i].Oracion);
        }
    }

    private void ContinuarDialogo()
    {
        if (NPCDisponible == null)
        {
            return;
        }

        if (despedidaMostrada)
        {
            return;
        }

        if (dialogosSecuencia.Count == 0)
        {
            string despedida = NPCDisponible.Dialogo.Despedida;
            MostrarTextoConAnimacion(despedida);
            despedidaMostrada = true;
            return;
        }

        string siguienteDialogo = dialogosSecuencia.Dequeue();
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
            yield return new WaitForSeconds(velocidadAnimacion);
        }

        dialogoAnimado = true;
    }

    private void MostrarTextoConAnimacion(string oracion)
    {
        StartCoroutine(AnimarTexto(oracion));
    }
}