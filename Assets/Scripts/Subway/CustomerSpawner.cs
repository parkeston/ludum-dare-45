using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
	[SerializeField] int poolCount;
	[SerializeField] Transform[] spawnPositions;
	[SerializeField] Transform[] destinations;
	[SerializeField] GameObject[] customersPrefab;
	[SerializeField] private float queueOffset;

	private List<GameObject> customers;
	private float[] spawnPointsOffsets;
	private float[] destinationOffsets;


    private void Awake()
    {
		customers = new List<GameObject>();
		spawnPointsOffsets = new float[spawnPositions.Length];
		destinationOffsets = new float[destinations.Length];

        for(int i = 0;i<poolCount;i++)
		{
			var customer = Instantiate(customersPrefab[Random.Range(0,customersPrefab.Length)]);
			customer.SetActive(false);

			customers.Add(customer);
		}
    }

	public void SpawnCustomer()
	{
		foreach(var customer in customers)
		{
			if(!customer.activeSelf)
			{
				int i = Random.Range(0, spawnPositions.Length);

				customer.transform.position = spawnPositions[i].position + Vector3.back*spawnPointsOffsets[i];
				spawnPointsOffsets[i] += queueOffset;

				i = Random.Range(0, destinations.Length);

				customer.GetComponent<Customer>().SetDestination(destinations[i].position + Vector3.back*destinationOffsets[i]);
				destinationOffsets[i] += queueOffset;

				customer.SetActive(true);

				return;
			}
		}
	}

	public void ClearOffsets()
	{
		System.Array.Clear(spawnPointsOffsets,0,spawnPointsOffsets.Length);
		System.Array.Clear(destinationOffsets, 0, destinationOffsets.Length);
	}
}
