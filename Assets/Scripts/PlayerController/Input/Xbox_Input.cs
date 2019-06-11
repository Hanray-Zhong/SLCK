using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xbox_Input : GameInput {
    private Vector2 MoveDir;
    private float hl;
	private float vt;
	private float Shoot;

    public override Vector2 GetMoveDir() {
        hl = Input.GetAxis("Horizontal_Xbox");
		vt = Input.GetAxis("Vertical_Xbox");
        MoveDir = new Vector2(hl, vt).normalized;
        return MoveDir;
    }
    public override Vector2 GetForward() {
        return Vector2.zero;
    }
    public override float GetInputInteraction_1() {
		Shoot = Input.GetAxis("Shoot_XBox");
        return Shoot;
    }
    public override float GetInputInteraction_2() {
		Shoot = Input.GetAxis("ChangeWeapon_XBox");
        return Shoot;
    }
}
