using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketInfo : MonoBehaviour
{
    [SerializeField]  private Text buttontext;
    [SerializeField] private Text nametext;
    [SerializeField] private Text chtext;
    [SerializeField] private Image mainimage;
    public void UpdateInfo(ProductScript products)
    {
        nametext.text = products.name;
        chtext.text = products.health.ToString() + "\n" + products.attack.ToString() + "\n" + products.attackspeed.ToString() + "\n" + products.movespeed.ToString() + "\n" + products.cost.ToString();
        mainimage.sprite = products.artwork;
        buttontext.text = products.isbought == true ? products.ischoosen == true ? "Choosen" : "Choose" : "Buy";
    }
}
