using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    public GameObject DeadEffect;
    public float Damage;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Map" || other.tag == "Player" || other.tag == "Knife") {
            if (other.gameObject.tag == "Player") {
                PlayerUnit u = other.gameObject.GetComponent<PlayerUnit>();
                if (u != null) {
                    u.Damage(Damage);
                }
            }
            Instantiate(DeadEffect, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
        Destroy(gameObject, 1.5f);
    }
}