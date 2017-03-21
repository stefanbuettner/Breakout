using UnityEngine;

[RequireComponent(typeof(TextMesh))]
public class GameControl : MonoBehaviour
{
    public TextMesh turnDisplay;
    public TextMesh pointsDisplay;
    public PlayerControls player;

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
        player.Reset();
    }

    public void EndTurn()
    {
        if (turns > 0)
        {
            turns = turns - 1;
            player.SpawnNewBall();
        }
        else
            Debug.Log("Game ended");
    }

    void UpdateDisplays()
    {
        pointsDisplay.text = points.ToString("D3");
        turnDisplay.text = turns.ToString();
    }
}
