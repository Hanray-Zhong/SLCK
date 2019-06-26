using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : MonoBehaviour
{
    public float Health = 100;
    public PlayerController Player;
    [Header("Death")]
    public bool IsDead = false;
    public GameObject DeadEffect;
    public GameObject DeadUI;
    
    private void Awake() {
        if (GameObject.FindGameObjectWithTag("Player") != null)
            Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void Damage(float damage) {
        gameObject.GetComponent<PlayerUnit>().Health -= damage;
        if (gameObject.GetComponent<PlayerUnit>().Health <= 0) {
            IsDead = true;
            if (gameObject.tag == "Enemy" && Player != null) {
                if (gameObject.GetComponent<EnemyAI>().Used_Gun == Gun.Shotgun)
                    Player.Shotgun_Number += 12;
                else if (gameObject.GetComponent<EnemyAI>().Used_Gun == Gun.Sniper_rifle)
                    Player.Sniper_rifle_Number += 5;
                Player.Score += 10;
            }
            if (gameObject.tag == "Player")
                DeadUI.SetActive(true);
            Instantiate(DeadEffect, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
    }
}
