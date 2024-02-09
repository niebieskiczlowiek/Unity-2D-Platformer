using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnlockedAbilities", menuName = "Character/Abilities")]
public class UnlockedAbilities : ScriptableObject
{
    public bool dashAbility = false;
    public bool doubleJumpAbility = false;
}
