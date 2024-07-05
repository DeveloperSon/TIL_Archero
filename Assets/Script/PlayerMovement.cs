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


    public float moveSpd = 25f;


    private void FixedUpdate()
    {
        float moveHor = Input.GetAxis("Horizontal");
        float moveVert = Input.GetAxis("Vertical");

        rigidbody.velocity = new Vector3(moveHor * moveSpd, rigidbody.velocity.y, moveVert * moveSpd);
    }
}

