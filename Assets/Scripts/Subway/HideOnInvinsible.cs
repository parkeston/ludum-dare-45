using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOnInvinsible : MonoBehaviour
{
	private void OnBecameInvisible()
	{
		print(transform.parent.gameObject.name + "hide on invinsible!");

		transform.parent.gameObject.SetActive(false);
	}
}
