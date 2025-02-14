using UnityEngine;

public class PlayerLives : MonoBehaviour
{
    private int lives = 4;
    [SerializeField] private GameObject playerPrefab; // Assign basket prefab

    private int currentLives;
    private GameObject[] baskets; // Stores references to stacked baskets

    private void Start()
    {
        ResetLives();
        CreatePlayers();
    }

    public void CreatePlayers()
    {
        baskets = new GameObject[lives + 1]; // +1 to include the player basket

        // Find the existing player basket (already in the scene)
        GameObject playerBasket = GameObject.FindGameObjectWithTag("Player");
        if (playerBasket == null)
        {
            Debug.LogError("Player basket not found! Make sure the player has the tag 'Player'.");
            return;
        }

        baskets[0] = playerBasket; // Assign the existing player basket as the bottom basket

        for (int i = 1; i <= lives - 1; i++) // Start from 1 to avoid duplicating the player basket
        {
            Vector3 position = new Vector3(playerBasket.transform.position.x, playerBasket.transform.position.y + (i * 0.5f), 0);
            GameObject newBasket = Instantiate(playerPrefab, position, Quaternion.identity);
            baskets[i] = newBasket;

            // Make upper baskets follow the one below them
            newBasket.AddComponent<FollowBasket>().SetTarget(baskets[i - 1]);
        }
    }

    public void ResetLives()
    {
        currentLives = lives;
    }

    public void RemoveLife()
    {
        if (currentLives > 0)
        {
            currentLives--;
            RemoveBasket();
        }

        if (currentLives <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }

    private void RemoveBasket()
    {
        if (baskets[currentLives] != null)
        {
            // Destroy the topmost basket
            Destroy(baskets[currentLives]); 
            baskets[currentLives] = null;
        }

        // If it's the last basket (the player basket), disable it instead of just leaving it stuck
        if (currentLives == 0 && baskets[0] != null)
        {
            Debug.Log("Game Over! Disabling Player Basket.");
            baskets[0].SetActive(false); // Disable player basket instead of freezing it
        }
    }
}
