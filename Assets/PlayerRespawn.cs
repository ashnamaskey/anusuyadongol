using UnityEngine;
using TMPro;

public class PlayerRespawn : MonoBehaviour
{
    [Header("UI")]
    public GameObject hitPanel;
    public TextMeshProUGUI hitText;

    private Vector3 spawnPosition;
    private Quaternion spawnRotation;
    private bool isHit = false;

    // Reference to character controller
    private CharacterController characterController;

    void Start()
    {
        // Get the character controller
        characterController = GetComponent<CharacterController>();

        // Remember spawn position
        spawnPosition = transform.position;
        spawnRotation = transform.rotation;

        if (hitPanel != null)
            hitPanel.SetActive(false);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {

        if (hit.gameObject.CompareTag("Car") && !isHit)
            HandleHit();
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Car") && !isHit)
            HandleHit();
    }

    void OnCollisionEnter(Collision collision)
    {


        if (collision.gameObject.CompareTag("Car") && !isHit)
            HandleHit();
    }

    void HandleHit()
    {
        isHit = true;
        Debug.Log("PLAYER HIT — respawning in 2 seconds!");

        if (hitPanel != null)
            hitPanel.SetActive(true);

        if (hitText != null)
            hitText.text = "OUCH!\nYou got hit by a car!\n\nRespawning...";

        Invoke("Respawn", 2f);
    }

    void Respawn()
    {
        Debug.Log("Respawning now!");

        // Disable character controller first
        if (characterController != null)
            characterController.enabled = false;

        // Teleport to spawn
        transform.position = spawnPosition;
        transform.rotation = spawnRotation;

        // Re enable character controller
        if (characterController != null)
            characterController.enabled = true;

        // Hide hit panel
        if (hitPanel != null)
            hitPanel.SetActive(false);

        isHit = false;
        Debug.Log("Player respawned at: " + spawnPosition);
    }
}