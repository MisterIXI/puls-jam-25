using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private bool _isInvincible = false;

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
        Debug.Log("Player sunk");
    }
}
