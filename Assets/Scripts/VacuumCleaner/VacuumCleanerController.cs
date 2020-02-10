using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumCleanerController : MonoBehaviour
{
	[SerializeField] private float speed;

	private bool isMoving;
	private CharacterController controller;

	private Vector3 direction;

	private void Awake()
	{
		controller = GetComponent<CharacterController>();
	}

	private void Update()
	{
		if (!isMoving && Input.touchCount>0)
		{
			var touch = Input.GetTouch(0);
			var swipeVelocity = touch.deltaPosition / touch.deltaTime;
			float hDirection =0, vDirection = 0;

			if (swipeVelocity.magnitude >= 300)
			{
				if (swipeVelocity.x / swipeVelocity.y >= 3)
					hDirection = Mathf.Clamp(swipeVelocity.x, -1, 1);
				else if (swipeVelocity.y / swipeVelocity.x >= 2)
					vDirection = Mathf.Clamp(swipeVelocity.y, -1, 1);
			}
			
			direction = new Vector3(hDirection, 0, vDirection);

			if (direction != Vector3.zero)
			{
				isMoving = true;
				controller.Move(direction * speed * Time.deltaTime);
			}
		}
		else
			controller.Move(direction * speed * Time.deltaTime);
	}

	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (hit.collider.CompareTag("Walls"))
		{
			isMoving = false;
			direction = Vector3.zero;
		}
	}
}
