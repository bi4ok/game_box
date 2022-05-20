using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public ProductScript productScript;
   [SerializeField] private Image slotimage;

   
    public void OnCreate(ProductScript product)
    {
        productScript = product;
        slotimage.sprite = product.artwork;
    }

}
