using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GamePlay
{
	public abstract class MovementBehaviour : ScriptableObject
	{
		protected GameObject owner;
		public virtual void Initialize(GameObject owner, params object[] args)
		{
			this.owner = owner;
		}

		/// <summary>
		/// Should be called every frame 
		/// </summary>
		public abstract void UpdateMovement();
	}
}