using UnityEngine;

[CreateAssetMenu(fileName = "PlayerState", menuName = "Player/State")]
public class PlayerState : ScriptableObject
{
    public bool isDashing;
    public bool isJumping;
    public bool doubleJumped;
    public bool isSlashing;
    public bool isBeingKnockBacked;

    public bool hitStunActive; 
    // hit stun appears after the player has been hit
    // when it's active the player cannot move
    // it lasts while the player is being knock-backed
    
    // all false by default
}