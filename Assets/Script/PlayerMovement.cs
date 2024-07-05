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
    public float moveSpd = 25f;


    private void FixedUpdate()
    {
        float moveHor = Input.GetAxis("Horizontal");
        float moveVert = Input.GetAxis("Vertical");

        rigidbody.velocity = new Vector3(moveHor * moveSpd, rigidbody.velocity.y, moveVert * moveSpd);
    }
}
