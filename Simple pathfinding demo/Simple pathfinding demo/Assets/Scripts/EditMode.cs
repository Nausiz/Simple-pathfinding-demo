using UnityEngine;

public class EditMode : MonoBehaviour
{
    [SerializeField] private CameraController cameraController;
    [SerializeField] private MapGenerator mapGenerator;
    [SerializeField] private Player player;

    private Edit currentMode;

    public Edit CurrentMode
    {
        get => currentMode;
        set => currentMode = value;
    }

    private void Start()
    {
        CurrentMode = Edit.Obstacles;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && cameraController.CurrentMode == CameraController.CameraMode.EditCamera && !player.IsMoving)
        {
            if (CurrentMode == Edit.Obstacles)
                HandleObstacles();

            if (CurrentMode == Edit.Start)
                SetStart();

            if (CurrentMode == Edit.Finish)
                SetFinish();
        }
    }

    //Sets or removes an obstacle
    private void HandleObstacles()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Tile tile = hit.transform.GetComponent<Tile>();
            if (tile != null)
            {
                tile.IsBlocked = !tile.IsBlocked;
            } 
            else if (hit.transform.tag == "Obstacle")
            {
                tile = hit.transform.parent.GetComponent<Tile>();
                tile.IsBlocked = !tile.IsBlocked;
            }
        }
    }

    //Sets or removes start position
    private void SetStart()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Tile tile = hit.transform.GetComponent<Tile>();
            if (tile != null)
            {
                if (!tile.IsStart)
                    RemovePreviousStart();

                tile.IsStart = !tile.IsStart;
            }
            else if (hit.transform.tag == "Obstacle")
            {
                tile = hit.transform.parent.GetComponent<Tile>();
                
                if (!tile.IsStart)
                    RemovePreviousStart();

                tile.IsStart = !tile.IsStart;
            }
        }
    }

    //Sets or removes finish position
    private void SetFinish()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Tile tile = hit.transform.GetComponent<Tile>();
            if (tile != null)
            {
                if (!tile.IsFinish)
                    RemovePreviousFinish();

                tile.IsFinish = !tile.IsFinish;
            }
            else if (hit.transform.tag == "Obstacle")
            {
                tile = hit.transform.parent.GetComponent<Tile>();

                if (!tile.IsFinish)
                    RemovePreviousFinish();

                tile.IsFinish = !tile.IsFinish;
            }
        }
    }

    //Deletes the previously selected start
    private void RemovePreviousStart()
    {
        if (mapGenerator.Map != null)
        {
            for (int row = 0; row < mapGenerator.Map.GetLength(0); row++)
            {
                for (int col = 0; col < mapGenerator.Map.GetLength(1); col++)
                {
                    if (mapGenerator.Map[row, col].IsStart)
                    {
                        mapGenerator.Map[row, col].IsStart = false;
                    }
                }
            }
        }
    }

    //Deletes the previously selected finish
    private void RemovePreviousFinish()
    {
        if (mapGenerator.Map != null)
        {
            for (int row = 0; row < mapGenerator.Map.GetLength(0); row++)
            {
                for (int col = 0; col < mapGenerator.Map.GetLength(1); col++)
                {
                    if (mapGenerator.Map[row, col].IsFinish)
                    {
                        mapGenerator.Map[row, col].IsFinish = false;
                    }
                }
            }
        }
    }

    public enum Edit
    {
        Obstacles,
        Start,
        Finish,
        None
    }
}
