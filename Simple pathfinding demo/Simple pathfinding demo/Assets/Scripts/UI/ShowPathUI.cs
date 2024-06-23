using UnityEngine;
using UnityEngine.UI;

public class ShowPathUI : MonoBehaviour
{
    [SerializeField] private MapGenerator mapGenerator;
    [SerializeField] private Pathfinding pathfinding;
    [SerializeField] private Player player;

    //UI elements
    [SerializeField] private Button findPathButton;
    [SerializeField] private Text information;

    void Start()
    {
        findPathButton.onClick.AddListener(FindPath);
    }

    void Update()
    {
        // Check if the start and finish points are set and if the player is moving
        if (mapGenerator.IsStartAndFinishSet() && player.IsMoving)
        {
            findPathButton.interactable = false;
            information.enabled = false;
        }

        // Check if the start and finish points are set
        if (mapGenerator.IsStartAndFinishSet())
        {
            findPathButton.interactable = true;
            information.enabled = false;
        }
        else
        {
            findPathButton.interactable = false;
            information.enabled = true;
            pathfinding.ClearPreviousPath();
        }
    }

    //After clicking the findPathButton, show the path and move the player from the start to the finish
    private void FindPath()
    {
        player.MovePlayer();
    }
}
