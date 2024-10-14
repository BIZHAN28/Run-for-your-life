using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityRandom = UnityEngine.Random;

public class WindTrap : MonoBehaviour
{
    private Vector3 windDirection;
	private Vector3 multiplaer;
    public float windForce = 2f;
	public Transform fan;
	public Transform center;
    private float changeDirectionTime = 2f;
	private float posX, posZ, length, relation;
	
    void Start()
    {
        StartCoroutine(ChangeWindDirection());
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
			fan.LookAt(center);
			posX = windDirection.x*-1;
			posZ = windDirection.z*-1;
			length = (float) Math.Sqrt(posX * posX + posZ * posZ);
			relation = 1 / length;
			fan.localPosition = new Vector3(posX * relation, 1.3f, posZ * relation);
			
            other.GetComponent<CharacterController>().Move(windDirection * windForce * Time.deltaTime);
        }
    }

    IEnumerator ChangeWindDirection()
    {
        while (true)
        {
			multiplaer = UnityRandom.insideUnitSphere;
            windDirection = new Vector3(multiplaer.x, 0, multiplaer.z);
			
            yield return new WaitForSeconds(changeDirectionTime);
        }
    }
}