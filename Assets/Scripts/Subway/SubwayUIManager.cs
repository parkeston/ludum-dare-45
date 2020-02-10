using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubwayUIManager : MonoBehaviour
{
	[SerializeField] Slider progressBar;

	public static SubwayUIManager Instance { get; set; }
	private bool isUpdating;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
			Destroy(gameObject);
	}

	public void InitProgressBar(int minValue, int maxValue, int current)
	{
		progressBar.minValue = minValue;
		progressBar.maxValue = maxValue;
		progressBar.value = current;
	}

	public void UpdateProgressBar(int value)
	{
		progressBar.value = value;
	}
}
