using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketScript : MonoBehaviour
{
    [SerializeField] private Text nametext;
    [SerializeField] private Text chtext;
    [SerializeField] private Image mainimage;
    [SerializeField] ProductScript[] Products;
    [SerializeField] private Text buttontext;
    [SerializeField] private GameObject slot;
    [SerializeField] private GameObject parentslot;
    int f;
    private void Awake()
    {
        UpdateInfo(0);
        Slotssort();
    }
    public void UpdateInfo(int i)
    {
        nametext.text = Products[i].name;
        chtext.text = Products[i].health.ToString() + "\n" + Products[i].attack.ToString() + "\n" + Products[i].attackspeed.ToString() + "\n" + Products[i].movespeed.ToString() + "\n" + Products[i].cost.ToString();
        mainimage.sprite = Products[i].artwork;
        buttontext.text = Products[i].isbought == true ? Products[i].ischoosen == true ? "Choosen" : "Choose" : "Buy";


    }
    public void Slotssort()
    {
        for (int d = 0; d == Products.Length;)
        {
            GameObject newProducts = Instantiate(slot, parentslot.transform);
            Slot slotscript = newProducts.GetComponent<Slot>();
            Button buttonSlots = newProducts.GetComponentInChildren<Button>();
            buttonSlots.onClick.AddListener(() => slotscript.Updateinfo());
            slotscript.i = f;
            f++;
            print("dd");
        }
    }
}
