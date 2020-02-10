using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField] private int startPoints;
	[SerializeField] private int totalPointsToWin;

	private int currentPoints;

	public static GameManager Instance { get; set; }

	public CustomersManager Manager { get; set; }

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			currentPoints = startPoints;
			Manager = GetComponent<CustomersManager>();
		}
	}

	private void Start()
	{
		SubwayUIManager.Instance.InitProgressBar(0, totalPointsToWin,startPoints);
	}

	//flyAway case
	public void ProcessPoints(bool isPaid)
	{
		if (currentPoints == totalPointsToWin || currentPoints == 0)
			return;

		if (isPaid)
			currentPoints++;
		else
			currentPoints--;

		currentPoints = Mathf.Clamp(currentPoints, 0, totalPointsToWin);

		SubwayUIManager.Instance.UpdateProgressBar(currentPoints);

		if (currentPoints == totalPointsToWin)
		{
			print("You win!");
			LevelsManager.Instance.ToNextLevel(1);
		}
		else if (currentPoints == 0)
		{
			LevelsManager.Instance.RestartLevel(1);
		}
	}

	public void CustomersIsOver()
	{
		if (currentPoints != totalPointsToWin)
		{
			LevelsManager.Instance.RestartLevel(1);
			Destroy(this);
		}
	}
}
