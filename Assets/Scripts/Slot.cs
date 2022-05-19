using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] private MarketScript marketScript;
    public int i;
    public ProductScript productScript;
    [SerializeField] private Image slotimage;
    private void Start()
    {
        slotimage.sprite = productScript.artwork;
        marketScript = FindObjectOfType<MarketScript>();
    }

    public void Updateinfo()
    {
        marketScript.UpdateInfo(i);
        print(i);
    }

}
