using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimationConstants
{
    // player animation states
    public static string Player_Idle_Back = "Idle_back";
    public static string Player_Idle_side = "Idle_side";
    public static string Player_Idle_front = "Idle_front";
    public static string Player_Walk_front = "Walk_front";
    public static string Player_Walk_back = "Walk_back";
    public static string Player_Walk_side = "Walk_side";

    // door
    public static string Door_Open = "doorOpen";
    public static string Door_Close = "doorClose";

    // spirit enemy
    public static string Spirit_Idle = "enemyDistanceIdle";
    public static string Spirit_Spit = "enemyDistanceSpit";
    public static string Spirit_Hit = "enemyDistanceHit";
    public static string Spirit_Walk = "enemyDistanceWalk";
    public static string Spirit_Die = "enemyDistanceDie";
}

