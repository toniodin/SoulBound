using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/Decisiones/Detectar Personaje")]
public class DecisionDetectarPersonaje : IADecision
{
    public override bool Decidir(IAController controller)
    {
        return DetectarPersonaje(controller);
    }

    private bool DetectarPersonaje(IAController controller)
    {
        Collider2D personajeDetectado = Physics2D.OverlapCircle(controller.transform.position,
            controller.RangoDeteccion, controller.PersonajeLayerMask);
        if (personajeDetectado != null)
        {
            controller.PersonajeReferencia = personajeDetectado.transform;
            return true;
        }

        controller.PersonajeReferencia = null;
        return false;
    }
}
