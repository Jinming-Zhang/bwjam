using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    [SerializeField]
    GameObject graphics;
    [SerializeField]
    bool isDefaultFacingRight = true;
    [SerializeField]
    float deadZoneRange = .3f;
    GamePlay.PlayerController player => GameCore.GameManager.Instance.Player;
    private void Update()
    {
        if (player)
        {
            float x = player.transform.position.x - transform.position.x;
            if (Mathf.Abs(x) > deadZoneRange)
            {
                // should facing left
                if (x < 0)
                {
                    if (isDefaultFacingRight)
                    {
                        graphics.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                    }
                    else
                    {
                        graphics.transform.rotation = Quaternion.Euler(Vector3.zero);
                    }
                }
                // should facing right
                else
                {
                    if (isDefaultFacingRight)
                    {
                        graphics.transform.rotation = Quaternion.Euler(Vector3.zero);
                    }
                    else
                    {
                        graphics.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                    }
                }
            }
        }

    }
}
