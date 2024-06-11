using UnityEngine;
using UnityEngine.UI;

public class CameraMenuScript : MonoBehaviour
{
    public Image hiddenImage; // Assign this in the inspector

    // Start is called before the first frame update
    void Start()
    {
        // Initially hide the image
        hiddenImage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Reveal the image when Shift + B is pressed
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && Input.GetKeyDown(KeyCode.B))
        {
            hiddenImage.enabled = true;
        }
    }
}