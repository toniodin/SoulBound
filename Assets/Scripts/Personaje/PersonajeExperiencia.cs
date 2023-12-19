using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeExperiencia : MonoBehaviour
{
    [Header("Stas")] 
    [SerializeField] private PersonajeStats stats;
    
    [Header("Config")]
    [SerializeField] private int nivelMax;
    [SerializeField] private int expBase;
    [SerializeField] private int valorIncremental;

    private float expActual;
    private float expRequeridaSiguienteNivel;
    
    private void Start()
    {
        stats.Nivel = 1;
        expRequeridaSiguienteNivel = expBase;
        stats.ExpRequeridaSiguienteNivel = expRequeridaSiguienteNivel;
        ActualizarBarraExp();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            AñadirExperiencia(2f);
        }
    }

    public void AñadirExperiencia(float expObtenida)
    {
        if (expObtenida <= 0) return;
        expActual += expObtenida;
        stats.ExpActual = expActual;

        if (expActual == expRequeridaSiguienteNivel)
        {
            ActualizarNivel();
        }
        else if (expActual > expRequeridaSiguienteNivel)
        {
            float dif = expActual - expRequeridaSiguienteNivel;
            ActualizarNivel();
            AñadirExperiencia(dif);
        }

        stats.ExpTotal += expObtenida;
        ActualizarBarraExp();
    }

    private void ActualizarNivel()
    {
        if (stats.Nivel < nivelMax)
        {
            stats.Nivel++;
            stats.ExpActual = 0;
            expActual = 0;
            expRequeridaSiguienteNivel *= valorIncremental;
            stats.ExpRequeridaSiguienteNivel = expRequeridaSiguienteNivel;
            stats.PuntosDisponibles += 3;
        }
    }

    private void ActualizarBarraExp()
    {
        UIManager.Instance.ActualizarExpPersonaje(expActual, expRequeridaSiguienteNivel);
    }
    
    private void RespuestaEnemigoDerrotado(float exp)
    {
        AñadirExperiencia(exp);
    }
    
    private void OnEnable()
    {
        EnemigoVida.EventoEnemigoDerrotado += RespuestaEnemigoDerrotado;
    }

    private void OnDisable()
    {
        EnemigoVida.EventoEnemigoDerrotado -= RespuestaEnemigoDerrotado;
    }
}
