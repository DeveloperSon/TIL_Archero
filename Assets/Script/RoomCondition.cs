using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCondition : MonoBehaviour
{

    public const string TAG_PLAYER = "Player";
    public const string TAG_MONTSER = "Monster";

    private List<GameObject> listMonsters = new List<GameObject>();
    private bool isPlayerInRoom = false;
        
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TAG_PLAYER))
        {
            isPlayerInRoom = true;
            PlayerTargeting.instance.CopyMonsters(ref listMonsters);
        }
        else if (other.CompareTag(TAG_MONTSER))
        {
            listMonsters.Add(other.gameObject);
            Debug.Log("Found Monster : " + other.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(TAG_PLAYER))
        {
            isPlayerInRoom = false;
            PlayerTargeting.instance.ClearMonsters();

        }
        else if (other.CompareTag(TAG_MONTSER))
        {
            listMonsters.Remove(other.gameObject);
        }
    }
}
