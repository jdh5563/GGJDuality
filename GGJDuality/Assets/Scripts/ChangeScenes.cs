using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
	public void LoadMainMenu()
	{
		SceneManager.LoadScene("Main Menu");
	}

	public void LoadGame()
	{
		SceneManager.LoadScene("John Scene");
	}

	public void LoadWinMenu()
	{
		SceneManager.LoadScene("Win Menu");
	}

	public void LoadLoseMenu()
	{
		SceneManager.LoadScene("Lose Menu");
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
