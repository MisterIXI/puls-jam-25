using UnityEngine;

public class ReelGame : MonoBehaviour
{

    [SerializeField] private ReelFish _fish;
    [SerializeField] private ReelZone _reelZone;
    [SerializeField] private FishingProgressBar _progressBar;
    [SerializeField] private GameObject _reelBackground;
    [SerializeField] private AudioClip reelSound;
    [SerializeField] private AudioClip[] fishCaughtSounds;
    public static ReelGame Instance;
    private AudioSource _audioSource;
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this);
        _progressBar.OnProgressComplete += () => EndGame(true);
        _progressBar.OnProgressLose += () => EndGame(false);
    }
    public void StartNewGame()
    {
        gameObject.SetActive(true);
        _audioSource = AudioManager.Instance.PlayClip(reelSound, transform.position, true, PlayerPrefs.GetFloat("soundVolume"), Random.Range(0.9f, 1.1f));
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
        _fish.RandomPosition();
        _reelZone.ResetPosition();
        _progressBar.SetProgress(0.33f);
        PlayerMovement.Instance.DisableMovement();
    }

    public void EndGame(bool hasWon)
    {
        if(gameObject.activeInHierarchy == false) return;
        try
        {
            // if (!hasWon) return;
            _audioSource.Stop();
            FindFirstObjectByType<PlayerBreakingIce>().enabled = true;
            gameObject.SetActive(false);
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }

            if (hasWon)
            {
                PlayerFishCounter.Instance.AddFish();
                PlayerTriggerCheck.Instance.DestroyFishSwarm();
                FishSwarmSpawnManager.Instance.SpawnFishSwarm();
                AudioManager.Instance.PlayClip(fishCaughtSounds[Random.Range(0, fishCaughtSounds.Length)], transform.position, PlayerPrefs.GetFloat("soundVolume") * 1.3f, Random.Range(0.9f, 1.1f));
                Debug.Log("You caught a fish!");

            }
            else
            {
                PlayerTriggerCheck.Instance.DestroyFishSwarm();
                FishSwarmSpawnManager.Instance.SpawnFishSwarm();
                Debug.Log("You lost the fish!");
            }
        }
        finally
        {
            // emergency movement enable on any error
            PlayerMovement.Instance.EnableMovement();
        }
    }
}
