using UnityEngine;

public class LootManager : Singleton<LootManager>
{
    [Header("Config")]
    [SerializeField] private GameObject panelLoot;
    [SerializeField] private LootButton lootButtonPrefab;
    [SerializeField] private Transform lootContenedor;
    
    public void MostrarLoot(EnemigoLoot enemigoLoot)
    {
        panelLoot.SetActive(true);
        if (ContenedorOcupado())
        {
            foreach (Transform hijo in lootContenedor.transform)
            {
                Destroy(hijo.gameObject);
            }
        }

        for (int i = 0; i < enemigoLoot.LootSeleccionado.Count; i++)
        {
            CargarLootPanel(enemigoLoot.LootSeleccionado[i]);
        }
    }

    public void CerrarPanel()
    {
        panelLoot.SetActive(false);
    }
    
    private void CargarLootPanel(DropItem dropItem)
    {
        if (dropItem.ItemRecogido)
        {
            return;
        }

        LootButton loot = Instantiate(lootButtonPrefab, lootContenedor);
        loot.ConfigurarLootItem(dropItem);
        loot.transform.SetParent(lootContenedor);
    }

    private bool ContenedorOcupado()
    {
        LootButton[] hijos = lootContenedor.GetComponentsInChildren<LootButton>();
        if (hijos.Length > 0)
        {
            return true;
        }

        return false;
    }
}
