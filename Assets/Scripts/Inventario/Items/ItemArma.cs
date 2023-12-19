using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Arma")]
public class ItemArma : InventarioItem
{
    [Header("Arma")] 
    public Arma Arma;

    public override bool EquiparItem()
    {
        if (ContenedorArma.Instance.ArmaEquipada != null)
        {
            return false;
        }
        
        ContenedorArma.Instance.EquiparArma(this);
        return true;
    }

    public override bool RemoverItem()
    {
        if (ContenedorArma.Instance.ArmaEquipada == null)
        {
            return false;
        }
        
        ContenedorArma.Instance.RemoverArma();
        return true;
    }

    public override string DescripcionItemCrafting()
    {
        string descripcion = $"- Chance Critico: {Arma.ChanceCritico}%\n" +
                             $"- Chance Bloqueo: {Arma.ChanceBloqueo}%";
        return descripcion;
    }
}
