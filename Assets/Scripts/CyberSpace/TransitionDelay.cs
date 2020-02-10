using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionDelay : MonoBehaviour
{
	[SerializeField] private float delay;

	private void Start()
	{
		LevelsManager.Instance.ToNextLevel(delay);
	}
}
