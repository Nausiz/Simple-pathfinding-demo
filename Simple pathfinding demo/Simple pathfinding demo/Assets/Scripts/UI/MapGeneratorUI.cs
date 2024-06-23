using UnityEngine;
using UnityEngine.UI;

public class MapGeneratorUI : MonoBehaviour
{
    [SerializeField] private MapGenerator mapGenerator;

    //UI elements
    [SerializeField] private InputField lengthInput;
    [SerializeField] private InputField widthInput;

    void Start()
    {
        lengthInput.onValueChanged.AddListener(lengthChanged);
        widthInput.onValueChanged.AddListener(widthChanged);
    }

    private void Update()
    {
        //Updates map parameters in UI

        if (lengthInput.text != mapGenerator.Length.ToString())
            lengthInput.text = mapGenerator.Length.ToString();

        if (widthInput.text != mapGenerator.Width.ToString())
            widthInput.text = mapGenerator.Width.ToString();
    }

    //Checks the value provided by the user and updates the map length
    private void lengthChanged(string length)
    {
        int lengthTryParsed;
        if (int.TryParse(length, out lengthTryParsed))
            mapGenerator.Length = lengthTryParsed;
    }

    //Checks the value provided by the user and updates the map width
    private void widthChanged(string width)
    {
        int widthTryParsed;
        if (int.TryParse(width, out widthTryParsed))
            mapGenerator.Width = widthTryParsed;
    }
}
