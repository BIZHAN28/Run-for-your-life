using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PressTrap : MonoBehaviour
{
    public Transform pressTopPosition;    
    public Transform pressBottomPosition;
    public float pressSpeed = 5f;
    public float pressDelay = 3f;
    public int pressDamage = 20;

    private bool isPressing = false;
    private bool goingDown = true;
    private Rigidbody rb; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        StartCoroutine(PressCycle());
    }

    void FixedUpdate()
    {
        if (isPressing)
        {
            if (goingDown)
            {
                MovePress(pressBottomPosition.position);
            }
            else
            {
                MovePress(pressTopPosition.position);
            }
        }
    }

    void MovePress(Vector3 targetPosition)
    {
        Vector3 newPosition = Vector3.MoveTowards(rb.position, targetPosition, pressSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);

  
        if (Vector3.Distance(rb.position, targetPosition) < 0.1f)
        {
            isPressing = false; 
        }
    }

    IEnumerator PressCycle()
    {
        while (true)
        {
            goingDown = true;
            isPressing = true;

            yield return new WaitUntil(() => !isPressing);

            yield return new WaitForSeconds(1f);

            goingDown = false;
            isPressing = true;

            yield return new WaitUntil(() => !isPressing);

            yield return new WaitForSeconds(pressDelay);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && goingDown)
        {
            other.GetComponent<PlayerController>().TakeDamage(pressDamage);
        }
    }
}
