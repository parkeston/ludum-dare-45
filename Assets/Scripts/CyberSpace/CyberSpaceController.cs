using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyberSpaceController : MonoBehaviour
{
	[SerializeField] private float maxVelocity;
	[SerializeField] private float velocity;
	[SerializeField] private Vector3 playerSize;

	private Vector3 direction;

	private new Rigidbody rigidbody;

	private new  Camera camera;
	//private Vector3 playerSize;

	private float frustrumHeight, frustrumWidth;

	Touch touch;

    void Awake()
    {
		rigidbody = GetComponent<Rigidbody>();

		camera = Camera.main;

		frustrumHeight = camera.orthographicSize;
		frustrumWidth = frustrumHeight * camera.aspect;

	}

    // Update is called once per frame
    void Update()
    {
		if (Input.touchCount > 0)
		{
			touch = Input.GetTouch(0);
			direction = touch.deltaPosition.normalized;
		}
	}

	private void FixedUpdate()
	{
		rigidbody.AddForce(direction * velocity);
		rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxVelocity);


		Vector3 position = new Vector3(Mathf.Clamp(transform.position.x, -frustrumWidth + playerSize.x / 1.5f, frustrumWidth - playerSize.x / 1.5f),
			Mathf.Clamp(transform.position.y, -frustrumHeight + playerSize.y / 1.5f, frustrumHeight - playerSize.y / 1.5f), transform.position.z);

		rigidbody.MovePosition(position);
		

	}
}
