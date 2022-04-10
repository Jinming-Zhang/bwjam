using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamePlay;
using System;
using GamePlay.Projectiles;

public class Boss : MonoBehaviour, IDamagable
{
	[Header("Movement Templates")]
	[SerializeField]
	MovementBehaviour defaultMovementBehaviourTemplate;
	[SerializeField]
	AIBossAbilityHitMovementBehavour defaultAbilityHitMovementBehaviour;
	[Header("Weapon Position")]
	public Transform defaultWeaponTransform;
	public Transform meleeWeaponTransform;
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
		if (currentMovementBehaviour)
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
			if (fleetCR == null)
			{
				currentMovementBehaviour = Instantiate(defaultMovementBehaviourTemplate);
				currentMovementBehaviour.Initialize(gameObject, defaultWeaponTransform);
				switchBehaviourCR = null;
			}
		}
	}

	public void TakeDamage(float amount, MonoBehaviour source, IDamagable.DamageType damageType = IDamagable.DamageType.Health)
	{
		if (source.GetComponent<PlayerWeaponProjectile>())
		{
			health.Value = Mathf.Max(0, health.Value - Mathf.FloorToInt(amount));
			healthbar.fillAmount = health.Value / (float)health.MaxValue;
			if (health.Value <= 0)
			{
				Die();
			}
			//else
			//{
			HitByPlayer();
			//}
		}
	}
	void HitByPlayer()
	{
		if (fleetCR == null && fleetCDTimer <= 0)
		{
			GameObject[] corners = GameObject.FindGameObjectsWithTag("RoomCorner");
			Vector3 playerPosition = GameCore.GameManager.Instance.Player.transform.position;
			Vector3 targetLocation = corners[0].transform.position;

			foreach (GameObject corner in corners)
			{
				if (Vector2.Distance(corner.transform.position, playerPosition)
					> Vector2.Distance(corner.transform.position, targetLocation))
				{
					targetLocation = corner.transform.position;
				}
			}
			Fart();
			MoveToTargetPosition(targetLocation);
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
			currentMovementBehaviour.Initialize(gameObject, defaultWeaponTransform);
		}
	}

	private void Fart()
	{
		Instantiate(perfume, transform.position, Quaternion.identity);
	}

	void Die()
	{

	}
	private void OnDrawGizmos()
	{
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(meleeWeaponTransform.position, meleeAttackRange);
	}
}

