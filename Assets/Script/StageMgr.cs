using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMgr : MonoBehaviour
{
    public static StageMgr instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<StageMgr>();
                if (_instance == null)
                {
                    var instContianer = new GameObject("StageMgr");
                    _instance = instContianer.AddComponent<StageMgr>();
                }
            }

            return _instance;
        }
    }

    private static StageMgr _instance;

    public GameObject player;
    [System.Serializable]
    public class RoomArray
    {
        public List<RoomCondition> listRooms = new List<RoomCondition>();
    }

    public enum RoomType
    {
        Start,
        Normal,
        Hard,
        Angel,
        Boss,
    }

    public RoomArray[] aryRooms;
    private int currentStage = 0;
    private const int lastStage = 20;
    private static Vector3 tempStartPos = new Vector3(0,0,-3.1f);

    public void Start()
    {
        // 시작 방 위치로 이동
        player.transform.position = CalcStartPos(aryRooms[(int)RoomType.Start].listRooms[0]);
    }

    private Vector3 CalcStartPos(RoomCondition room)
    {
        return room.transform.parent.transform.TransformPoint(tempStartPos);
    }

    public void NextStage()
    {
        currentStage++;

        if (currentStage > lastStage)
            return;

        // 일단 일반 방만 돌게 만듬

        RoomCondition randomRoom = null;
        
        // 일반 방
        if(currentStage % 5 != 0)
        {
            int randomIndex = Random.Range(0, aryRooms[(int)RoomType.Normal].listRooms.Count);
            randomRoom = aryRooms[(int)RoomType.Normal].listRooms[randomIndex];
            aryRooms[(int)RoomType.Normal].listRooms.RemoveAt(randomIndex);
        }
        else
        {
            // 보스방 또는 엔젤방

        }

        if (randomRoom == null)
            return;

        player.transform.position = CalcStartPos(randomRoom);
    }
}
