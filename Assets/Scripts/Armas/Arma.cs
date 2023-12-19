using UnityEngine;

public enum TipoArma
{
    Magia,
    Melee
}

[CreateAssetMenu(menuName = "Personaje/Arma")]
public class Arma : ScriptableObject
{
    [Header("Config")] 
    public Sprite ArmaIcono;
    public Sprite IconoSkill;
    public TipoArma Tipo;
    public float Daño;

    [Header("Arma Magica")] 
    public Proyectil ProyectilPrefab;
    public float ManaRequerida;

    [Header("Stats")] 
    public float ChanceCritico;
    public float ChanceBloqueo;
}
