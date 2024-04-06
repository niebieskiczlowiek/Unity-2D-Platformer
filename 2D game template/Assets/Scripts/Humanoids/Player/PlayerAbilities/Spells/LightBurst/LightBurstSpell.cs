using Unity.VisualScripting;
using UnityEngine;

public class LightBurstSpell : Spell
{
    public void Update()
    {
        if (Input.GetButtonDown("Cast") &&  CanCast()) Cast();
    }

    protected override void Cast()
    {
        Debug.Log("Light Burst casted");
        // bool isFacingRight = gameObject.transform.
        var burst = Instantiate(spellObject, transform.position, transform.rotation);
        Vector3 localScale = burst.transform.localScale;
        localScale.x *= gameObject.transform.localScale.x;
        burst.transform.localScale = localScale;
        UpdateEnergyAfterCast();
    }
}