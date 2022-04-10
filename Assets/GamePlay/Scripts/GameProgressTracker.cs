using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgressTracker : MonoBehaviour
{
    [System.Serializable]
    public struct RoomSceneMap
    {
        public RoomType roomType;
        public List<string> roomSceneNames;
    }
    public enum RoomType
    {
        StartingEmptyRoom,
        FifthRoom,
        CommonEnemyRoom,
        EmptyRoomBeforeBoss,
        BossRoom
    }

    [SerializeField]
    List<string> commonSceneLevelName;
    [SerializeField]
    string bossSceneLevelName;
    public List<RoomSceneMap> gameOrder;
    int currentRoomInd;

    public void ResetStatus()
    {
        currentRoomInd = 0;
    }
    public string GetNextRoom(int currentClue, out bool fightBoss)
    {
        fightBoss = false;
        currentRoomInd = Mathf.Min(gameOrder.Count, currentRoomInd + 1);
        RoomSceneMap current = gameOrder[currentRoomInd];
        if (current.roomType != RoomType.BossRoom)
        {
            int random = Random.Range(0, current.roomSceneNames.Count);
            return current.roomSceneNames[random];
        }
        else
        {
            fightBoss = Random.Range(0, 11) < currentClue;
            if (fightBoss)
            {
                return current.roomSceneNames[0];
            }
            else
            {
                currentRoomInd = 0;
                return current.roomSceneNames[0];
            }
        }
    }
}
