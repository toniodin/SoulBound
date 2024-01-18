using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraccion : MonoBehaviour
{
    [SerializeField] private GameObject npcButtonInteractuar;
    [SerializeField] private NPCDialogo npcDialogo;

    public NPCDialogo Dialogo => npcDialogo;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DialogoManager.Instance.NPCDisponible = this;
            npcButtonInteractuar.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DialogoManager.Instance.NPCDisponible = null;
            npcButtonInteractuar.SetActive(false);
        }
    }
}
