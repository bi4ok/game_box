using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Product",menuName ="ProductforMarket" )]
public class ProductScript : ScriptableObject
{
    public  string nameofProduct;
    public int cost;
    public int attack;
    public int health;
    public int attackspeed;
    public int movespeed;
    public Sprite artwork;
    public bool isbought;
    public bool ischoosen;

}
