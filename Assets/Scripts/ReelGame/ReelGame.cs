using UnityEngine;

public class ReelGame : MonoBehaviour
{

    [SerializeField] private ReelFish _fish;
    [SerializeField] private ReelZone _reelZone;
    [SerializeField] private FishingProgressBar _progressBar;
    [SerializeField] private GameObject _reelBackground;
    private void Awake()
    {
        _progressBar.OnProgressComplete += () => EndGame(true);
        _progressBar.OnProgressLose += () => EndGame(false);
    }
    public void StartNewGame()
    {
        gameObject.SetActive(true);
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
        _fish.RandomPosition();
        _reelZone.ResetPosition();
        _progressBar.SetProgress(0.0f);

    }

    public void EndGame(bool hasWon)
    {
        if (!hasWon) return;
        gameObject.SetActive(false);
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        if (hasWon)
        {
            PlayerFishCounter.Instance.AddFish();
            Debug.Log("You caught a fish!");
        }
        else
        {
            Debug.Log("You lost the fish!");
        }
    }
}
