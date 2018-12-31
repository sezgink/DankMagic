using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaverFireball : MonoBehaviour {
    public GameObject fireExplosion;
    private Transform target;
    private Rigidbody rb;
    Vector3 direction;
    float pastTime = 0;
    public bool isThrowed = false;

    public float passiveSpeed;
    public float activeSpeed;
    public float speed;
    public float turnHeight;
    public float turnRadious;
    public float trigCo = 1f;
    public int owner = 2;
    float activationTime;
    
    private void OnEnable()
    {
        print("Ball created");

        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        if (target && rb) ;
        else if(rb) {
            rb.velocity = Vector3.forward * 12;
        }
        activationTime = Random.Range(5f,30f);


    }
    private void Update()
    {
        pastTime += Time.deltaTime;
        if (target && rb)
        {
            if (isThrowed)
            {


                direction = (target.position + Vector3.up * 1.2f - transform.position).normalized;
                rb.velocity = direction * activeSpeed;

            }
            else
            {
                direction = (target.position + Vector3.up * turnHeight - transform.position + turnRadious * Mathf.Cos(pastTime * trigCo) * Vector3.right + turnRadious * Mathf.Sin(pastTime * trigCo) * Vector3.forward).normalized;
                rb.velocity = direction * passiveSpeed;
                pastTime += Time.deltaTime;
            }
        }
        if (pastTime > activationTime)
            ThrowFireball();
        
    }
    public void ThrowFireball() {
        isThrowed = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (isThrowed || other.gameObject.tag=="Player")
        {
            gameObject.SetActive(false);
            Instantiate(fireExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        } 
    }
}
