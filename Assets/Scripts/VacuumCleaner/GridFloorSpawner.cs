using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridFloorSpawner : MonoBehaviour
{
	[SerializeField] Vector3 size;
	[SerializeField] GameObject floorPrefab;
	[SerializeField] GameObject wallPrefab;
	[SerializeField] float prefabExtents;
	[SerializeField] float wallsWidth;
	[SerializeField] float wallsHeight;

	private void Awake()
	{
		Vector3 spawnPoint = transform.position;

		SpawnWalls();

		spawnPoint = spawnPoint + Vector3.left * (size.x/2 - prefabExtents) + Vector3.forward * (size.z/2-prefabExtents);

		for(int i = 0;i<size.z;i++)
		{
			for(int j = 0;j<size.x;j++)
			{
				Instantiate(floorPrefab).transform.position = spawnPoint + Vector3.right*prefabExtents*2*j;
			}
			spawnPoint += Vector3.back * prefabExtents * 2;
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(transform.position, size);
	}

	private void SpawnWalls()
	{
		var cube1 = Instantiate(wallPrefab);
		var cube2 = Instantiate(wallPrefab);
		var cube3 = Instantiate(wallPrefab);
		var cube4 = Instantiate(wallPrefab);

		cube1.transform.position = transform.position + Vector3.left * (size.x / 2+wallsWidth/2) + Vector3.up*wallsHeight/2;
		cube1.transform.localScale = Vector3.Scale(cube1.transform.localScale, new Vector3(wallsWidth,wallsHeight,size.z));

		cube2.transform.position = transform.position + Vector3.right * (size.x / 2+wallsWidth/2) + Vector3.up*wallsHeight/2;
		cube2.transform.localScale = Vector3.Scale(cube2.transform.localScale, new Vector3(wallsWidth,wallsHeight,size.z));

		cube3.transform.position = transform.position + Vector3.back * (size.z / 2+wallsWidth/2) + Vector3.up*wallsHeight/2;
		cube3.transform.localScale = Vector3.Scale(cube3.transform.localScale, new Vector3(size.x + wallsWidth*2,wallsHeight,wallsWidth));

		cube4.transform.position = transform.position + Vector3.forward * (size.z / 2+wallsWidth/2) + Vector3.up*wallsHeight/2;
		cube4.transform.localScale = Vector3.Scale(cube4.transform.localScale, new Vector3(size.x + wallsWidth*2,wallsHeight,wallsWidth));
	}
}
