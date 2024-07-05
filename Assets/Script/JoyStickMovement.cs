using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStickMovement : MonoBehaviour
{
    public static JoyStickMovement Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindAnyObjectByType<JoyStickMovement>();
                if(_instance== null)
                {
                    var instanceContainer = new GameObject("JoyStickMovement");
                    _instance = instanceContainer.AddComponent<JoyStickMovement>();
                }
            }

            return _instance;
        }
    }

    private static JoyStickMovement _instance;

    [SerializeField] private Transform smallStick;
    [SerializeField] private Transform bgStick;
    Vector3 stickFirstPosition;
    public Vector3 joyVec;
    float stickRadius;
    public bool isMoving = false;


    private void Start()
    {
        stickRadius = (bgStick as RectTransform).sizeDelta.y * 0.5f;
        ResetUI(bgStick.position);
    }

    private void ResetUI(Vector2 pos)
    {
        stickFirstPosition = pos;
        smallStick.position = bgStick.position = stickFirstPosition;
    }

    public void PointDown()
    {
        ResetUI(Input.mousePosition);
        PlayerMovement.Instance.PlayAnim_Walk();
        isMoving = true;
    }

    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector3 dragPos = pointerEventData.position;
        joyVec = (dragPos - stickFirstPosition).normalized;

        float stickDistance = Vector3.Distance(dragPos, stickFirstPosition);
        smallStick.position = stickFirstPosition + joyVec * Mathf.Min(stickDistance, stickRadius);

        Debug.Log("drag : " + stickDistance);
    }

    public void Drop()
    {
        joyVec = Vector3.zero;
        ResetUI(stickFirstPosition);
        PlayerMovement.Instance.PlayAnim_Idle();
        isMoving = false;
    }
}
