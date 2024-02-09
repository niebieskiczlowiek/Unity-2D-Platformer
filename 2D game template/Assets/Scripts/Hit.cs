using System.Collections;
using UnityEngine;

public class Hit
{
    private readonly int _damageDealt;
    private readonly Vector2 _hitDirection;

    public Hit(int damageDealt, Vector2 hitDirection)
    {
        _damageDealt = damageDealt;
        _hitDirection = hitDirection;
    }

    // Getters
    public int GetDamageDealt() { return _damageDealt; }
    public Vector2 GetHitDirection() { return _hitDirection; }
}

/* Hit object is created when a Unity humanoid is hit. The object contains details necessary to process things like
damage dealt to the Humanoid or the knockback they will receive */