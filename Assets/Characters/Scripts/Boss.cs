using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamePlay;
using System;
using GamePlay.Projectiles;
using System.Linq;

public class Boss : MonoBehaviour, IDamagable
{
    [Header("Movement Templates")]
    [SerializeField]
    BossDefaultMovement defaultMovementBehaviourTemplate;
    [SerializeField]
    AIBossAbilityHitMovementBehavour defaultAbilityHitMovementBehaviour;
    [Header("Weapon Position")]
    public Transform defaultWeaponTransform;
    public Transform meleeWeaponTransform;
    public float defaultAttackRange = 2f;
    public float meleeAttackRange = 2f;
    [Header("Others")]
    [SerializeField]
    Rigidbody2D rb;
    [SerializeField]
    Health health;
    [SerializeField]
    float fleeSpeed;
    [SerializeField]
    float fleetCD = 10f;
    float fleetCDTimer = 0;
    [SerializeField]
    GameObject perfume;
    MovementBehaviour currentMovementBehaviour;

    [SerializeField]
    UnityEngine.UI.Image healthbar;
    Coroutine switchBehaviourCR;
    Coroutine fleetCR;
    [SerializeField]
    Animator animator;
    bool isDying;
    // Start is called before the first frame update
    void Start()
    {
        SwitchToDefaultMoveBehaviour();
        healthbar.fillAmount = 1f;
    }
    private void Update()
    {
        fleetCDTimer = Mathf.Max(0, fleetCDTimer - Time.deltaTime);
    }
    private void FixedUpdate()
    {
        if (currentMovementBehaviour && !isDying)
        {
            currentMovementBehaviour.UpdateMovement();
        }
    }
    public void DefaultProjectileHitPlayer(float charmingDuration)
    {
        if (fleetCR != null)
        {
            return;
        }
        AIBossAbilityHitMovementBehavour b = Instantiate(defaultAbilityHitMovementBehaviour);
        b.Initialize(gameObject, charmingDuration);
        currentMovementBehaviour = b;
    }
    #region animation calls
    public void DoMeleeAttackAnimation()
    {
        PlayAnimation(AnimationConstants.Boss_Gun);
    }
    public void DoWalkingAnimation()
    {
        PlayAnimation(AnimationConstants.Boss_Float);
    }

    public void DoKissAnimation()
    {
        WolfAudioSystem.AudioSystem.Instance.PlaySFXOnCamera(GameCore.GameManager.Instance.ResourceLocator.audioSetup.BossKiss);
        PlayAnimation(AnimationConstants.Boss_Kiss);
    }

    void PlayAnimation(string state)
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName(state))
        {
            animator.SetTrigger(state);
        }
    }
    #endregion
    public void SwitchToDefaultMoveBehaviour()
    {
        if (switchBehaviourCR == null)
        {
            switchBehaviourCR = StartCoroutine(DelayCR(2f));
        }
        IEnumerator DelayCR(float timer)
        {
            currentMovementBehaviour = null;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            yield return new WaitForSeconds(timer);
            if (fleetCR == null)
            {
                currentMovementBehaviour = Instantiate(defaultMovementBehaviourTemplate);
                currentMovementBehaviour.Initialize(gameObject);
                (currentMovementBehaviour as BossDefaultMovement).Setup(this, rb, defaultWeaponTransform, defaultAttackRange);
                switchBehaviourCR = null;
            }
        }
    }

    public void TakeDamage(float amount, MonoBehaviour source, IDamagable.DamageType damageType = IDamagable.DamageType.Health)
    {
        if (!isDying)
        {
            if (source.GetComponent<PlayerWeaponProjectile>())
            {
                health.Value = Mathf.Max(0, health.Value - Mathf.FloorToInt(amount));
                healthbar.fillAmount = health.Value / (float)health.MaxValue;
                if (health.Value <= 0)
                {
                    isDying = true;
                    rb.velocity = Vector2.zero;
                    PlayAnimation(AnimationConstants.Boss_Die);
                }
                else
                {
                    HitByPlayer();
                }
            }
        }
    }
    void HitByPlayer()
    {
        if (fleetCR == null && fleetCDTimer <= 0 && !isDying)
        {
            GameObject[] corners = GameObject.FindGameObjectsWithTag("RoomCorner");
            float minDist = float.MaxValue;
            GameObject closestCorner = corners[0];

            foreach (GameObject corner in corners)
            {
                float distance = Vector3.Distance(corner.transform.position, gameObject.transform.position);
                if (distance < minDist)
                {
                    minDist = distance;
                    closestCorner = corner;
                }
            }
            List<GameObject> candidates = corners.ToList();
            candidates.Remove(closestCorner);
            Vector3 targetLocation = candidates[UnityEngine.Random.Range(0, candidates.Count)].transform.position;

            Fart();
            PlayAnimation(AnimationConstants.Boss_Perfume);
            MoveToTargetPosition(targetLocation);
        }
        else
        {
            PlayAnimation(AnimationConstants.Boss_Hit);
        }
    }

    private void MoveToTargetPosition(Vector3 targetLocation)
    {
        currentMovementBehaviour = null;
        fleetCR = StartCoroutine(FleetCR());
        IEnumerator FleetCR()
        {
            fleetCDTimer = fleetCD;
            yield return new WaitForFixedUpdate();
            Vector3 distanceVector = targetLocation - transform.position;
            float timer = distanceVector.magnitude / fleeSpeed;
            rb.velocity = distanceVector.normalized * fleeSpeed;
            yield return new WaitForSeconds(timer);
            fleetCR = null;
            currentMovementBehaviour = Instantiate(defaultMovementBehaviourTemplate);
            currentMovementBehaviour.Initialize(gameObject);
            (currentMovementBehaviour as BossDefaultMovement).Setup(this, rb, defaultWeaponTransform, defaultAttackRange);
        }
    }

    private void Fart()
    {
        Instantiate(perfume, transform.position, Quaternion.identity);
    }

    public void TheEnd()
    {
        Destroy(gameObject);
        GameCore.GameManager.Instance.GameFinish(true);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(defaultWeaponTransform.position, meleeAttackRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(defaultWeaponTransform.position, defaultAttackRange);
    }
}

