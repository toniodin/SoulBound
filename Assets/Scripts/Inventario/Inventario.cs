using System.Collections.Generic;
using BayatGames.SaveGameFree;
using Unity.VisualScripting;
using UnityEngine;

public class Inventario : Singleton<Inventario>
{
    [Header("Items")] 
    [SerializeField] private InventarioAlmacen inventarioAlmacen;
    [SerializeField] private InventarioItem[] itemsInventario;
    [SerializeField] private Personaje personaje;
    [SerializeField] private int numeroDeSlots;

    public Personaje Personaje => personaje;
    public int NumeroDeSlots => numeroDeSlots;
    public InventarioItem[] ItemsInventario => itemsInventario;

    private readonly string INVENTARIO_KEY = "MiJuegoMiInventario105205120";
    
    private void Start()
    {
        itemsInventario = new InventarioItem[numeroDeSlots];
        CargarInventario();
    }

    public void AñadirItem(InventarioItem itemPorAñadir, int cantidad)
    {
        if (itemPorAñadir == null)
        {
            return;
        }

        // Verificacion en caso tener ya un item similar en inventario
        List<int> indexes = VerificarExistencias(itemPorAñadir.ID);
        if (itemPorAñadir.EsAcumulable)
        {
            if (indexes.Count > 0)
            {
                for (int i = 0; i < indexes.Count; i++)
                {
                    if (itemsInventario[indexes[i]].Cantidad < itemPorAñadir.AcumulacionMax)
                    {
                        itemsInventario[indexes[i]].Cantidad += cantidad;
                        if (itemsInventario[indexes[i]].Cantidad > itemPorAñadir.AcumulacionMax)
                        {
                            int diferencia = itemsInventario[indexes[i]].Cantidad - itemPorAñadir.AcumulacionMax;
                            itemsInventario[indexes[i]].Cantidad = itemPorAñadir.AcumulacionMax;
                            AñadirItem(itemPorAñadir, diferencia);
                        }
                        
                        InventarioUI.Instance.DibujarItemEnInventario(itemPorAñadir, 
                            itemsInventario[indexes[i]].Cantidad, indexes[i]);
                        return;
                    }
                }
            }
        }

        if (cantidad <= 0)
        {
            return;
        }

        if (cantidad > itemPorAñadir.AcumulacionMax)
        {
            AñadirItemEnSlotDisponible(itemPorAñadir, itemPorAñadir.AcumulacionMax);
            cantidad -= itemPorAñadir.AcumulacionMax;
            AñadirItem(itemPorAñadir, cantidad);
        }
        else
        {
            AñadirItemEnSlotDisponible(itemPorAñadir, cantidad);
        }
        
        GuardarInventario();
    }

    private List<int> VerificarExistencias(string itemID)
    {
        List<int> indexesDelItem = new List<int>();
        for (int i = 0; i < itemsInventario.Length; i++)
        {
            if (itemsInventario[i] != null)
            {
                if (itemsInventario[i].ID == itemID) 
                {
                    indexesDelItem.Add(i);
                }
            }
        }

        return indexesDelItem;
    }

    public int ObtenerCantidadDeItems(string itemID)
    {
        List<int> indexes = VerificarExistencias(itemID);
        int cantidadTotal = 0;
        foreach (int index in indexes)
        {
            if (itemsInventario[index].ID == itemID)
            {
                cantidadTotal += itemsInventario[index].Cantidad;
            }
        }

        return cantidadTotal;
    }

    public void ConsumirItem(string itemID)
    {
        List<int> indexes = VerificarExistencias(itemID);
        if (indexes.Count > 0)
        {
            EliminarItem(indexes[indexes.Count - 1]);
        }
    }
    
    private void AñadirItemEnSlotDisponible(InventarioItem item, int cantidad)
    {
        for (int i = 0; i < itemsInventario.Length; i++)
        {
            if (itemsInventario[i] == null)
            {
                itemsInventario[i] = item.CopiarItem();
                itemsInventario[i].Cantidad = cantidad;
                InventarioUI.Instance.DibujarItemEnInventario(item, cantidad, i);
                return;
            }
        }
    }

    private void EliminarItem(int index)
    {
        ItemsInventario[index].Cantidad--;
        if (itemsInventario[index].Cantidad <= 0)
        {
            itemsInventario[index].Cantidad = 0;
            itemsInventario[index] = null;
            InventarioUI.Instance.DibujarItemEnInventario(null, 0, index);
        }
        else
        {
            InventarioUI.Instance.DibujarItemEnInventario(itemsInventario[index], 
                itemsInventario[index].Cantidad, index);
        }
        
        GuardarInventario();
    }

