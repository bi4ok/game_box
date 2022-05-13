using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float respawnKD;
    private PlayerController character;
    void Start()
    {
        character = player.GetComponent<PlayerController>();
    }

    public IEnumerator Respawn()
    {   //Анимация смерти
        player.SetActive(false);
       
        yield return new WaitForSeconds(respawnKD);
        player.transform.position = transform.position;
        player.SetActive(true);
        character.TakeHeal(5);
    }

}
