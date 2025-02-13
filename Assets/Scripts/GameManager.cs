using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI gameOverScoreText;
    [SerializeField] private GameObject roundTextPrefab;

    private int score = 0;
    private int basketCount = 0;
    private int currentRound = 1;
    private const int basketsNeededForRound = 10;
    private const int maxRounds = 4;

    private void Awake()
    {
        // Implement singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }

        UpdateScoreUI();
    }

    public void AddScore(int points)
    {
        score += points;
        basketCount++;
        UpdateScoreUI();
        CheckLevelProgress();
    }

    public void RemoveScore(int points)
    {
        if (score > 0)
        {
            score -= points;
        }
        UpdateScoreUI();
    }

    private void CheckLevelProgress()
    {
        // If the player makes more than (or equal to) max rounds worth of baskets, show Game Over
        if (basketCount >= maxRounds * basketsNeededForRound)
        {
            GameOver();
        }
        // If the player just completed a round and there's another round to go, advance the round
        else if (basketCount % basketsNeededForRound == 0)
        {
            currentRound++;

            if (roundTextPrefab != null)
            {
                // Instantiate the RoundText prefab. Assumes the prefab has a TextMeshProUGUI component in its children.
                GameObject roundTextInstance = Instantiate(roundTextPrefab);
                TextMeshProUGUI tmp = roundTextInstance.GetComponentInChildren<TextMeshProUGUI>();
                if (tmp != null)
                {
                    tmp.text = "Round " + currentRound;
                }
                else
                {
                    Debug.LogWarning("RoundText prefab does not have a TextMeshProUGUI component!");
                }
            }
            else
            {
                Debug.LogWarning("RoundText prefab is not assigned in the inspector!");
            }
        }
    }

    public void GameOver()
    {
        if (gameOverPanel != null)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("GameOver Panel is not assigned in the inspector!");
        }
        
        // Update the score shown in the restart panel
        if (gameOverScoreText != null)
        {
            gameOverScoreText.text = "Score: " + score;
        }
        else
        {
            Debug.LogWarning("GameOver Score Text is not assigned in the inspector!");
        }

        UpdateScoreUI();
    }

    public void RestartGame()
    {
        RestartGameState();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RestartGameState()
    {
        score = 0;
        basketCount = 0;
        currentRound = 1;
        UpdateScoreUI();
        Time.timeScale = 1;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        else
        {
            Debug.LogWarning("GameOver Panel is not assigned in the inspector!");
        }

        PlayerLives playerLives = FindFirstObjectByType<PlayerLives>();
        if (playerLives != null)
        {
            playerLives.ResetLives();
        }
        else
        {
            Debug.LogWarning("Player Lives script is not found in the scene!");
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
        else
        {
            Debug.LogWarning("Score Text is not assigned in the inspector!");
        }
    }
}
