using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    
    public GameObject Player;
    // Update is called once per frame
    void Update() {
        if (Player != null) {
            gameObject.GetComponent<Text>().text = "Score : " + Player.GetComponent<PlayerController>().Score.ToString();
        }
    }
}
