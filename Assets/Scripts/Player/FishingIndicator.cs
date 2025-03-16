using UnityEngine;

public class FishingIndicator : MonoBehaviour
{
    [SerializeField] private GameObject fishingIndicator;
    [SerializeField] private ReelGame _reelGame;
    private PlayerTriggerCheck _playerTriggerCheck;
    private void Awake()
    {
        _playerTriggerCheck = GetComponent<PlayerTriggerCheck>();
        _playerTriggerCheck.CanInteractHasChanged += OnCanInteractHasChanged;
        _reelGame.GameStarted += OnGameStarted;
        _reelGame.GameEnded += OnGameEnded;
    }

    private void OnCanInteractHasChanged()
    {
        fishingIndicator.SetActive(_playerTriggerCheck.CanInteract);
    }

    private void OnGameStarted()
    {
        fishingIndicator.SetActive(false);
    }

    private void OnGameEnded()
    {
        if (_playerTriggerCheck.CanInteract)
            fishingIndicator.SetActive(true);
    }

    private void OnDestroy()
    {
        try
        {
            _playerTriggerCheck.CanInteractHasChanged -= OnCanInteractHasChanged;
            _reelGame.GameStarted -= OnGameStarted;
            _reelGame.GameEnded -= OnGameEnded;
        }
        catch  
        {
            // ignore
        }
    }
}
