using UnityEngine;

public class ThirdPersonPlayerController : MonoBehaviour
{
    [SerializeField] private float verticalInput;
    [SerializeField] private float horizontalInput;

    [SerializeField] private float rotateSpd;

    [SerializeField] private Camera cam;

    private Vector3 lookDir;

    private void Update() {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        MovePlayer();
    }

    private void MovePlayer() {
        lookDir = cam.transform.forward * verticalInput + cam.transform.right * horizontalInput;

        transform.forward = Vector3.Slerp(transform.forward, lookDir, Time.deltaTime * rotateSpd);
    }
}
