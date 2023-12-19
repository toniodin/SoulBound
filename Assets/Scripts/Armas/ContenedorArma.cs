using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ContenedorArma : Singleton<ContenedorArma>
{
    [SerializeField] private Image armaIcono;
    [SerializeField] private Image armaSkillIcono;

    public ItemArma ArmaEquipada { get; set; }
    
    public void EquiparArma(ItemArma itemArma)
    {
        ArmaEquipada = itemArma;
        armaIcono.sprite = itemArma.Arma.ArmaIcono;
        armaIcono.gameObject.SetActive(true);

        if (itemArma.Arma.Tipo == TipoArma.Magia)
        {
            armaSkillIcono.sprite = itemArma.Arma.IconoSkill;
            armaSkillIcono.gameObject.SetActive(true);
        }
        
        Inventario.Instance.Personaje.PersonajeAtaque.EquiparArma(itemArma);
    }

    public void RemoverArma()
    {
        armaIcono.gameObject.SetActive(false);
        armaSkillIcono.gameObject.SetActive(false);
        ArmaEquipada = null;
        Inventario.Instance.Personaje.PersonajeAtaque.RemoverArma();
    }
}
