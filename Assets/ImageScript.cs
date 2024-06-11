using UnityEngine;

public class ImageScript : MonoBehaviour
{
    public GameObject targetObject; // Assign this in the inspector
    private bool keysWerePressed; // Tracks whether the keys were pressed in the previous frame

    // Start is called before the first frame update
    void Start()
    {
        // Initially hide the target object
        targetObject.SetActive(false);
        keysWerePressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if Shift + B + E are being held down
        bool keysArePressed = (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && Input.GetKey(KeyCode.B) && Input.GetKey(KeyCode.E);

        // Toggle the target object's visibility when Shift + B + E are pressed, but weren't in the previous frame
        if (keysArePressed && !keysWerePressed)
        {
            targetObject.SetActive(!targetObject.activeSelf);
        }

        // Update keysWerePressed for the next frame
        keysWerePressed = keysArePressed;
    }
}