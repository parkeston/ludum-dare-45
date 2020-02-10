using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelsManager : MonoBehaviour
{
	public static LevelsManager Instance;

	[SerializeField] private GameObject transitionScreen;
	[SerializeField] private TMP_Text transitionTitle;

	private Animation transitionScreenAnim;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);

			transitionScreenAnim = transitionScreen.GetComponent<Animation>();

			Application.targetFrameRate = 60;
		}
		else
			Destroy(gameObject);
	}

	private void Update()
	{
		if (Input.touchCount >= 3)
		{
			if(Input.GetTouch(0).phase == TouchPhase.Ended)
				ToNextLevel();
		}
	}

	public void ToNextLevel(float delay = 0)
	{
		transitionTitle.text = winTitles[SceneManager.GetActiveScene().buildIndex];
		StartCoroutine(LevelTransition(delay));
	}

	public void RestartLevel(float delay)
	{
		transitionTitle.text = "";
		StartCoroutine(LevelTransition(delay,true));
	}

	IEnumerator LevelTransition( float delay = 0, bool isRestart = false)
	{
		if(delay>0.1f)
			yield return new WaitForSeconds(delay);

		transitionScreenAnim.Play();

		yield return new WaitForSeconds(0.57f);

		if (isRestart)
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		else
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	private static string[] winTitles =
	{
		"Far far\naway galaxy",
		"Boiled\nRevenge!?",
		"Tea time!",
		"Vacuum puzzle",
		"Make it dirty!",
		"Is is much\ncleaner?",
		"Cleaniest guy!",
		"Tennis is fun!",
		"Bullseye",
		"Control ball",
		"Timeout",
		"Custom service",
		"Tickets away",
		"Underground",
		"Back to\nthe roots"
	};
}
