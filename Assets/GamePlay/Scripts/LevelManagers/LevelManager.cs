using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour, ILevelManager
{






    IEnumerable<IPausableComponent> pausableComponents;
    bool initialized;

    [SerializeField]
    bool isRoomBeforeBoss;
    [SerializeField]
    List<Enemy> sceneEnemies;
    [SerializeField]
    LoadLevelGate levelGate;
    int totalEnemies;
    private void Start()
    {
        if (isRoomBeforeBoss)
        {
            levelGate.gameObject.SetActive(true);
            return;
        }
        levelGate.gameObject.SetActive(false);
        if (sceneEnemies == null || sceneEnemies.Count == 0)
        {
            sceneEnemies = FindObjectsOfType<Enemy>().ToList();
            totalEnemies = sceneEnemies.Count;
            GameCore.GameManager.Instance.currentLevelManager = this;
        }
    }

    public void OnEnemyDead()
    {
        totalEnemies--;
        if (totalEnemies == 0)
        {
            levelGate.gameObject.SetActive(true);
        }
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
