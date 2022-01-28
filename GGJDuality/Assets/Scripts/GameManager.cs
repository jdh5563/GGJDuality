using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Camera main;
    private float colorProgress = 0;
    private Color startColor = Color.black;
    private Color endColor = Color.white;

    // Start is called before the first frame update
    void Start()
    {
        main = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Toggle Light/Dark Mode
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
        while (colorProgress < 1)
        {
            colorProgress += 0.05f;
            main.backgroundColor = new Color(
                Mathf.Lerp(startColor.r, endColor.r, colorProgress),
                Mathf.Lerp(startColor.g, endColor.g, colorProgress),
                Mathf.Lerp(startColor.b, endColor.b, colorProgress));
            yield return new WaitForFixedUpdate();
        }

        colorProgress = 0;
        this.endColor = startColor;
        this.startColor = endColor;
	}
}
