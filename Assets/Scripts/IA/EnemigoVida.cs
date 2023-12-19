using System;
using Unity.VisualScripting;
using UnityEngine;

public class EnemigoVida : VidaBase
{
    public static Action<float> EventoEnemigoDerrotado;
    
    [Header("Vida")]
    [SerializeField] private EnemigoBarraVida barraVidaPrefab;
    [SerializeField] private Transform barraVidaPosicion;

    [Header("Rastros")] 
    [SerializeField] private GameObject rastros;
    
    private EnemigoBarraVida _enemigoBarraVidaCreada;
    private EnemigoInteraccion _enemigoInteraccion;
    private EnemigoMovimiento _enemigoMovimiento;
    private EnemigoLoot _enemigoLoot;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;
    private IAController _controller;
    
    private void Awake()
    {
        _enemigoInteraccion = GetComponent<EnemigoInteraccion>();
        _enemigoMovimiento = GetComponent<EnemigoMovimiento>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _enemigoLoot = GetComponent<EnemigoLoot>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _controller = GetComponent<IAController>();
    }

    protected override void Start()
    {
        base.Start();
        CrearBarraVida();
    }

    private void CrearBarraVida()
    {
        _enemigoBarraVidaCreada = Instantiate(barraVidaPrefab, barraVidaPosicion);
        ActualizarBarraVida(Salud, saludMax);
    }

    protected override void ActualizarBarraVida(float vidaActual, float vidaMax)
    {
        _enemigoBarraVidaCreada.ModificarSalud(vidaActual, vidaMax);
    }

    protected override void PersonajeDerrotado()
    {
        DesactivarEnemigo();
        EventoEnemigoDerrotado?.Invoke(_enemigoLoot.ExpGanada);
        QuestManager.Instance.AñadirProgreso("Mata10", 1);
        QuestManager.Instance.AñadirProgreso("Mata25", 1);
        QuestManager.Instance.AñadirProgreso("Mata50", 1);
    }

    private void DesactivarEnemigo()
    {
        rastros.SetActive(true);
        _enemigoBarraVidaCreada.gameObject.SetActive(false);
        _spriteRenderer.enabled = false;
        _enemigoMovimiento.enabled = false;
        _controller.enabled = false;
        _boxCollider2D.isTrigger = true;
        _enemigoInteraccion.DesactivarSpritesSeleccion();
    }
}