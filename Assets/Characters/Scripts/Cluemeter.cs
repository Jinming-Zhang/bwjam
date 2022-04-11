using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WolfUISystem;
using WolfUISystem.Presets;

public class Cluemeter : MonoBehaviour
{
    [SerializeField]
    int initial = 0;
    [SerializeField]
    int max = 10;
    int current;
    public int Value
    {
        get => current;
        set
        {
            if (value <= MaxValue)
            {
                current = value;
                if (UIManager.Instance)
                {
                    HudScreen hud = UIManager.Instance.GetScreenComponent<HudScreen>();
                    if (hud)
                    {
                        hud.UpdateClumeter(Value, MaxValue);
                    }
                }
            }
        }
    }
    public int MaxValue { get => max; }

    // Start is called before the first frame update
    void Start()
    {
        current = initial;
    }
}
