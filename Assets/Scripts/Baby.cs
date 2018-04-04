using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class Baby : MonoBehaviour{
 // Normal Movements Variables
	private float walkSpeed;
	private float curSpeed;
	private float maxSpeed;
	private Rigidbody2D rbd2;

	void Start()
	{
		rbd2 = GetComponent<Rigidbody2D> ();
		walkSpeed = 5f;
		curSpeed = 2f;
	}

	void FixedUpdate()
	{
		 curSpeed = walkSpeed;
		 maxSpeed = curSpeed;

		 // Move senteces
		   rbd2.velocity = new Vector2(Mathf.Lerp(0, Input.GetAxis("Horizontal2")* curSpeed, 0.8f),
		                                        Mathf.Lerp(0, Input.GetAxis("Vertical2")* curSpeed, 0.8f));

 	}
 }