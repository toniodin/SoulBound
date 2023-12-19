using System;
using UnityEngine;

[CreateAssetMenu]
public class Quest : ScriptableObject
{
    public static Action<Quest> EventoQuestCompletado;

    [Header("Info")] 
    public string Nombre;
    public string ID;
    public int CantidadObjetivo;

    [Header("Descripcion")] 
    [TextArea] public string Descripcion;

    [Header("Recompensas")] 
    public int RecompensaOro;
    public float RecompensaExp;
    public QuestRecompensaItem RecompensaItem;

    [HideInInspector] public int CantidadActual;
    [HideInInspector] public bool QuestCompletadoCheck;
    [HideInInspector] public bool QuestAceptado;

    public void AñadirProgreso(int cantidad)
    {
        CantidadActual += cantidad;
        VerificarQuestCompletado();
    }

    private void VerificarQuestCompletado()
    {
        if (CantidadActual >= CantidadObjetivo)
        {
            CantidadActual = CantidadObjetivo;
            QuestCompletado();
        }
    }

    private void QuestCompletado()
    {
        if (QuestCompletadoCheck)
        {
            return;
        }

        QuestCompletadoCheck = true;
        EventoQuestCompletado?.Invoke(this);
    }

    public void ResetQuest()
    {
        QuestCompletadoCheck = false;
        CantidadActual = 0;
    }
}

[Serializable]
public class QuestRecompensaItem
{
    public InventarioItem Item;
    public int Cantidad;
}
