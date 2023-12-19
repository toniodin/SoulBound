using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Pocion Vida")]
public class ItemPocionVida : InventarioItem
{
    [Header("Pocion info")] 
    public float HPRestauracion;

    public override bool UsarItem()
    {
        if (Inventario.Instance.Personaje.PersonajeVida.PuedeSerCurado)
        {
            Inventario.Instance.Personaje.PersonajeVida.RestaurarSalud(HPRestauracion);
            return true;
        }

        return false;
    }

    public override string DescripcionItemCrafting()
    {
        string descripcion = $"Restaura {HPRestauracion} de Salud";
        return descripcion;
    }
}