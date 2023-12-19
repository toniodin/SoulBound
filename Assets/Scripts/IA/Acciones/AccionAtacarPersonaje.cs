using UnityEngine;

[CreateAssetMenu(menuName = "IA/Acciones/Atacar Personaje")]
public class AccionAtacarPersonaje : IAAccion
{
    public override void Ejecutar(IAController controller)
    {
        Atacar(controller);
    }

    private void Atacar(IAController controller)
    {
        if (controller.PersonajeReferencia == null)
        {
            return;
        }

        if (controller.PersonajeReferencia.GetComponent<PersonajeVida>().Derrotado)
        {
            return;
        }
        
        if (controller.EsTiempoDeAtacar() == false)
        {
            return;
        }

        if (controller.PersonajeEnRangoDeAtaque(controller.RangoDeAtaqueDeterminado))
        {
            if (controller.TipoAtaque == TiposDeAtaque.Embestida)
            {
                controller.AtaqueEmbestida(controller.Daño);
            }
            else
            {
                controller.AtaqueMelee(controller.Daño);
            }
            
            controller.ActualizarTiempoEntreAtaques();
        }
    }
}
