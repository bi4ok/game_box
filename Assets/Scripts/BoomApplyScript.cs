using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BoomApplyScript : MonoBehaviour
{

    public float damage;
    public float radius;

    private PlayableDirector director;

    private void Awake()
    {
        director = GetComponent<PlayableDirector>();
    }

    private void Start()
    {
        gameObject.transform.localScale.Set(radius, radius, 0.1f);
        //Destroy(gameObject, 0.1f);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        IDamageAble enemy = collision.GetComponent<IDamageAble>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage, tag);
        }
    }

    public void StartTimeline()
    {
        director.Play();
    }

    public void DestroyEffect()
    {
        Destroy(gameObject);
    }

}
