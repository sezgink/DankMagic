using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball1 : MonoBehaviour {

    public GameObject fireExplosion;
    
    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        Instantiate(fireExplosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
