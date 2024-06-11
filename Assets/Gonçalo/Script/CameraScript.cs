using UnityEngine;
using UnityEngine.AI; // Import the AI namespace

public class CameraScript : MonoBehaviour
{
    public Transform player; // The player object
    public Vector3 offset; // The offset from the player's position
    public float bobFrequency = 1f; // The speed of the bob
    public float bobHeight = 0.1f; // The height of the bob

    private float timer;

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            // Calculate the desired position
            Vector3 desiredPosition = player.position + offset;

            // Set the camera's position to the desired position
            transform.position = desiredPosition;

            // Make the camera look at the player
            transform.LookAt(player.position);

            // Get the player's velocity
            float velocity = player.GetComponent<NavMeshAgent>().velocity.magnitude;

            if (velocity > 0.1f)
            {
                // The player is moving
                timer += Time.deltaTime * bobFrequency;
                transform.position = transform.position + new Vector3(0, Mathf.Sin(timer) * bobHeight, 0);
            }
            else
            {
                // The player is not moving
                timer = 0;
            }
        }
    }
}