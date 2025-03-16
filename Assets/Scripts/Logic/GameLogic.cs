using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 
public class GameLogic : MonoBehaviour
{
    public float gameDuration = 80f; // Dauer des Spiels in Sekunden
    public float timer;
    private bool isGameOver = false;
    public GameObject player;
    public TextMeshProUGUI timerText;

    [Header("UI Elements")]
    public GameObject gameOverPanel;
    void Start()
    {
        timer = gameDuration;
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
            return;

        timer -= Time.deltaTime;
        UpdateTimerUI();
        if (timer <= 0f)
        {
            GameOver();
        }
    }

    private void GameOver()
    {   
        player.GetComponent<PlayerMovement>().DisableMovement();
        isGameOver = true;
        Debug.Log("Game Over! Zeit ist abgelaufen.");
        timer = 0f;
        UpdateTimerUI();

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true); 
        }
    }

      public void Restart()
    {
        isGameOver = false;
        timer = gameDuration;
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false); 
        }
    }

    public void LoadMainMenu(){
        SceneManager.LoadScene("MainMenu");
    }

       void UpdateTimerUI()
    {
        // Zeit formatieren in Minuten und Sekunden
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
