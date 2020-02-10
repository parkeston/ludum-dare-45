using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTile : MonoBehaviour
{
	[SerializeField] Color color;
	[SerializeField] private LayerMask obstaclesMask;

	private bool isColored;
	private static MaterialPropertyBlock propertyBlock;

	private static int totalfloorTiles;
	private static int TotalFloortiles
	{
		get => totalfloorTiles;
		set
		{
			totalfloorTiles = value;
			if (value == 0)
			{
				LevelsManager.Instance.ToNextLevel(1);
				print("Win!");
			}
		}
	}

	private void Awake()
	{
		if(propertyBlock == null)
		{
			propertyBlock = new MaterialPropertyBlock();
		}

		TotalFloortiles++;
	}

	private void Start()
	{
		if (Physics.Raycast(transform.position, Vector3.up,1,obstaclesMask))
		{
			TotalFloortiles--;
			Destroy(this);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!isColored)
			ChangeColor();
	}

	private void ChangeColor()
	{
		isColored = true;
		TotalFloortiles--;

		transform.GetChild(Random.Range(0,transform.childCount)).gameObject.SetActive(true);

		propertyBlock.SetColor("_Color", color);
		GetComponent<MeshRenderer>().SetPropertyBlock(propertyBlock);

		//Destroy(this);
	}
}
