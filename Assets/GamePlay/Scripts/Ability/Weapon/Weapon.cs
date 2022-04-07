using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WolfAudioSystem;

namespace GamePlay.Weapons
{
    public abstract class Weapon : ScriptableObject, IManagedAudioSource
    {
        [SerializeField]
        public float criticalHitProbabilityMultiplier = 1f;
        [SerializeField]
        public float criticalHitDmgMultiplier = 1f;

        [Header("Weapon Audio")]
        [SerializeField] AudioClip fireSFX;
        [SerializeField] AudioSource weaponAudioSource;

        public float InitialVolume => 1f;

        public AudioSource AudioSource => weaponAudioSource;

        public GameObject owner;
        public virtual void Initialize(GameObject owner, params object[] args)
        {
            this.owner = owner;
        }
        public virtual void Fire(Transform pos, Vector2 direction)
        {
            if (fireSFX && weaponAudioSource && AudioSystem.Instance)
            {
                AudioSystem.Instance.PlaySFXOnManagedAudioSource(fireSFX, this);
            }
        }
    }
}
