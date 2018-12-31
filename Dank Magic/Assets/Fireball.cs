using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {

    public GameObject fireExplosion;
    private Transform target;
    private Rigidbody rb;

    private void OnEnable()
    {
        print("Ball created");
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Player").transform;

        if(target && rb)
        {
            Vector3 direction = (target.position  + Vector3.up * 1.2f- transform.position).normalized;
            rb.velocity = direction * 12;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        Instantiate(fireExplosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
