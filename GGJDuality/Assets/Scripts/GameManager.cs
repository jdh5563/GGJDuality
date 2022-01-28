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
	[SerializeField] private Color startColor = Color.black;
	[SerializeField] private Color endColor = Color.white;

	private GameObject[] tilemaps;

	// Start is called before the first frame update
	void Start()
	{
		main = Camera.main;
		tilemaps = GameObject.FindGameObjectsWithTag("Level");
		tilemaps[0].SetActive(true);
		tilemaps[1].SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{
		// Toggle Light/Dark Mode
		if (Input.GetKeyDown(KeyCode.Space))
		{
			StartCoroutine(LERPBackgroundColor(startColor, endColor));
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
		foreach(GameObject tilemap in tilemaps) {
			tilemap.SetActive(!tilemap.activeSelf);
		}
	}
}