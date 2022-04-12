using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandmaEnemy : Enemy
{
    [SerializeField]
    GameObject goodGrandma;

    public override void OnDead()
    {
        base.OnDead();
        Animator.Play(AnimationConstants.Spirit_Die);
    }
    public override void OnDeadAnimationFinished()
    {
        base.OnDeadAnimationFinished();
        // spawn good grandma
        Instantiate(goodGrandma, transform.position, Quaternion.identity);
    }

    public override void DoIdleAnimation()
    {
        base.DoIdleAnimation();
        Animator.Play(AnimationConstants.Spirit_Idle);
    }
    public override void DoAttackAnimation()
    {
        base.DoAttackAnimation();
        WolfAudioSystem.AudioSystem.Instance.PlaySFXOnCamera(GameCore.GameManager.Instance.ResourceLocator.audioSetup.rangeEnemyAttack);
        Animator.Play(AnimationConstants.Spirit_Spit);
    }
    public override void DoDamagedAnimation()
    {
        base.DoDamagedAnimation();
        Animator.Play(AnimationConstants.Spirit_Hit);
    }
    public override void DoWalkAnimation()
    {
        base.DoWalkAnimation();
        Animator.Play(AnimationConstants.Spirit_Walk);
    }

}
