using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritEnemy : Enemy
{
    [SerializeField]
    GameObject witnessAfterDead;
    public override void OnDead()
    {
        base.OnDead();
        Animator.Play(AnimationConstants.Spirit_Die);
    }

    public override void DoAttackAnimation()
    {
        base.DoAttackAnimation();
        WolfAudioSystem.AudioSystem.Instance.PlaySFXOnCamera(GameCore.GameManager.Instance.ResourceLocator.audioSetup.SpiritStab);
        Animator.Play(AnimationConstants.Spirit_Spit);
    }

    public override void DoIdleAnimation()
    {
        base.DoIdleAnimation();
        Animator.Play(AnimationConstants.Spirit_Idle);
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
    public override void OnDeadAnimationFinished()
    {
        base.OnDeadAnimationFinished();
        GameObject tar = GameCore.GameManager.Instance.enemyPoop[Random.Range(0, GameCore.GameManager.Instance.enemyPoop.Count)];
        Instantiate(tar, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}