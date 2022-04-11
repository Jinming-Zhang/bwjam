using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationEvents : MonoBehaviour
{
    [SerializeField]
    Boss boss;
    public void OnDeadFinished()
    {
        boss.TheEnd();
    }
}
