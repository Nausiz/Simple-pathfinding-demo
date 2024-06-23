using UnityEngine;

public class CameraModeUI : MonoBehaviour
{
    [SerializeField] private CameraController cameraController;
    [SerializeField] private MapGenerator mapGenerator;

    void Update()
    {
        if (Input.anyKeyDown)
        {
            //Check if the 'E' key is pressed, set the camera to Edit mode
            if (Input.GetKeyDown(KeyCode.E))
                cameraController.CurrentMode = CameraController.CameraMode.EditCamera;

            //Check if the 'F' key is pressed, set the camera to Free mode
            if (Input.GetKeyDown(KeyCode.F))
                cameraController.CurrentMode = CameraController.CameraMode.FreeCamera;

            //Check if the 'R' key is pressed, reset the map and camera position
            if (Input.GetKeyDown(KeyCode.R))
            {
                mapGenerator.ResetMap();
                cameraController.ResetCamera();
            }

            //Check if the 'X' key is pressed, quit the application
            if (Input.GetKeyDown(KeyCode.X))
                Application.Quit();
        }
    }
}
