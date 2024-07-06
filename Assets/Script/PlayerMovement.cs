using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlayerMovement>();
                if (_instance == null)
                {
                    var instanceContainer = new GameObject("PlayerMovement");
                    _instance = instanceContainer.AddComponent<PlayerMovement>();
                }
            }

            return _instance;
        }
    }

    private static PlayerMovement _instance;

    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private Animator anim;

    const string ANIM_WALK = "WALK";
    const string ANIM_IDLE = "IDLE";
    const string ANIM_ATTACK = "ATTACK";
    const string ANIM_DMG = "DMG";
        
    public void PlayAnim_Walk()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(ANIM_WALK))
            return;
        
        anim.SetBool(ANIM_IDLE, false);
        anim.SetBool(ANIM_ATTACK, false);
        anim.SetBool(ANIM_WALK, true);
    }

    public void PlayAnim_Idle()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(ANIM_IDLE))
            return;

        anim.SetBool(ANIM_WALK, false);
        anim.SetBool(ANIM_ATTACK, false);
        anim.SetBool(ANIM_IDLE, true);
    }

    public void PlayAnim_Atk()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(ANIM_ATTACK))
            return;

        anim.SetBool(ANIM_WALK, false);
        anim.SetBool(ANIM_ATTACK, true);
        anim.SetBool(ANIM_IDLE, false);
    }


    public float moveSpd = 25f;


    private void FixedUpdate()
    {
        if(JoyStickMovement.Instance.joyVec.x != 0 || JoyStickMovement.Instance.joyVec.y != 0)
        {
            rigidbody.velocity = new Vector3(JoyStickMovement.Instance.joyVec.x, 0, JoyStickMovement.Instance.joyVec.y) * moveSpd;
            rigidbody.rotation = Quaternion.LookRotation(new Vector3(JoyStickMovement.Instance.joyVec.x, 0, JoyStickMovement.Instance.joyVec.y));
        }

    }
}

