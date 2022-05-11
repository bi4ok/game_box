using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFactory : Factory
{

    [SerializeField]
    private Rigidbody2D targetToFollow;

    [SerializeField]
    private GameObject bonusHandler;

    protected override IEnumerator SpawnObject(float interval, ObjToSpawn spawnObject)
    {

        for (int i = 0; i < spawnObject.count; i++)
        {
            yield return new WaitForSeconds(interval);
            GameObject objFromPrefab = Instantiate(spawnObject.prefab, transform.position, Quaternion.identity);
            objFromPrefab.SetActive(true);
            var objScript = objFromPrefab.GetComponent<MonsterController>();
            if (targetToFollow != null)
            {
                objScript.OnCreate(targetToFollow, bonusHandler);
            }


        }


    }
}
