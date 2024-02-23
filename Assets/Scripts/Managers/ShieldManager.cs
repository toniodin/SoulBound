using System.Collections;
using UnityEngine;

public class ShieldManager : MonoBehaviour
{
    [SerializeField] private Personaje personaje;
    public bool estado { get; set; }

    private Coroutine desactivarEscudoCoroutine; // Referencia a la corrutina
    private Coroutine restarManaCoroutine; // Referencia a la corrutina

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            estado = true;
            personaje.ProtegerPersonaje(estado);
            // Comenzar la corrutina para restar mana cada segundo
            restarManaCoroutine = StartCoroutine(RestarManaCadaSegundo());
        }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            DesprotegerPersonaje();
            // Detener la corrutina para restar mana
            if (restarManaCoroutine != null)
            {
                StopCoroutine(restarManaCoroutine);
            }
        }
    }

    public void DesprotegerPersonaje()
    {
        if (desactivarEscudoCoroutine != null)
        {
            StopCoroutine(desactivarEscudoCoroutine);
        }

        desactivarEscudoCoroutine = StartCoroutine(DesactivarEscudoDespuesDeTiempo(0.50f));
    }

    private IEnumerator DesactivarEscudoDespuesDeTiempo(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);

        estado = false;
        personaje.DesprotegerPersonaje(estado);
    }

    private IEnumerator RestarManaCadaSegundo()
    {
        while (estado)
        {
            RestarMana(0.50f); // Llamar a la función para restar mana cada segundo
            yield return new WaitForSeconds(0.50f);
        }
    }

    private void RestarMana(float tiempo)
    {
        personaje.PersonajeMana.RestarEscudoMana();
    }
}
