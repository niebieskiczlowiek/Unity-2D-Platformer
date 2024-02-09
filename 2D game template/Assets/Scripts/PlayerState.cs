using UnityEngine;

[CreateAssetMenu(fileName = "PlayerState", menuName = "Player/State")]
public class PlayerState : ScriptableObject
{
    public bool isDashing;
    public bool isJumping;
    public bool doubleJumped;
    public bool isSlashing;
    public bool isKnockBacked;

    public bool invinciblitiyOn;
    // all false by default
}