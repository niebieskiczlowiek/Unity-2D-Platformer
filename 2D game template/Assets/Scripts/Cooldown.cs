using UnityEngine;

[System.Serializable]
public class Cooldown
{
    [SerializeField] private float cooldownTime;
    private float _nextTime;
    public bool IsCoolingDown => Time.time < _nextTime;
    public void StartCoolDown() => _nextTime = Time.time + cooldownTime;
}