using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerController : MonoBehaviour
{
    [System.Serializable]
    public class TowerToBuy
    {
        public string name;
        public GameObject prefabTowerInGame;
        public GameObject prefabTowerInUI;
        public Sprite imgOfTower;
        public int cost;

    }

    [SerializeField]
    private List<TowerToBuy> allTowerObjects;
    [SerializeField]
    private GameObject scrollMenu;

    private GameObject _currentCell;

    private void OnEnable()
    {
        foreach(TowerToBuy tower in allTowerObjects)
        {
            GameObject newTowerUI = Instantiate(tower.prefabTowerInUI, scrollMenu.transform);
            Button buyTowerButton = newTowerUI.GetComponentInChildren<Button>();
            buyTowerButton.onClick.AddListener(() => BuyTower(tower.prefabTowerInGame, _currentCell));
        }
    }

    private void BuyTower(GameObject towerPrefab, GameObject cellForTower)
    {
        cellForTower.GetComponent<TowerCellController>().MakeNewTower(towerPrefab);
        print("TOWER BUy!");
    }

    public void GetNewCell(GameObject newCell)
    {
        _currentCell = newCell;
    }

}
