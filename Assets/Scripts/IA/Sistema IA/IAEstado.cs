using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/Estado")]
public class IAEstado : ScriptableObject
{
    public IAAccion[] Acciones;
    public IATransicion[] Transiciones;

    public void EjecutarEstado(IAController controller)
    {
        EjecutarAcciones(controller);
        RealizarTransiciones(controller);
    }
    
    private void EjecutarAcciones(IAController controller)
    {
        if (Acciones == null || Acciones.Length <= 0)
        {
            return;
        }
        
        for (int i = 0; i < Acciones.Length; i++)
        {
            Acciones[i].Ejecutar(controller);
        }
    }

    private void RealizarTransiciones(IAController controller)
    {
        if (Transiciones == null || Transiciones.Length <= 0)
        {
            return;
        }

        for (int i = 0; i < Transiciones.Length; i++)
        {
            bool decisionValor = Transiciones[i].Decision.Decidir(controller);
            if (decisionValor)
            {
                controller.CambiarEstado(Transiciones[i].EstadoVerdadero);
            }
            else
            {
                controller.CambiarEstado(Transiciones[i].EstadoFalso);
            }
        }
    }
}
