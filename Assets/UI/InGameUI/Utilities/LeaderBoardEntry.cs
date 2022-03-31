using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardEntry : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI playerNameTMP;
    [SerializeField] TMPro.TextMeshProUGUI scoreTMP;
    [SerializeField] TMPro.TextMeshProUGUI timestampTMP;

    public void Initilaize(string name, string score, string timestamp)
    {
        playerNameTMP.text = name;
        scoreTMP.text = score;
        timestampTMP.text = timestamp;
    }
}
