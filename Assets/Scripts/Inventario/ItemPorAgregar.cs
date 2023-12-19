
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
            Inventario.Instance.AñadirItem(inventarioItemReferencia, cantidadPorAgregar);
            Destroy(gameObject);
        }
    }
}
