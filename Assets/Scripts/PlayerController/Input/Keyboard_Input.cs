using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard_Input : GameInput {
    private Vector2 MoveDir;
    private float hl;
	private float vt;
	private float Shoot;

    public override Vector2 GetMoveDir() {
        hl = Input.GetAxis("Horizontal");
		vt = Input.GetAxis("Vertical");
        MoveDir = new Vector2(hl, vt).normalized;
        return MoveDir;
    }
    public override Vector2 GetForward() {
        /**************控制旋转***************/
        Vector2 obj = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        Vector2 myMouse = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 forward = myMouse - obj;
        forward = forward.normalized;
        return forward;
    }
    public override float GetInputInteraction_1() {
		Shoot = Input.GetAxis("Shoot");
        return Shoot;
    }
    public override float GetInputInteraction_2() {
		Shoot = Input.GetAxis("ChangeWeapen");
        return Shoot;
    }
}
