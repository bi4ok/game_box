using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Factory : MonoBehaviour
{
    [System.Serializable]
    public class ObjToSpawn
    {
        public string name;
        public GameObject prefab;
        public int count;
        public float spawnTimeFrom;
        public bool spawned = false;

    }

    [SerializeField]
    private List<ObjToSpawn> allSpawnObjects;

    [SerializeField]
    private float interval;

    private List<ObjToSpawn> currentSpawnObjects;
    private float _startTime=0;

    private void Start()
    {
        currentSpawnObjects = new List<ObjToSpawn>();
        WhoToSpawnUpdate();
        SpawnObjects();


    }

    private void Update()
    {
        _startTime += Time.deltaTime;
        WhoToSpawnUpdate();
        SpawnObjects();
    }

    private void WhoToSpawnUpdate()
    {
        currentSpawnObjects.Clear();
        foreach (ObjToSpawn obj in allSpawnObjects)
        {
            if (_startTime >= obj.spawnTimeFrom && !obj.spawned)
            {
                currentSpawnObjects.Add(obj);

            }
        }

    }

    private void SpawnObjects()
    {
        foreach (ObjToSpawn obj in currentSpawnObjects)
        {
            StartCoroutine(SpawnObject(interval, obj));
            obj.spawned = true;
        }
    }

    protected abstract IEnumerator SpawnObject(float interval, ObjToSpawn spawnObject);

}
