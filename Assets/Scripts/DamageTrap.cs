using System.Collections;
using UnityEngine;

public class DamageTrap : MonoBehaviour
{
    private bool isActive = false;
    private float activationDelay = 1f;
    private float resetTime = 5f;

    private Collider playerInTrap;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !isActive)
        {
            playerInTrap = other;
            StartCoroutine(ActivateTrap());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == playerInTrap)
        {
            playerInTrap = null;
        }
    }

    IEnumerator ActivateTrap()
    {
        isActive = true;
        GetComponent<Renderer>().material.color = new Color(0.996f, 0.541f, 0.094f); 

        yield return new WaitForSeconds(activationDelay);

        if (playerInTrap != null) 
        {
            playerInTrap.GetComponent<PlayerController>().TakeDamage(20);
        }
		GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(resetTime);

        GetComponent<Renderer>().material.color = new Color32(196,194,255,1); 
        isActive = false;
    }
}
