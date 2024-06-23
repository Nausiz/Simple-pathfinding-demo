using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] MapGenerator mapGenerator;
    [SerializeField] Pathfinding pathfinding;
    [SerializeField] GameObject playerCharacter;

    [SerializeField] float moveSpeed = 5f;

    private bool isMoving;

    public bool IsMoving { get => isMoving; }

    void Start()
    {
        playerCharacter.gameObject.SetActive(false);
        isMoving = false;
    }

    private void Update()
    {
        if (mapGenerator.StartTile != null && !isMoving)
        {
            //Puts the player in the starting position
            playerCharacter.SetActive(true);
            playerCharacter.transform.position = mapGenerator.StartTile.TargetPosition;
        }
        else if (mapGenerator.StartTile == null)
        {
            //Disables the player if there is no starting position set
            playerCharacter.SetActive(false);
        }

        if (mapGenerator.FinishTile != null && FinishedPath())
        {
            //Replaces finish with start after completing a path
            isMoving = false;

            mapGenerator.StartTile.IsStart = false;
            mapGenerator.FinishTile.IsStart = true;
        }
    }

    //Runs the script responsible for the player's movement
    public void StartMoving(List<Vector2Int> path)
    {
        if (path != null) 
        { 
            isMoving = true;
            List<Vector3> targets = ConvertToTarget(path);
            targets.Add(mapGenerator.FinishTile.TargetPosition);

            if (targets != null && targets.Count > 0)
            {
                StartCoroutine(MoveAlongTargets(targets));
            }
        }
    }

    //Moves the player's character along a designated path
    private IEnumerator MoveAlongTargets(List<Vector3> targets)
    {
        foreach (Vector3 target in targets)
        {
            yield return StartCoroutine(MoveToPosition(target));
        }
    }

    //Moves the player's character to the specified point
    private IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        while ((targetPosition - playerCharacter.transform.position).sqrMagnitude > 0.01f)
        {
            playerCharacter.transform.position = Vector3.MoveTowards(playerCharacter.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        playerCharacter.transform.position = targetPosition;
    }

    //Converts locations in the array to vectors
    private List<Vector3> ConvertToTarget(List<Vector2Int> path)
    {
        List<Vector3> targets = new List<Vector3>();
        for (int i = 0; i < path.Count - 1; i++)
        {
            targets.Add(mapGenerator.Map[path[i].x, path[i].y].TargetPosition);
        }
        return targets;
    }

    //Runs the script responsible for finding the path and the player's movement
    public void MovePlayer()
    {
        List<Vector2Int> path = pathfinding.FindPath();
        pathfinding.ColorPath(path);
        StartMoving(path);
    }

    //Checks if the path is complete
    private bool FinishedPath()
    {
        return playerCharacter.transform.position == mapGenerator.FinishTile.TargetPosition && mapGenerator.StartTile != null && mapGenerator.FinishTile !=null;
    }
}