    public void MoverItem(int indexInicial, int indexFinal)
    {
        if (itemsInventario[indexInicial] == null || itemsInventario[indexFinal] != null)
        {
            return;
        }
        
        // Copiar item en slot final
        InventarioItem itemPorMover = itemsInventario[indexInicial].CopiarItem();
        itemsInventario[indexFinal] = itemPorMover;
        InventarioUI.Instance.DibujarItemEnInventario(itemPorMover, itemPorMover.Cantidad, indexFinal);
        
        // Borramos Item de Slot inicial
        itemsInventario[indexInicial] = null;
        InventarioUI.Instance.DibujarItemEnInventario(null, 0, indexInicial);
        
        GuardarInventario();
    }
    
    private void UsarItem(int index)
    {
        if (itemsInventario[index] == null)
        {
            return;
        }

        if (itemsInventario[index].UsarItem())
        {
            EliminarItem(index);
        }
    }

    private void EquiparItem(int index)
    {
        if (itemsInventario[index] == null)
        {
            return;
        }

        if (itemsInventario[index].Tipo != TiposDeItem.Armas)
        {
            return;
        }

        itemsInventario[index].EquiparItem();
    }

    private void RemoverItem(int index)
    {
        if (itemsInventario[index] == null)
        {
            return;
        }
        
        if (itemsInventario[index].Tipo != TiposDeItem.Armas)
        {
            return;
        }

        itemsInventario[index].RemoverItem();
    }

    #region Guardado

    private InventarioItem ItemExisteEnAlmacen(string ID)
    {
        for (int i = 0; i < inventarioAlmacen.Items.Length; i++)
        {
            if (inventarioAlmacen.Items[i].ID == ID)
            {
                return inventarioAlmacen.Items[i];
            }
        }

        return null;
    }
    
    private InventarioData dataGuardado;
    private void GuardarInventario()
    {
        dataGuardado = new InventarioData();
        dataGuardado.ItemsDatos = new string[numeroDeSlots];
        dataGuardado.ItemsCantidad = new int[numeroDeSlots];

        for (int i = 0; i < numeroDeSlots; i++)
        {
            if (itemsInventario[i] == null || string.IsNullOrEmpty(itemsInventario[i].ID))
            {
                dataGuardado.ItemsDatos[i] = null;
                dataGuardado.ItemsCantidad[i] = 0;
            }
            else
            {
                dataGuardado.ItemsDatos[i] = itemsInventario[i].ID;
                dataGuardado.ItemsCantidad[i] = itemsInventario[i].Cantidad;
            }
        }
        
        SaveGame.Save(INVENTARIO_KEY, dataGuardado);
    }

    private InventarioData dataCargado;
    private void CargarInventario()
    {
        if (SaveGame.Exists(INVENTARIO_KEY))
        {
            dataCargado = SaveGame.Load<InventarioData>(INVENTARIO_KEY);
            for (int i = 0; i < numeroDeSlots; i++)
            {
                if (dataCargado.ItemsDatos[i] != null)
                {
                    InventarioItem itemAlmacen = ItemExisteEnAlmacen(dataCargado.ItemsDatos[i]);
                    if (itemAlmacen != null)
                    {
                        itemsInventario[i] = itemAlmacen.CopiarItem();
                        itemsInventario[i].Cantidad = dataCargado.ItemsCantidad[i];
                        InventarioUI.Instance.DibujarItemEnInventario(itemsInventario[i], 
                            itemsInventario[i].Cantidad, i);
                    }
                }
                else
                {
                    itemsInventario[i] = null;
                }
            }
        }
    }
    
    #endregion
    
    #region Eventos

    private void SlotInteraccionRespuesta(TipoDeInteraccion tipo, int index)
    {
        switch (tipo)
        {
            case TipoDeInteraccion.Usar:
                UsarItem(index);
                break;
            case TipoDeInteraccion.Equipar:
                EquiparItem(index);
                break;
            case TipoDeInteraccion.Remover:
                RemoverItem(index);
                break;
        }
    }
    
    private void OnEnable()
    {
        InventarioSlot.EventoSlotInteraccion += SlotInteraccionRespuesta;
    }

    private void OnDisable()
    {
        InventarioSlot.EventoSlotInteraccion -= SlotInteraccionRespuesta;
    }

    #endregion
}
