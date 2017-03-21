using UnityEngine;

[RequireComponent(typeof(TextMesh))]
public class GameControl : MonoBehaviour
{
    public TextMesh turnDisplay;
    public TextMesh pointsDisplay;

    public int turns = 3;
    public int points = 0;

    // Use this for initialization
    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplays();
    }

    void Reset()
    {
        turns = 3;
        points = 0;
        UpdateDisplays();
    }

    void UpdateDisplays()
    {
        pointsDisplay.text = points.ToString();
        turnDisplay.text = turns.ToString();
    }
}
