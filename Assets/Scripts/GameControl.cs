using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TextMesh))]
public class GameControl : MonoBehaviour
{
    private StatsDisplay statsDisplay;
    private Player player;
    private Paddle paddle;
    private BrickManager brickManager;
    private GameOverMenu gameOverMenu;
    private PlayerBorder topBorder;
    private PlayArea playArea;

    public int turns = 3;
    public int points = 0;
    public float paddleFraction = 0.5f;
    private int numBrickHits = 0;

    [System.Serializable]
    public class SpeedGainEntry
    {
        public int hits;
        public float gain;
    }

    public List<SpeedGainEntry> speedGains = new List<SpeedGainEntry>();

    void Awake()
    {
        statsDisplay = FindObjectOfType<StatsDisplay>();
        player = FindObjectOfType<Player>();
        paddle = FindObjectOfType<Paddle>();
        brickManager = FindObjectOfType<BrickManager>();
        gameOverMenu = FindObjectOfType<GameOverMenu>();
        topBorder = FindObjectOfType<PlayerBorder>();
        playArea = FindObjectOfType<PlayArea>();
    }

    // Use this for initialization
    void Start()
    {
        LevelReset();
    }

    void OnEnable()
    {
        gameOverMenu.OnPlayAgain += LevelReset;
        gameOverMenu.OnQuit += Shutdown;
        topBorder.OnBallHit += BallHit;
        playArea.OnBallExit += BallLost;
        brickManager.OnBrickHit += BallHit;
    }

    void OnDisable()
    {
        gameOverMenu.OnPlayAgain -= LevelReset;
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

    void UpdateDisplays()
    {
        statsDisplay.SetPoints(points);
        statsDisplay.SetTurns(turns);
    }

    public void Shutdown()
    {
        Application.Quit();
    }

    public void LevelReset()
    {
        turns = 3;
        points = 0;
        numBrickHits = 0;
        UpdateDisplays();
        gameOverMenu.gameObject.SetActive(false);
        player.enabled = true;
        player.LevelReset();
        brickManager.LevelReset();
    }

    public void EndTurn()
    {
        if (turns > 0)
        {
            if (turns > 1)
                player.TurnReset();
            else
            {
                EndLevel(false);
            }
            turns = turns - 1;
        }
    }

    public void EndLevel(bool won)
    {
        player.enabled = false;
        gameOverMenu.DisplayWin(won);
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

            foreach (SpeedGainEntry speedGain in speedGains)
            {
                if (speedGain.hits == numBrickHits)
                {
                    Rigidbody ballRB = ball.GetComponent<Rigidbody>();
                    ballRB.velocity += ballRB.velocity.normalized * speedGain.gain;
                    paddle.shotSpeed += speedGain.gain;
                    Debug.Log("Speed increase after " + numBrickHits + " hits");
                }
            }

            if (brickManager.GetActiveBricks() <= 0)
            {
                EndLevel(true);
            }
        }

        PlayerBorder border = hit as PlayerBorder;
        if (border != null)
        {
            Rigidbody ballRB = ball.GetComponent<Rigidbody>();
            paddle.ScalePaddleWidth(paddleFraction);
        }
    }

    public void BallLost(Ball ball, Hittable playArea)
    {
        EndTurn();
        Destroy(ball.gameObject);
    }
}
