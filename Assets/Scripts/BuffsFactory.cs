using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffsFactory : Factory
{

    [SerializeField]
    private float timeToBonusLive;

    [SerializeField]
    private GameObject bonusHandler;

    protected override IEnumerator SpawnObject(float interval, ObjToSpawn spawnObject)
    {

        for (int i = 0; i < spawnObject.count; i++)
        {
            yield return new WaitForSeconds(interval);
            GameObject objFromPrefab = Instantiate(spawnObject.prefab, transform.position, Quaternion.identity);
            var objScript = objFromPrefab.GetComponent<Bonus>();
            objScript.aliveBonusTimer = timeToBonusLive;
            objScript.bonusHandler = bonusHandler;
            objFromPrefab.SetActive(true);
            
            


        }


    }
}
