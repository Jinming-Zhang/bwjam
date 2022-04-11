using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadAnimationFinished : MonoBehaviour
{
    [SerializeField]
    Enemy me;
    void OnDeadFinished()
    {
        if (me)
        {
            me.OnDeadAnimationFinished();
        }
    }
}
