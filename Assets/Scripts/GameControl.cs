using UnityEngine;

[RequireComponent(typeof(TextMesh))]
public class GameControl : MonoBehaviour
{
    public TextMesh turnDisplay;
    public TextMesh pointsDisplay;
    public PlayerControls player;
    public BrickManager brickMgr;
    public GameOverMenu gameOverMenu;

    public int turns = 3;
    public int points = 0;
    private int numBrickHits = 0;

    public float speedGain = 4f;

    // Use this for initialization
    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplays();
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Shutdown();
        }
    }

    public void Shutdown()
    {
        Application.Quit();
    }

    public void Reset()
    {
        turns = 3;
        points = 0;
        numBrickHits = 0;
        UpdateDisplays();
        gameOverMenu.gameObject.SetActive(false);
        player.enabled = true;
        player.Reset();
        brickMgr.Reset();
    }

    public void EndTurn()
    {
        if (turns > 0)
        {
            if (turns > 1)
                player.SpawnNewBall();
            else
            {
                player.enabled = false;
                gameOverMenu.DisplayWin(false);
                gameOverMenu.gameObject.SetActive(true);
            }
            turns = turns - 1;
        }
    }

    public void GameWon()
    {
        player.enabled = false;
        gameOverMenu.DisplayWin(true);
        gameOverMenu.gameObject.SetActive(true);
        foreach (Ball ball in GameObject.FindObjectsOfType<Ball>())
        {
            Destroy(ball.gameObject);
        }
    }

    public void BrickHit(int brickHitPoints, Ball ball)
    {
        points += brickHitPoints;
        ++numBrickHits;

        switch (numBrickHits)
        {
            case 4:
            case 12:
                Rigidbody ballRB = ball.GetComponent<Rigidbody>();
                ballRB.velocity += ballRB.velocity.normalized * speedGain;
                player.initialBallSpeed += player.initialBallSpeed.normalized * speedGain;
                Debug.Log("Speed increase after " + numBrickHits + " hits");
                break;
        }
    }

    void UpdateDisplays()
    {
        pointsDisplay.text = points.ToString("D3");
        turnDisplay.text = turns.ToString();
    }
}
