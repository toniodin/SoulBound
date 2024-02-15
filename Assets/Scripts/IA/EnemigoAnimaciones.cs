using System;
using UnityEngine;

public class EnemigoAnimaciones : MonoBehaviour
{
    public enum EstadoEnemigo
    {
        Idle,
        Caminando,
        Atacando
    }

    [SerializeField] private string layerIdle;
    [SerializeField] private string layerCaminar;
    [SerializeField] private string layerAtacar;

    private Animator _animator;
    private EstadoEnemigo _estadoActual;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void ActualizarLayers()
    {
        Debug.Log(_estadoActual);
        switch (_estadoActual)
        {
            case EstadoEnemigo.Idle:
                ActivarLayer(layerIdle);
                break;
            case EstadoEnemigo.Caminando:
                ActivarLayer(layerCaminar);
                break;
            case EstadoEnemigo.Atacando:
                ActivarLayer(layerAtacar);
                break;
            default:
                break;
        }
    }

    private void ActivarLayer(string nombreLayer)
    {
        for (int i = 0; i < _animator.layerCount; i++)
        {
            _animator.SetLayerWeight(i, 0);
        }

        _animator.SetLayerWeight(_animator.GetLayerIndex(nombreLayer), 1);
    }

    public void SetEstadoIdle()
    {
        _estadoActual = EstadoEnemigo.Idle;
        ActualizarLayers();
    }
}
