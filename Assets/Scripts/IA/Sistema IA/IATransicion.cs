using System;
using UnityEngine;

[Serializable]
public class IATransicion
{
    public IADecision Decision;
    public IAEstado EstadoVerdadero;
    public IAEstado EstadoFalso;
}
