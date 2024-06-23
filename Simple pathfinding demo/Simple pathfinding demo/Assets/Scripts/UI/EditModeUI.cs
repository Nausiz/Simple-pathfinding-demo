using UnityEngine;
using UnityEngine.UI;

public class EditModeUI : MonoBehaviour
{
    [SerializeField] private EditMode edit;

    //UI elements
    [SerializeField] private Toggle addObstacles;
    [SerializeField] private Toggle setStart;
    [SerializeField] private Toggle setFinish;

    void Start()
    {
        addObstacles.onValueChanged.AddListener(SetObstaclesMode);
        setStart.onValueChanged.AddListener(SetStartMode);
        setFinish.onValueChanged.AddListener(SetFinishMode);
    }

    //Allows the player to edit obstacles
    private void SetObstaclesMode(bool isOn)
    {
        if (addObstacles.isOn)
        {
            edit.CurrentMode = EditMode.Edit.Obstacles;
            setStart.isOn = false;
            setFinish.isOn = false;
        }
        CheckNoneMode();
    }

    //Allows the player to edit the start location
    private void SetStartMode(bool isOn)
    {
        if (setStart.isOn)
        {
            edit.CurrentMode = EditMode.Edit.Start;
            setFinish.isOn = false;
            addObstacles.isOn = false;
        }
        CheckNoneMode();
    }

    //Allows the player to edit the finish location
    private void SetFinishMode(bool isOn)
    {
        if (setFinish.isOn)
        {
            edit.CurrentMode = EditMode.Edit.Finish;
            setStart.isOn = false;
            addObstacles.isOn = false;
        }
        CheckNoneMode();
    }

    //If all options are disabled, enable none mode
    private void CheckNoneMode()
    {
        if (!addObstacles.isOn && !setStart.isOn && !setFinish.isOn)
        {
            edit.CurrentMode = EditMode.Edit.None;
        }
    }
}
