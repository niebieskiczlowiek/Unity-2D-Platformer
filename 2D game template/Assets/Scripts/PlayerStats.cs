using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Player/Stats")]
public class PlayerStats : ScriptableObject
{
    public int maxHealth = 5;
    // to keep health consistent between scenes
    public int currentHealth = 5;
}