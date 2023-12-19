using System;
using System.Collections;
using UnityEngine;

public enum TipoPersonaje
{
    Player,
    IA
}

public class PersonajeFX : MonoBehaviour
{
    [Header("Pooler")] 
    [SerializeField] private ObjectPooler pooler;
    
    [Header("Config")] 
    [SerializeField] private GameObject canvasTextoAnimacionPrefab;
    [SerializeField] private Transform canvasTextoPosicion;

    [Header("Tipo")] 
    [SerializeField] private TipoPersonaje tipoPersonaje;

    private EnemigoVida _enemigoVida;

    private void Awake()
    {
        _enemigoVida = GetComponent<EnemigoVida>();
    }

    private void Start()
    {
        pooler.CrearPooler(canvasTextoAnimacionPrefab);
    }

    private IEnumerator IEMostrarTexto(float cantidad, Color color)
    {
        GameObject nuevoTextoGO = pooler.ObtenerInstancia();
        TextoAnimacion texto = nuevoTextoGO.GetComponent<TextoAnimacion>();
        texto.EstablecerTexto(cantidad, color);
        nuevoTextoGO.transform.SetParent(canvasTextoPosicion);
        nuevoTextoGO.transform.position = canvasTextoPosicion.position;
        nuevoTextoGO.SetActive(true);

        yield return new WaitForSeconds(1f);
        nuevoTextoGO.SetActive(false);
        nuevoTextoGO.transform.SetParent(pooler.ListaContenedor.transform);
    }

    private void RespuestaDañoRecibidoHaciaPlayer(float daño)
    {
        if (tipoPersonaje == TipoPersonaje.Player)
        {
            StartCoroutine(IEMostrarTexto(daño, Color.black));
        }
    }

    private void RespuestaDañoHaciaEnemigo(float daño, EnemigoVida enemigoVida)
    {
        if (tipoPersonaje == TipoPersonaje.IA && _enemigoVida == enemigoVida)
        {
            StartCoroutine(IEMostrarTexto(daño, Color.red));
        }
    }
    
    private void OnEnable()
    {
        IAController.EventoDañoRealizado += RespuestaDañoRecibidoHaciaPlayer;
        PersonajeAtaque.EventoEnemigoDañado += RespuestaDañoHaciaEnemigo;
    }

    private void OnDisable()
    {
        IAController.EventoDañoRealizado -= RespuestaDañoRecibidoHaciaPlayer;
        PersonajeAtaque.EventoEnemigoDañado -= RespuestaDañoHaciaEnemigo;
    }
}
