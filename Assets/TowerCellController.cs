using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCellController : MonoBehaviour
{
    [SerializeField]
    private GameObject towerBuyCanvas;

    private SpriteRenderer spriteHandler;
    private Color originalColor;

    private void Start()
    {
        spriteHandler = GetComponent<SpriteRenderer>();
        originalColor = spriteHandler.color;
        towerBuyCanvas.SetActive(false);
    }

    private void OnMouseDown()
    {
        towerBuyCanvas.SetActive(true);
        towerBuyCanvas.GetComponent<TowerController>().GetNewCell(gameObject);
    }

    private void OnMouseEnter()
    {
        spriteHandler.color = Color.yellow;
    }

    private void OnMouseExit()
    {
        spriteHandler.color = originalColor;
    }

    public void MakeNewTower(GameObject towerPrefab)
    {
        GameObject newTower = Instantiate(towerPrefab, transform);
    }
}
