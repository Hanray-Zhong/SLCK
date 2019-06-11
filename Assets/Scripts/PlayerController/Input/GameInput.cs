using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameInput : MonoBehaviour {
    public abstract Vector2 GetMoveDir();

    public abstract Vector2 GetForward();
    public abstract float GetInputInteraction_1();
    public abstract float GetInputInteraction_2();
}
