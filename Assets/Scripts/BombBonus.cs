using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BombBonus : Bonus
{
    [SerializeField]
    private float timeToBoom;
    [SerializeField]
    private float damageOfBoom;
    [SerializeField]
    private float radiusOfBoom;
    [SerializeField]
    private GameObject boomEffect;

    private float standartBoomTime = 5f;
    private PlayableDirector director;

    private void Awake()
    {
        director = GetComponent<PlayableDirector>();
        director.RebuildGraph();
        director.playableGraph.GetRootPlayable(0).SetSpeed(standartBoomTime / timeToBoom);
    }

    private void Start()
    {
        base.OnCreate();
    }

    protected override IEnumerator BonusActivateEffect(Collider2D collision)
    {
        director.Play();
        yield return new WaitForSeconds(timeToBoom);
        GameObject objFromPrefab = Instantiate(boomEffect, transform.position, Quaternion.identity);
        BoomApplyScript boomScript = objFromPrefab.GetComponent<BoomApplyScript>();
        boomScript.damage = damageOfBoom;
        boomScript.radius = radiusOfBoom;
        objFromPrefab.SetActive(true);
        boomScript.StartTimeline();
        Destroy(gameObject);

    }

}
