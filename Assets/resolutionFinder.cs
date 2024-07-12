using UnityEngine;
using UnityEngine.UI;

public class DisplayResolution : MonoBehaviour
{
    public Text resolutionText;  // Reference to a UI Text component

    void Start()
    {
        // Get the width and height of the screen
        int width = Screen.width;
        int height = Screen.height;

        // Format the resolution as a string
        string resolution = width + " x " + height;

        // Display the resolution in the console
        Debug.Log("Resolution: " + resolution);

        // Display the resolution on the UI text component if available
        if (resolutionText != null)
        {
            resolutionText.text = "Resolution: " + resolution;
        }
    }
}
