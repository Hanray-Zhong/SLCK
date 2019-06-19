using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public float Damage;

    private Animator anim;
    private AnimatorStateInfo info;

    private void Start() {
        anim = gameObject.GetComponent<Animator>();
    }
    private void Update() {
        info = anim.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("WeaponHit") && info.normalizedTime > 0.5) {
            // Debug.Log("get");
            anim.SetBool("Attack", false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            PlayerUnit u = other.gameObject.GetComponent<PlayerUnit>();
            if (u != null) {
                u.Damage(Damage);
            }
        }
    }
}
