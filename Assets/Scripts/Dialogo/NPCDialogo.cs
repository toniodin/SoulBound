using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteraccionExtraNPC
{
    Quests,
    Tienda,
    Crafting
}

[CreateAssetMenu]
public class NPCDialogo : ScriptableObject
{
    [Header("Info")] 
    public string Nombre;
    public Sprite Icono;
    public bool ContieneInteraccionExtra;
    public InteraccionExtraNPC InteraccionExtra;

    [Header("Saludo")] 
    [TextArea] public string Saludo;

    [Header("Chat")] 
    public DialogoTexto[] Conversacion;
    
    [Header("Despedida")] 
    [TextArea] public string Despedida;
}

[Serializable]
public class DialogoTexto
{
    [TextArea] public string Oracion;
}