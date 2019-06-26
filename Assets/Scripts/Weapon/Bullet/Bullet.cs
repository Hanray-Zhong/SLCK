using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject DeadEffect;
    public float Damage;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Map" || other.tag == "Enemy") {
            if (other.gameObject.tag == "Enemy") {
                PlayerUnit u = other.gameObject.GetComponent<PlayerUnit>();
                EnemyAI ai = other.gameObject.GetComponent<EnemyAI>();
                if (u != null) {
                    u.Damage(Damage);
                }
                if (ai != null) {
                    ai.beHit = true;
                }
            }
            Instantiate(DeadEffect, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
        Destroy(gameObject, 1.5f);
    }
}
