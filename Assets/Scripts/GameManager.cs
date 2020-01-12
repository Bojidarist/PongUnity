using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Text scoreText;
    public Button winToMainMenuButton;
    public Slider pauseMenuVolumeSlider;
    public int winScore = 10;
    public bool isPaused;

    private PlayerController[] players;
    private BallController ball;
    private int[] ballMultipliers;
    private int leftScore = 0;
    private int rightScore = 0;
    private bool isStarted = false;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        players = FindObjectsOfType<PlayerController>();
        ball = FindObjectOfType<BallController>();
        ballMultipliers = new[] { -1, 1 };
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStarted && Input.anyKeyDown && IsInGameScene())
        {
            isStarted = true;
            ball.gameObject.transform.position = Vector3.zero;
            ball.movement = new Vector3(0.1f * ballMultipliers[Random.Range(0, 2)], 0f, 0f);
        }
        else if (IsInGameScene())
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!isPaused)
                {
                    isPaused = true;
                    winToMainMenuButton.gameObject.SetActive(true);
                    pauseMenuVolumeSlider.gameObject.SetActive(true);
                }
                else
                {
                    isPaused = false;
                    winToMainMenuButton.gameObject.SetActive(false);
                    pauseMenuVolumeSlider.gameObject.SetActive(false);
                }
            }
        }
    }

    public void ScorePoint()
    {
        if (ball.transform.position.x > 0)
        {
            leftScore++;
        }
        else
        {
            rightScore++;
        }

        if (leftScore == winScore)
        {
            scoreText.text = "Left Wins";
            winToMainMenuButton.gameObject.SetActive(true);
            ball.gameObject.SetActive(false);
            foreach (PlayerController p in players)
            {
                p.gameObject.SetActive(false);
            }
            return;
        }
        else if (rightScore == winScore)
        {
            scoreText.text = "Right Wins";
            winToMainMenuButton.gameObject.SetActive(true);
            ball.gameObject.SetActive(false);
            foreach (PlayerController p in players)
            {
                p.gameObject.SetActive(false);
            }
            return;
        }

        foreach (PlayerController p in players)
        {
            p.gameObject.transform.position = new Vector3(p.transform.position.x, 0f);
        }

        ball.gameObject.transform.position = Vector3.zero;
        ball.movement = new Vector3(0.1f * ballMultipliers[Random.Range(0, 2)], 0f, 0f);

        scoreText.text = $"{ leftScore } - { rightScore }";
    }

    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public bool IsInGameScene()
    {
        return SceneManager.GetActiveScene().name == Screens.OfflinePlay1P ||
                SceneManager.GetActiveScene().name == Screens.OfflinePlay2P;
    }

    public void ExitGame()
    {
        Application.Quit(0);
    }

    public void ChangeVolumeFromMenuSlider()
    {
        PongOptions.Volume = pauseMenuVolumeSlider.value;
        ball.hitSound.volume = PongOptions.Volume;
    }
}
