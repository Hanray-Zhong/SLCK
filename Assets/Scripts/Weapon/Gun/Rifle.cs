using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Gun {
    Rifle,
    Sniper_rifle,
    Shotgun,
}

public static class Rifle
{
    public static int ShootNumPerTime = 1;
    public static float ShootForce = 10;
    public static float BulletDamage = 20;
    public static float ShootCD = 15;
}
