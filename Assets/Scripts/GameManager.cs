using System.Collections;
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
    private const int basketsNeededForRound = 3;
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
        Debug.Log("Score: " + score + " | Basket Count: " + basketCount + " | Current Round: " + currentRound);
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
        if (basketCount >= maxRounds * basketsNeededForRound)
        {
            GameOver();
        }
        else if (basketCount % basketsNeededForRound == 0)
        {
            currentRound++;

            if (roundTextPrefab != null)
            {
                // Find the existing RoundText GameObject
                TextMeshProUGUI roundText = roundTextPrefab.GetComponent<TextMeshProUGUI>();

                if (roundText != null)
                {
                    roundText.text = "Round " + currentRound;
                    roundText.gameObject.SetActive(true); // Make sure it's always visible
                }
                else
                {
                    Debug.LogWarning("RoundText GameObject does not have a TextMeshProUGUI component!");
                }
            }
            else
            {
                Debug.LogWarning("RoundText GameObject is not assigned in the Inspector!");
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

    public void LoadMainMenu()
    {
        RestartGameState();
        SceneManager.LoadScene("Main Menu");
    }

    public void RestartGame()
    {
        RestartGameState();
        SceneManager.LoadScene("ApplePicker");
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
        
        StartCoroutine(FindPlayerLivesAfterSceneLoad());
    }

    private IEnumerator FindPlayerLivesAfterSceneLoad()
    {
        yield return new WaitForSeconds(0.1f);

        PlayerLives playerLives = FindFirstObjectByType<PlayerLives>();
        if (playerLives != null)
        {
            Debug.Log("PlayerLives found after scene reload. Resetting lives...");
            playerLives.ResetLives();
        }
        else
        {
            Debug.LogWarning("Player Lives script is not found in the scene after reloading!");
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
