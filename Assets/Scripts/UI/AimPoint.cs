using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimPoint : MonoBehaviour
{
    private void Update() {
        Cursor.visible = false;
        gameObject.transform.position = Input.mousePosition;
    }
}
