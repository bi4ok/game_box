using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedPackBonus : Bonus
{
    [SerializeField]
    private float healAmount;

    private void Start()
    {
        base.OnCreate();
    }
    protected override IEnumerator BonusActivateEffect(Collider2D collision)
    {
        yield return new WaitForSeconds(0);
        PlayerController playerScript = collision.GetComponent<PlayerController>();
        playerScript.TakeHeal(healAmount);
        Destroy(gameObject);

    }
}
