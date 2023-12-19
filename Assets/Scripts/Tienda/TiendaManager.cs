using System;
using UnityEngine;

public class TiendaManager : MonoBehaviour
{
    [Header("Config")] 
    [SerializeField] private ItemTienda itemTiendaPrefab;
    [SerializeField] private Transform panelContenedor;

    [Header("Items")] 
    [SerializeField] private ItemVenta[] itemsDisponibles;

    private void Start()
    {
        CargarItemEnVenta();
    }

    private void CargarItemEnVenta()
    {
        for (int i = 0; i < itemsDisponibles.Length; i++)
        {
            ItemTienda itemTienda = Instantiate(itemTiendaPrefab, panelContenedor);
            itemTienda.ConfigurarItemVenta(itemsDisponibles[i]);
        }
    }
}