using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemigoLoot : MonoBehaviour
{
 [Header("Exp")] 
    [SerializeField] private float expGanada;

    [Header("Loot")] 
    [SerializeField] private DropItem[] lootDisponible;

    private List<DropItem> lootSeleccionado = new List<DropItem>();
    public List<DropItem> LootSeleccionado => lootSeleccionado;
    public float ExpGanada => expGanada;
    
    private void Start()
    {
        SeleccionarLoot();
    }

    private void SeleccionarLoot()
    {
        foreach (DropItem item in lootDisponible)
        {
            float probabilidad = Random.Range(0, 100);
            if (probabilidad <= item.PorcentajeDrop)
            {
                lootSeleccionado.Add(item);
            }
        }
    }
}