using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeDetector : MonoBehaviour
{
    public static Action<EnemigoInteraccion> EventoEnemigoDetectado;
    public static Action EventoEnemigoPerdido;

    public EnemigoInteraccion EnemigoDetectado { get; private set; }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemigo"))
        {
            EnemigoDetectado = other.GetComponent<EnemigoInteraccion>();
            if (EnemigoDetectado.GetComponent<EnemigoVida>().Salud > 0)
            {
                EventoEnemigoDetectado?.Invoke(EnemigoDetectado);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemigo"))
        {
            EventoEnemigoPerdido?.Invoke();
        }
    }
}
