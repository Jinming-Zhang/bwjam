using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamePlay;

public class Boss : MonoBehaviour
{
    [Header("Movement Templates")]
    [SerializeField]
    MovementBehaviour defaultMovementBehaviourTemplate;
    [SerializeField]
    AIBossAbilityHitMovementBehavour defaultAbilityHitMovementBehaviour;
    [SerializeField]
    public Transform defaultWeaponTransform;
    public Transform meleeWeaponTransform;
    MovementBehaviour currentMovementBehaviour;

    Coroutine switchBehaviourCR;
    // Start is called before the first frame update
    void Start()
    {
        SwitchToDefaultMoveBehaviour();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        if (currentMovementBehaviour)
        {
            currentMovementBehaviour.UpdateMovement();
        }
    }
    public void DefaultProjectileHitPlayer(float charmingDuration)
    {
        AIBossAbilityHitMovementBehavour b = Instantiate(defaultAbilityHitMovementBehaviour);
        b.Initialize(gameObject, charmingDuration);
        currentMovementBehaviour = b;
    }
    public void DoMeleeAttackAnimation()
    {

    }

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
            currentMovementBehaviour = Instantiate(defaultMovementBehaviourTemplate);
            currentMovementBehaviour.Initialize(gameObject, defaultWeaponTransform);
            switchBehaviourCR = null;
        }
    }
}
