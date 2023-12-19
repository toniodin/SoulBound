using System;
using UnityEngine;

public enum TipoAtributo
{
    Fuerza,
    Inteligencia,
    Destreza
}

public class AtributoButton : MonoBehaviour
{
    public static Action<TipoAtributo> EventoAgregarAtributo;
    [SerializeField] private TipoAtributo tipo;

    public void AgregarAtributo()
    {
        EventoAgregarAtributo?.Invoke(tipo);
    }
}
