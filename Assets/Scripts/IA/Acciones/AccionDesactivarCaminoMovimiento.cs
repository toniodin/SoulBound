using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/Acciones/Desactivar Camino Movimiento")]
public class AccionDesactivarCaminoMovimiento : IAAccion
{
    public override void Ejecutar(IAController controller)
    {
        if (controller.EnemigoMovimiento == null)
        {
            return;
        }

        controller.EnemigoMovimiento.enabled = false;
    }
}
