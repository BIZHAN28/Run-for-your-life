using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB; 
    public float speed = 2f; 
	private float plSpeed;
    private bool movingToB = true;
	private Vector3 moveDirection;

    private void FixedUpdate()
    {
        MovePlatform();
    }

    private void MovePlatform()
    {

        if (movingToB)
        {
			plSpeed = speed;
			moveDirection = Vector3.MoveTowards(transform.position, pointB.position, speed * Time.deltaTime);
            transform.position = moveDirection;
            if (Vector3.Distance(transform.position, pointB.position) < 0.1f)
            {
                movingToB = false;
            }
        }
        else
        {
			plSpeed = -speed;
			moveDirection = Vector3.MoveTowards(transform.position, pointA.position, speed * Time.deltaTime);
            transform.position = moveDirection;
            if (Vector3.Distance(transform.position, pointA.position) < 0.1f)
            {
                movingToB = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<CharacterController>().Move(new Vector3(0,0,plSpeed * Time.deltaTime));
        }
    }

}
