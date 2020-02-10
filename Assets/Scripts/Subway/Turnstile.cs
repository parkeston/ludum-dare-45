using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turnstile : MonoBehaviour
{
	[SerializeField] private float awaydistance;

	private Customer enteredCustomer;

	private void OnTriggerEnter(Collider other)
	{
		enteredCustomer = other.GetComponent<Customer>();
		if (enteredCustomer != null)
		{
			enteredCustomer.IsInTurniket = true;
			enteredCustomer.turnstile = this;
			MoveQueue(other.GetComponent<Customer>(), transform.position + Vector3.forward * awaydistance);
		}
	}


	private void OnTriggerExit(Collider other)
	{
		var customer = other.GetComponent<Customer>();

		if (customer!= null)
		{
			customer.IsInTurniket = false;
			customer.turnstile = null;
			GameManager.Instance.ProcessPoints(!customer.IsPaid);
			GameManager.Instance.Manager.TotalCustomers--;
		}
	}

	private void MoveQueue(Customer customer, Vector3 position)
	{
		if (customer == null)
			return;

		customer.SetDestination(position);

		Vector3 castPos = transform.position;
		castPos.z = customer.transform.position.z;

		if (Physics.BoxCast(castPos, new Vector3(3, 1, 1), Vector3.back, out RaycastHit hit, Quaternion.identity, 5))
			MoveQueue(hit.collider.GetComponent<Customer>(), castPos+ Vector3.forward*customer.TargetRadius);

	}
}
