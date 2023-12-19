using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Transform nuevaPos;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.localPosition = nuevaPos.position;
        }
    }
}
