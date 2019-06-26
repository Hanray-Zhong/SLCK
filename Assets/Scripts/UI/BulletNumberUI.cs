using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletNumberUI : MonoBehaviour
{
    public Gun Used_Gun;
    public GameObject Player;

    // Update is called once per frame
    void Update() {
        if (Player == null)
            return;
        if (Used_Gun == Gun.Shotgun) {
            gameObject.GetComponent<Text>().text = Player.GetComponent<PlayerController>().Shotgun_Number.ToString();
        }
        else if (Used_Gun == Gun.Sniper_rifle) {
            gameObject.GetComponent<Text>().text = Player.GetComponent<PlayerController>().Sniper_rifle_Number.ToString();
        }
    }
}
