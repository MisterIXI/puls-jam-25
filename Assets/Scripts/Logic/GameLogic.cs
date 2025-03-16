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

    private bool isPaused = false;
    [Header("UI Elements")]
    public GameObject gameOverPanel;
    
    public GameObject pauseMenuPanel;
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

          if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
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
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

       void UpdateTimerUI()
    {
        // Zeit formatieren in Minuten und Sekunden
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    
    public void Pause()
    {

        if (isPaused)
        {

            if (pauseMenuPanel != null)
            {
                pauseMenuPanel.SetActive(false);
            }
            Time.timeScale = 1f;
            isPaused = false;
        }
        else
        {
            if (pauseMenuPanel != null)
            {
                pauseMenuPanel.SetActive(true);
            }
            Time.timeScale = 0f;
            isPaused = true;
        }
    }
}
