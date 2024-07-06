using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.forward * 20f;
    }

    private void OnHit(Transform target)
    {
        // if 없앤 이유 : 충돌 판정 Layer를 설정해 감자는 벽과 몬스터에만 충돌 판정 되도록 설정함.
        Debug.Log("hit : " + target.name);
        rb.velocity = Vector3.zero;
        Destroy(gameObject, 0.2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        OnHit(other.transform);
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnHit(collision.transform);
    }
}
