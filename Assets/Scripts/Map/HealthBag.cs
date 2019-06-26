using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBag : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            PlayerUnit u = other.gameObject.GetComponent<PlayerUnit>();
            if (u != null && u.Health != 100) {
                u.Health = 100;
                Destroy(gameObject);
            }
        }
    }
}
