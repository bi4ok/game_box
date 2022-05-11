using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockBonus : Bonus
{
    [SerializeField, Range(0.1f, 1)]
    private float timeScale;
    [SerializeField]
    private float timerOfBonus;
    [SerializeField]
    private GameObject freezePanel;

    private void Start()
    {
        freezePanel.SetActive(false);
        base.OnCreate();
    }
    protected override IEnumerator BonusActivateEffect(Collider2D collision)
    {
        freezePanel.SetActive(true);
        Time.timeScale = timeScale;
        PlayerController playerScript = collision.GetComponent<PlayerController>();
        Item clock = new Item(movementSpeedPercent: 100/timeScale);
        Debug.Log(1 / timeScale);
        playerScript.EquipItem(clock, timerOfBonus * timeScale);
        yield return new WaitForSeconds(timerOfBonus * timeScale);
        freezePanel.SetActive(false);
        Time.timeScale = 1;
        Destroy(gameObject);

    }
}
