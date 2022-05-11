using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    [SerializeField]
    private float timeToBoom;
    [SerializeField]
    private float damageOfBoom;
    [SerializeField]
    private float radiusOfBoom;
    [SerializeField]
    private GameObject boomEffect;
    [SerializeField]
    private float aliveBonusTimer;

    private float standartBoomTime = 5f;
    private Animator _bombAnimator;
    private bool isActivated=false;


    private void Start()
    {
        _bombAnimator = GetComponent<Animator>();
        _bombAnimator.speed = standartBoomTime / timeToBoom;
        StartCoroutine(BombDissappear());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(StartToTick());
        }
    }

    private IEnumerator StartToTick()
    {
        _bombAnimator.SetTrigger("Boom");
        isActivated = true;
        yield return new WaitForSeconds(timeToBoom);
        GameObject objFromPrefab = Instantiate(boomEffect, transform.position, Quaternion.identity);
        BoomApplyScript boomScript = objFromPrefab.GetComponent<BoomApplyScript>();
        boomScript.damage = damageOfBoom;
        boomScript.radius = radiusOfBoom;
        objFromPrefab.SetActive(true);
        Destroy(gameObject);

    }

    private IEnumerator BombDissappear()
    {
        yield return new WaitForSeconds(aliveBonusTimer);
        if (!isActivated)
        {
            Destroy(gameObject);
        }
    }

}
