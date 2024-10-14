using System.Collections;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public GameObject crossPrefab;  
    public GameObject spikePrefab;
    public float spikeDelay = 2f;
    public float spikeHeight = 1f;
    public float spikeSpeed = 5f;  

    private GameObject currentCross; 
    private GameObject currentSpike;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 crossPosition = new Vector3(other.transform.position.x, other.transform.position.y-0.2f, other.transform.position.z);
            currentCross = Instantiate(crossPrefab, crossPosition, crossPrefab.transform.rotation);

            StartCoroutine(ActivateSpike(other));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (currentCross != null)
            {
                Destroy(currentCross);
            }

            if (currentSpike != null)
            {
                Destroy(currentSpike);
            }
        }
    }

    IEnumerator ActivateSpike(Collider player)
    {
        yield return new WaitForSeconds(spikeDelay); 

        if (player != null && currentCross != null)
        {

            Vector3 spikeStartPos = currentCross.transform.position;
            Vector3 spikeEndPos = spikeStartPos + Vector3.up * spikeHeight;

            currentSpike = Instantiate(spikePrefab, spikeStartPos, Quaternion.identity);

			if (Vector3.Distance(player.transform.position, spikeEndPos) < 1f)
            {
                player.GetComponent<PlayerController>().TakeDamage(20);
            }
            float timeElapsed = 0f;
            while (timeElapsed < 1f)
            {
                currentSpike.transform.position = Vector3.Lerp(spikeStartPos, spikeEndPos, timeElapsed * spikeSpeed);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            currentSpike.transform.position = spikeEndPos;
			
            yield return new WaitForSeconds(1f);
            if (currentSpike != null)
            {
                Destroy(currentSpike);
            }
        }

        if (currentCross != null)
        {
            Destroy(currentCross);
        }
    }
}
