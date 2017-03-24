using UnityEngine;

[RequireComponent(typeof(TextMesh))]
public class GameControl : MonoBehaviour
{
    public TextMesh turnDisplay;
    public TextMesh pointsDisplay;
    public PlayerControls player;
    public BrickManager brickManager;
    public GameOverMenu gameOverMenu;
    public PlayerBorder topBorder;
    public PlayArea playArea;

    public int turns = 3;
    public int points = 0;
    private int numBrickHits = 0;

    public float speedGain = 4f;

    // Use this for initialization
    void Start()
    {
        Reset();
    }

    void OnEnable()
    {
        gameOverMenu.OnPlayAgain += Reset;
        gameOverMenu.OnQuit += Shutdown;
        topBorder.OnBallHit += BallHit;
        playArea.OnBallExit += BallLost;
        brickManager.OnBrickHit += BallHit;
    }

    void OnDisable()
    {
        gameOverMenu.OnPlayAgain -= Reset;
        gameOverMenu.OnQuit -= Shutdown;
        topBorder.OnBallHit -= BallHit;
        playArea.OnBallExit -= BallLost;
        brickManager.OnBrickHit -= BallHit;
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
        brickManager.Reset();
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

    public void BallHit(Ball ball, Hittable hit)
    {
        Brick brick = hit as Brick;
        if (brick != null)
        {
            points += brick.GetHitpoints();
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

            if (brickManager.GetActiveBricks() <= 0)
            {
                // Add some delay until the game is won.
                Invoke("GameWon", 0.1f);
            }
        }

        PlayerBorder border = hit as PlayerBorder;
        if (border != null)
        {
            Rigidbody ballRB = ball.GetComponent<Rigidbody>();
            ballRB.velocity += ballRB.velocity.normalized * border.speedGain;
            player.initialBallSpeed += player.initialBallSpeed.normalized * border.speedGain;
            Debug.Log("Speed gained by hitting " + name + ": " + speedGain);
        }
    }

    void UpdateDisplays()
    {
        pointsDisplay.text = points.ToString("D3");
        turnDisplay.text = turns.ToString();
    }

    public void BallLost(Ball ball, Hittable playArea)
    {
        EndTurn();
        Destroy(ball);
    }
}
