
using UnityEngine;

public class ItemPorAgregar : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private InventarioItem inventarioItemReferencia;
    [SerializeField] private int cantidadPorAgregar;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (inventarioItemReferencia.EsConsumible && (inventarioItemReferencia.Nombre == "Pocion de Vida" || inventarioItemReferencia.Nombre == "Pocion de Mana"))
            {
                UIManager.Instance.ActualizarUIPersonaje(cantidadPorAgregar, inventarioItemReferencia.Nombre);
                Inventario.Instance.AñadirItem(inventarioItemReferencia, cantidadPorAgregar);
            }
            else 
            {
                Inventario.Instance.AñadirItem(inventarioItemReferencia, cantidadPorAgregar);
            }
            Destroy(gameObject);

        }
    }
}
