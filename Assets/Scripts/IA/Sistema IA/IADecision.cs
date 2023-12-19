using UnityEngine;

public abstract class IADecision : ScriptableObject
{
    public abstract bool Decidir(IAController controller);
}
