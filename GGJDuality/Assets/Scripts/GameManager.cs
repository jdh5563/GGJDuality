using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
	// The world camera
	private Camera main;

	// Controls going back and forth between black and white worlds
	private float colorProgress = 0;
	[SerializeField] private Color startColor;
	[SerializeField] private Color endColor;

	// The black and white tilemaps
	private List<GameObject> tilemaps = new List<GameObject>();

	// The background music for the game
	[SerializeField] private AudioSource backgroundMusic;

	// The number of the current level the player is on
	public static int currentLevelNumber = 0;

	// The total number of levels in the game
	private int totalLevels;

	// The current level the player is on
	private GameObject activeLevel;

	// The player and its spawn point
	[SerializeField] private GameObject player;
	[SerializeField] private GameObject playerSpawn;

	// Start is called before the first frame update
	void Start()
	{
		main = Camera.main;
		tilemaps.Add(GameObject.Find("Tutorial").transform.GetChild(0).gameObject);
		tilemaps.Add(GameObject.Find("Tutorial").transform.GetChild(1).gameObject);
		tilemaps.Add(GameObject.Find("Level1").transform.GetChild(0).gameObject);
		tilemaps.Add(GameObject.Find("Level1").transform.GetChild(1).gameObject);
		tilemaps.Add(GameObject.Find("Level2").transform.GetChild(0).gameObject);
		tilemaps.Add(GameObject.Find("Level2").transform.GetChild(1).gameObject);
		activeLevel = tilemaps[0];
		tilemaps[0].SetActive(false);
		tilemaps[1].SetActive(true);

		for(int i = 2; i < tilemaps.Count; i++)
		{
			tilemaps[i].SetActive(false);
		}

		currentLevelNumber = 0;
		totalLevels = tilemaps.Count / 2;
	}

	// Update is called once per frame
	void Update()
	{
		// Toggle Light/Dark Mode
		if (Input.GetKeyDown(KeyCode.Space))
		{
			StartCoroutine(LERPBackgroundColor(startColor, endColor));
		}

		// Check if we have entered a new level
		if (2 * currentLevelNumber != tilemaps.IndexOf(activeLevel))
		{
			if(currentLevelNumber == totalLevels)
			{
				GetComponent<ChangeScenes>().LoadWinMenu();
				return;
			}

			for(int i = -2; i < 2; i++)
			{
				// Set the old level to be inactive and the new level to be active
				tilemaps[currentLevelNumber * 2 + i].SetActive(i == 1 ? true : false);
			}

			// Reset the player's position
			player.transform.position = playerSpawn.transform.position;
			player.GetComponent<Player>().hasWon = false;

			// If the background is not white, we need to update some variables
			if(main.backgroundColor != Color.white)
			{
				StopAllCoroutines();
				main.backgroundColor = Color.white;
				startColor = Color.white;
				endColor = Color.black;
				backgroundMusic.volume = 1;
			}

			// Update the active level
			activeLevel = tilemaps[currentLevelNumber * 2];
		}

		// Check if the player has fallen off the level beyond return
		if(player.transform.position.y < activeLevel.GetComponent<Tilemap>().cellBounds.yMin - 10)
		{
			GetComponent<ChangeScenes>().LoadLoseMenu();
		}
	}

	/// <summary>
	/// LERPs from the current color to the given color
	/// </summary>
	/// <param name="startColor">The color to LERP to</param>
	/// <param name="endColor">The color to LERP to</param>
	private IEnumerator LERPBackgroundColor(Color startColor, Color endColor)
	{
		// LERP from startColor to endColor
		while (colorProgress < 1)
		{
			colorProgress += 0.05f;
			main.backgroundColor = new Color(
				Mathf.Lerp(startColor.r, endColor.r, colorProgress),
				Mathf.Lerp(startColor.g, endColor.g, colorProgress),
				Mathf.Lerp(startColor.b, endColor.b, colorProgress));

			backgroundMusic.volume = Mathf.Lerp(startColor.r, endColor.r, colorProgress);
			yield return new WaitForFixedUpdate();
		}

		// After that is finished, reset color progress, swap start and end colors, and switch tilemaps
		colorProgress = 0;
		this.endColor = startColor;
		this.startColor = endColor;
		ToggleTilemaps();
	}

	/// <summary>
	/// Switches between the 2 tilemaps for the level
	/// </summary>
	private void ToggleTilemaps()
	{
		for (int i = 0; i < 2; i++) {
			tilemaps[2 * currentLevelNumber + i].SetActive(!tilemaps[2 * currentLevelNumber + i].activeSelf);
		}
	}
}