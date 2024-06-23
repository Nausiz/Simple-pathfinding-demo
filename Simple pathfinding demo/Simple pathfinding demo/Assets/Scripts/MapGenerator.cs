using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private Tile tile;
    [SerializeField] private int length;
    [SerializeField] private int width; 

    private float tileSize = 1f;
    private Tile[,] map;
    private Tile startTile;
    private Tile finishTile;

    public int Length
    {
        get { return length; }
        set
        {
            if (value > 0)
            {
                length = value;
                Generate();
            }
            else
            {
                Debug.LogWarning("Length must be greater than 0.");
            }
        }
    }

    public int Width
    {
        get { return width; }
        set
        {
            if (value > 0)
            {
                width = value;
                Generate();
            }
            else
            {
                Debug.LogWarning("Width must be greater than 0.");
            }
        }
    }

    public Tile[,] Map
    {
        get { return map; }
        set
        {
            if (value != null)
            {
                map = value;
                Generate();
            }
        }
    }

    public Tile StartTile
    {
        get => startTile;
        set
        {
            if (startTile != value)
            { 
                startTile = value;

                if(value)
                    startTile.IsStart = true;
            }
        }
    }

    public Tile FinishTile
    {
        get => finishTile;
        set
        {
            if (finishTile != value)
            {
                finishTile = value;

                if(value)
                    finishTile.IsFinish = true;
            }
        }
    }

    private void Awake()
    {
        Generate();
    }

    //Generates a map
    private void Generate()
    {
        RemoveObstacles();
        ClearMap();

        map = new Tile[length, width];

        for (int row = 0; row < length; row++)
        {
            for (int col = 0; col < width; col++)
            {
                Vector3 position = new Vector3(row * tileSize, 0.0f,  col * tileSize);

                Tile tile = Instantiate(this.tile, position, Quaternion.identity, transform);
                tile.Initialize(row, col);

                if (tile != null)
                {
                    tile.OriginalColor = (row + col) % 2 == 0 ? Color.black : Color.gray;
                }
                map[row, col] = tile;
            }
        }
    }

    //Resets the map and restores the starting values
    public void ResetMap()
    {
        RemoveObstacles();
        ClearMap();

        length = 8;
        width = 8;

        StartTile = null;
        FinishTile = null;

        Generate();
    }

    //Removes obstacles from the map
    private void RemoveObstacles()
    {
        if (map != null)
        {
            for (int row = 0; row < map.GetLength(0); row++)
            {
                for (int col = 0; col < map.GetLength(1); col++)
                {
                    map[row, col].IsBlocked = false;
                }
            }
        }
    }

    //Removes all tiles
    private void ClearMap()
    {
        if (map != null)
        {
            for (int row = 0; row < map.GetLength(0); row++)
            {
                for (int col = 0; col < map.GetLength(1); col++)
                {
                    if (map[row, col] != null)
                    {
                        Destroy(map[row, col]);
                    }
                }
            }
            map = null;
        }
        DestroyAllChildren();
    }

    public void DestroyAllChildren()
    {
        int childCount = transform.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            GameObject childObject = transform.GetChild(i).gameObject;
            DestroyImmediate(childObject);
        }
    }

    //Checks if start and finish are set
    public bool IsStartAndFinishSet()
    {
        return StartTile != null && FinishTile != null;
    }
}
