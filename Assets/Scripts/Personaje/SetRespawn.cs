using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRespawn : MonoBehaviour
{
    [SerializeField] private GameObject pozoButtonInteractuar;
    [SerializeField] private Personaje _personaje;

    public Personaje Personaje => _personaje;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && pozoButtonInteractuar.activeSelf)
        {
            SetearPuntoRespawn();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            pozoButtonInteractuar.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            pozoButtonInteractuar.SetActive(false);
        }
    }

    private void SetearPuntoRespawn()
    {
        if (_personaje != null)
        {
            _personaje.SetearPuntoRespawn(transform.position);
        }
    }
}