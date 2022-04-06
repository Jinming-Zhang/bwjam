using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class LevelManager : MonoBehaviour, ILevelManager
{
	IEnumerable<IPausableComponent> pausableComponents;
	bool initialized;
	private void Start()
	{
		Initialize();
	}
	public void Initialize()
	{
		if (!initialized)
		{
            initialized = true;
			pausableComponents = FindObjectsOfType<MonoBehaviour>().OfType<IPausableComponent>();
		}
	}
	public void PauseLevel()
	{
		if (pausableComponents != null)
		{
			foreach (IPausableComponent go in pausableComponents)
			{
				go.Pause();
			}
		}
	}

	public void ResumeLevel()
	{
		if (pausableComponents != null)
		{
			foreach (IPausableComponent go in pausableComponents)
			{
				go.Resume();
			}
		}
	}
}
