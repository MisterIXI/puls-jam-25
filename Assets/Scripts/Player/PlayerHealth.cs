using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private bool _isInvincible = false;
    [SerializeField] private AudioClip[] fallIntoWaterSound;

    public void SetIsInvincible(bool value)
    {
        _isInvincible = value;        
    }

    public bool GetIsInvincible()
    {
        return _isInvincible;
    }

    public void PlayerSunk()
    {
        ReelGame.Instance.EndGame(false);
        GetComponent<PlayerFishCounter>().ClearFishCounter();
        AudioManager.Instance.PlayClip(fallIntoWaterSound[Random.Range(0, fallIntoWaterSound.Length)], transform.position, PlayerPrefs.GetFloat("soundVolume"), Random.Range(0.9f, 1.1f));
        RespawnManager.Instance.RespawnPlayer();
    }
}
