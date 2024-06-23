using UnityEngine;

public class Tile : MonoBehaviour
{
    //Tile height point
    [SerializeField] private Transform point;
    //Obstacle object
    [SerializeField] private GameObject obstacle;

    private MapGenerator mapGenerator;
    private bool isBlocked = false;
    private bool isStart = false;
    private bool isFinish = false;
    private Color originalColor;
    private int xCoord;
    private int yCoord;

    public Color OriginalColor
    {
        get { return originalColor; }
        set 
        {
            if (originalColor != value)
            {
                GetComponent<Renderer>().material.color = value;
                originalColor = value;
            }
        } 
    }

    public bool IsBlocked
    {
        get { return isBlocked; }
        set
        {
            if (isBlocked != value)
            {
                if (value)
                {
                    IsStart = false;
                    IsFinish = false;
                }
                isBlocked = value;
                UpdateObstacleState();
            }
        }
    }

    public bool IsStart
    {
        get { return isStart; }
        set
        {
            if (isStart != value)
            {
                if (value)
                {
                    IsFinish = false;
                    IsBlocked = false;
                    GetComponent<Renderer>().material.color = Color.yellow;
                    mapGenerator.StartTile = gameObject.GetComponent<Tile>();
                }
                else
                { 
                    GetComponent<Renderer>().material.color = OriginalColor;
                    mapGenerator.StartTile = null;
                }
                
                isStart = value;
            }
        }
    }

    public bool IsFinish
    {
        get { return isFinish; }
        set
        {
            if (isFinish != value)
            {
                if (value)
                {
                    IsStart = false;
                    IsBlocked = false;
                    GetComponent<Renderer>().material.color = Color.green;
                    mapGenerator.FinishTile = gameObject.GetComponent<Tile>();
                }
                else
                { 
                    GetComponent<Renderer>().material.color = OriginalColor;
                    mapGenerator.FinishTile = null;
                }
                
                isFinish = value;
            }
        }
    }

    public Vector3 TargetPosition
    {
        get { return point != null ? point.position : Vector3.zero; }
    }

    public int XCoord { get => xCoord; }
    public int YCoord { get => yCoord; }

    public void Initialize(int xCoord, int yCoord)
    {
        this.xCoord = xCoord;
        this.yCoord = yCoord;
    }

    public void Start()
    {
        mapGenerator = gameObject.GetComponentInParent<MapGenerator>();
    }

    //Checks the tile's state and sets or removes an obstacle
    public void UpdateObstacleState()
    {
        obstacle.SetActive(isBlocked);
    }
}
