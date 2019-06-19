using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    public GameObject Player;

    private void Update() {
        float Health = Player.GetComponent<PlayerUnit>().Health;
        gameObject.GetComponent<RectTransform>().offsetMax = new Vector2(-332 * (100 - Health) / 100, 0);
        gameObject.GetComponent<RectTransform>().offsetMin = new Vector2(-322 * (100 - Health) / 100, 0);
    }
}
