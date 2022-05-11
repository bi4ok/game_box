using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBonus : Bonus
{
    [SerializeField]
    private float shieldBuffTime;

    void Start()
    {
        base.OnCreate();
    }

    protected override IEnumerator BonusActivateEffect(Collider2D collision)
    {
        PlayerController playerScript = collision.GetComponent<PlayerController>();
        Item shield = new Item(energyPowerPlus: 1);
        playerScript.EquipItem(shield, 5);
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);

    }
}
