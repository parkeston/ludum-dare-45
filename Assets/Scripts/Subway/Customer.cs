using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
	[SerializeField] private float targetRadius;
	[SerializeField] private float timeToTarget;

	[SerializeField] private float maxSpeed;
	[SerializeField] private Vector2 maxSpeedModifierRange;
	[SerializeField] private float maxIdleTime;
	private float idleTime;

	[SerializeField] private bool isPaid;
	public bool IsPaid => isPaid;

	public float TargetRadius => targetRadius;

	private Vector3 destination;
	public bool IsDestinationSet { get; set; }

	private new Rigidbody rigidbody;
	private Animator animator;

	public bool IsInTurniket { get; set; }
	public Turnstile turnstile { get; set; }

	List<Collider> ragdollColliders;


	private void Awake()
	{
		ragdollColliders = new List<Collider>();

		rigidbody = GetComponent<Rigidbody>();
		animator = GetComponent<Animator>();


		maxSpeed *= Random.Range(maxSpeedModifierRange.x, maxSpeedModifierRange.y);

	}

	private void OnMouseDown()
	{
		if (IsInTurniket)
		{
			FlyAway();
			IsInTurniket = false;
			turnstile = null;
		}
	}

	private void Update()
	{
		if (IsDestinationSet)
			GoToDestination();

		animator.SetFloat("Velocity", rigidbody.velocity.magnitude);
		print(rigidbody.velocity);

		if(rigidbody.velocity.magnitude<0.01f)
		{
			if(System.Math.Abs(idleTime) < Mathf.Epsilon)
			{
				idleTime = Time.time + maxIdleTime;
			}
			else if (Time.time>idleTime)
			{
				destination = transform.position + Vector3.forward * 5;
				idleTime = 0;
			}
		}
	}

	public void SetDestination(Vector3 destination)
	{
		this.destination = destination;
		IsDestinationSet = true;
	}

	public void GoToDestination()
	{
		
		Vector3 direction = destination - transform.position;
		direction.y = 0;

		if (direction.magnitude <= targetRadius)
			return;

		Vector3 desiredVelocity = direction / timeToTarget;
		desiredVelocity = Vector3.ClampMagnitude(desiredVelocity, maxSpeed);

		rigidbody.velocity = desiredVelocity + Vector3.up*rigidbody.velocity.y;
		print(rigidbody.velocity);

		transform.rotation = Quaternion.LookRotation(desiredVelocity);
	}

	//set active to false, reuse
	public void FlyAway()
	{
		IsDestinationSet = false;
		gameObject.GetComponent<Collider>().enabled = false;
		rigidbody.AddForce(new Vector3(0, 2, -5) * 5, ForceMode.Impulse);

		turnstile.GetComponent<Animation>().Play();

		GameManager.Instance.ProcessPoints(isPaid);
		GameManager.Instance.Manager.TotalCustomers--;

		Invoke(nameof(Reset), 3);
	}

	private void Reset()
	{
		rigidbody.velocity = Vector3.zero;
		gameObject.GetComponent<Collider>().enabled = true;
		gameObject.SetActive(false);
	}

}
