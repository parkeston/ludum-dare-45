using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomersManager : MonoBehaviour
{
	[SerializeField] private int startSpawncount;
	[SerializeField] private float spawnDelay;
	[SerializeField] private int timesToSpawnAgain;

	private CustomerSpawner spawner;


	private int totalcustomers;
	public int TotalCustomers { get => totalcustomers; set { totalcustomers = value; if (value == 0) GameManager.Instance.CustomersIsOver(); print(value); }}


	private void Awake()
	{
		spawner = GetComponent<CustomerSpawner>();

		TotalCustomers = (timesToSpawnAgain + 1) * startSpawncount;

	}
	private void Start()
	{
		InvokeRepeating(nameof(DelayedSpawn), 0, spawnDelay);
	}

	public void DelayedSpawn()
	{
		if (timesToSpawnAgain == 0)
		{
			CancelInvoke();
		}

		for (int i = 0; i < startSpawncount; i++)
		{
			spawner.SpawnCustomer();
		}

		timesToSpawnAgain--;
		GetComponent<CustomerSpawner>().ClearOffsets();
	}
}
