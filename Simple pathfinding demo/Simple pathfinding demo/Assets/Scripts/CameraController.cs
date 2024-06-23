using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float sensitivity = 3f;
    [SerializeField] private Transform cameraSlot;

    private CameraMode currentMode;

    public CameraMode CurrentMode
    {
        get => currentMode;
        set => currentMode = value;
    }

    private void Start()
    {
        CurrentMode = CameraMode.EditCamera;
    }

    private void Update()
    {
        switch (currentMode)
        {
            case CameraMode.FreeCamera:
                FreeCameraMode();
                break;
            case CameraMode.EditCamera:
                EditMode();
                break;
        }
    }

    //Script responsible for free camera mode
    private void FreeCameraMode()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(moveHorizontal, 0f, moveVertical) * moveSpeed * Time.deltaTime;
        transform.Translate(moveDirection, Space.Self);

        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;
        transform.Rotate(Vector3.up, mouseX);
        transform.Rotate(Vector3.left, mouseY);

        transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, 0f);
    }

    //Script responsible for editing mode
    private void EditMode()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    //Script responsible for resetting the camera
    public void ResetCamera()
    {
        CurrentMode = CameraMode.EditCamera;

        transform.position = cameraSlot.position;
        transform.rotation = cameraSlot.rotation;
    }

    public enum CameraMode
    {
        FreeCamera,
        EditCamera
    }
}
