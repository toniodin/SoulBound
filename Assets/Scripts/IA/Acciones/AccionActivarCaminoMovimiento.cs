using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/Acciones/Activar Camino Movimiento")]
public class AccionActivarCaminoMovimiento : IAAccion
{
    public override void Ejecutar(IAController controller)
    {
        if (controller.EnemigoMovimiento == null)
        {
            return;
        }

        controller.EnemigoMovimiento.enabled = true;
    }
}
