using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class VideoTrigger : MonoBehaviour
{
    public VideoPlayer videoPlayer;   // Reference to the VideoPlayer component
    public GameObject player;         // Reference to the player object
    public float playerSpeed = 5f;    // Player's normal speed
    private bool isPlayingVideo = false;  // Flag to check if the video is playing

    private CharacterController controller; // Reference to the player's movement controller

    void Start()
    {
        // Ensure the player has a CharacterController component
        controller = player.GetComponent<CharacterController>();
        videoPlayer.gameObject.SetActive(false); // Initially hide the video player
    }

    void Update()
    {
        // If the video is not playing, allow the player to move
        if (!isPlayingVideo)
        {
            MovePlayer();
        }
    }

    // Trigger detection for hitting the capsule
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("object")) // Check if the player collided with the capsule (tagged as "Capsule")
        {
            StartCoroutine(PlayVideo());
        }
    }

    // Coroutine to handle video playback and freeze player movement
    IEnumerator PlayVideo()
    {
        isPlayingVideo = true;  // Stop player movement
        Time.timeScale = 0f;    // Freeze game time

        videoPlayer.gameObject.SetActive(true);  // Show the video player
        videoPlayer.Play();      // Play the video

        // Wait for the video to finish
        while (videoPlayer.isPlaying)
        {
            yield return null;
        }

        // After video finishes
        videoPlayer.gameObject.SetActive(false);  // Hide the video player
        Time.timeScale = 1f;    // Resume game time
        isPlayingVideo = false;  // Allow player to move again
    }

    // Basic player movement logic
    void MovePlayer()
    {
        Vector3 move = transform.forward * playerSpeed * Time.deltaTime;
        controller.Move(move);
    }
}
