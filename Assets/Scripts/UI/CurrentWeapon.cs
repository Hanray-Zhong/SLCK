using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentWeapon : MonoBehaviour
{
    public GameObject Player;
    public GameObject[] Images;

    private Gun currentWeapon;
    private void Update() {
        if (Player == null)
            return;
        currentWeapon = Player.GetComponent<PlayerController>().Used_Gun;
        switch (currentWeapon) {
            case (Gun)0: Images[0].SetActive(true);
                    Images[1].SetActive(false);
                    Images[2].SetActive(false);
                    break;
            case (Gun)1: Images[0].SetActive(false);
                    Images[1].SetActive(true);
                    Images[2].SetActive(false);
                    break;
            case (Gun)2: Images[0].SetActive(false);
                    Images[1].SetActive(false);
                    Images[2].SetActive(true);
                    break;
            default: break;
        }
    }
}
