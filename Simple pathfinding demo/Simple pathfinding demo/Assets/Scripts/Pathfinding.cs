using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    [SerializeField] private MapGenerator mapGenerator;

    //UI elements
    [SerializeField] private NotificationUI notificationUI;

    Vector2Int start, finish;

    //Searches for a path to the finish using the a* algorithm
    public List<Vector2Int> FindPath()
    {
        List<Vector2Int> openSet = new List<Vector2Int>();
        HashSet<Vector2Int> closedSet = new HashSet<Vector2Int>();
        Dictionary<Vector2Int, Vector2Int> cameFrom = new Dictionary<Vector2Int, Vector2Int>();
        Dictionary<Vector2Int, int> gScore = new Dictionary<Vector2Int, int>();
        Dictionary<Vector2Int, int> fScore = new Dictionary<Vector2Int, int>();
        start = new Vector2Int(mapGenerator.StartTile.XCoord, mapGenerator.StartTile.YCoord);
        finish = new Vector2Int(mapGenerator.FinishTile.XCoord, mapGenerator.FinishTile.YCoord);

        openSet.Add(start);
        gScore[start] = 0;
        fScore[start] = GetDistance(start, finish);

        while (openSet.Count > 0)
        {
            Vector2Int current = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (fScore[openSet[i]] < fScore[current])
                {
                    current = openSet[i];
                }
            }

            if (current == finish)
            {
                return RetracePath(cameFrom, current);
            }

            openSet.Remove(current);
            closedSet.Add(current);

            foreach (Vector2Int neighbor in GetNeighbors(current))
            {
                if (closedSet.Contains(neighbor) || mapGenerator.Map[neighbor.x, neighbor.y].IsBlocked)
                    continue;

                int tentativeGScore = gScore[current] + 1;

                if (!openSet.Contains(neighbor))
                {
                    openSet.Add(neighbor);
                }
                else if (tentativeGScore >= gScore[neighbor])
                {
                    continue;
                }

                cameFrom[neighbor] = current;
                gScore[neighbor] = tentativeGScore;
                fScore[neighbor] = gScore[neighbor] + GetDistance(neighbor, finish);
            }
        }

        notificationUI.ActivateForTime(4f);
        return null;
    }

    //Returns the return path
    List<Vector2Int> RetracePath(Dictionary<Vector2Int, Vector2Int> cameFrom, Vector2Int current)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        while (cameFrom.ContainsKey(current))
        {
            path.Add(current);
            current = cameFrom[current];
        }
        path.Reverse();
        return path;
    }

    //Returns a list of neighbors of the point
    List<Vector2Int> GetNeighbors(Vector2Int cell)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>();

        if (cell.x > 0)
            neighbors.Add(new Vector2Int(cell.x - 1, cell.y)); //West 
        if (cell.x < mapGenerator.Map.GetLength(0) - 1)
            neighbors.Add(new Vector2Int(cell.x + 1, cell.y)); //East
        if (cell.y > 0)
            neighbors.Add(new Vector2Int(cell.x, cell.y - 1)); //South
        if (cell.y < mapGenerator.Map.GetLength(1) - 1)
            neighbors.Add(new Vector2Int(cell.x, cell.y + 1)); //North

        return neighbors;
    }

    //Distance between points 
    private int GetDistance(Vector2Int a, Vector2Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }

    //Colors the path
    public void ColorPath(List<Vector2Int> path)
    {
        if (path == null)
        {
            ClearPreviousPath();
        }

        if (mapGenerator.Map != null && path != null)
        {
            for (int row = 0; row < mapGenerator.Map.GetLength(0); row++)
            {
                for (int col = 0; col < mapGenerator.Map.GetLength(1); col++)
                {
                    if (mapGenerator.Map[row, col] != null)
                    {
                        if (!mapGenerator.Map[row, col].IsStart && !mapGenerator.Map[row, col].IsFinish)
                        {
                            mapGenerator.Map[row, col].GetComponent<Renderer>().material.color = mapGenerator.Map[row, col].OriginalColor;
                        }

                        foreach (Vector2Int node in path.GetRange(0, path.Count - 1))
                        {
                            if (row == node.x && col == node.y)
                            {
                                mapGenerator.Map[row, col].GetComponent<Renderer>().material.color = Color.blue;
                            }
                        }
                    }
                }
            }
        }
    }

    //Clears the color of the previous track
    public void ClearPreviousPath()
    {
        if (mapGenerator.Map != null)
        {
            for (int row = 0; row < mapGenerator.Map.GetLength(0); row++)
            {
                for (int col = 0; col < mapGenerator.Map.GetLength(1); col++)
                {
                    if (mapGenerator.Map[row, col] != null)
                    {
                        if (!mapGenerator.Map[row, col].IsStart && !mapGenerator.Map[row, col].IsFinish)
                        {
                            mapGenerator.Map[row, col].GetComponent<Renderer>().material.color = mapGenerator.Map[row, col].OriginalColor;
                        }
                    }
                }
            }
        }
    }
}
