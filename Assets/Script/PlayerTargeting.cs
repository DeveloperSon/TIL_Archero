using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerTargeting : MonoBehaviour
{
    public static PlayerTargeting instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<PlayerTargeting>();
                if(_instance == null)
                {
                    var instContianer = new GameObject("PlayerTargeting");
                    _instance = instContianer.AddComponent<PlayerTargeting>();
                }
            }

            return _instance;
        }
    }

    private static PlayerTargeting _instance;

    float currentDist = 0;
    float targetDist = 100;
    float closeDist = 0;
    public LayerMask layerMask;

    GameObject closeTarget;
    private List<GameObject> listSearchedMonsters = new();
    [SerializeField] private GameObject goBullet;
    [SerializeField] private Transform trBulletContainer;



    public void CopyMonsters(ref List<GameObject> listMonsters)
    {
        listSearchedMonsters = new List<GameObject>(listMonsters);
    }

    public void ClearMonsters()
    {
        listSearchedMonsters.Clear();
    }

    private void Update()
    {
        if (SearchCloseTarget())
            AtkToTarget();
    }

    private void OnDrawGizmos()
    {
        if (listSearchedMonsters.Count == 0)
            return;

        Vector3 playerPos = transform.position;
        for (int i = 0; i < listSearchedMonsters.Count; i++)
        {
            Transform mon = listSearchedMonsters[i].transform;
            currentDist = Vector3.Distance(playerPos, mon.position);

            RaycastHit hit;
            bool isHit = Physics.Raycast(playerPos, mon.position - playerPos, out hit, 20f, layerMask);

            if (isHit && hit.transform.CompareTag(RoomCondition.TAG_MONTSER))
            {
                Gizmos.color = Color.green;
            }
            else
                Gizmos.color = Color.red;

            Gizmos.DrawRay(playerPos, mon.transform.position - playerPos);
        }
    }

    void Attack()
    {
        Instantiate(goBullet, trBulletContainer.position, transform.rotation);
    }

    void AtkToTarget()
    {
        if (closeTarget !=  null && !JoyStickMovement.Instance.isMoving)
        {
            transform.LookAt(closeTarget.transform.position);
            PlayerMovement.Instance.PlayAnim_Atk();
        }
        else if(JoyStickMovement.Instance.isMoving)
        {
            PlayerMovement.Instance.PlayAnim_Walk();
        }
    }

    bool SearchCloseTarget()
    {
        closeTarget = null;
        if (listSearchedMonsters.Count == 0)
            return false;

        closeTarget = listSearchedMonsters[0];
        Vector3 playerPos = transform.position;
        
        currentDist = 0;
        targetDist = Vector3.Distance(playerPos, closeTarget.transform.position);

        for (int i = 1; i < listSearchedMonsters.Count; i++)
        {
            Transform mon = listSearchedMonsters[i].transform;
            currentDist = Vector3.Distance(playerPos, mon.position);

            RaycastHit hit;
            bool isHit = Physics.Raycast(playerPos, mon.position - playerPos, out hit, 20f, layerMask);

            if(isHit && hit.transform.CompareTag(RoomCondition.TAG_MONTSER))
            {
                if(targetDist > currentDist)
                {
                    closeTarget = mon.gameObject;
                    targetDist = currentDist;
                }

            }
        }

        Debug.Log("close target : " + closeTarget.name);
        return true;
    }

}
