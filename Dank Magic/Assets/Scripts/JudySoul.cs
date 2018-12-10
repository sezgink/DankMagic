using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudySoul : MonoBehaviour {

    public Transform judyTrasform;

    public GameObject soulSeizeExplosion;
    public GameObject soulSeizeReturn;

    public ParticleSystem particleSystem;
    ParticleSystem.MainModule mainModule;

    public Color startColor;
    public Color soulSeizedColor;

    public bool soulSeized;
    public CharacterControl judyControl;

    Rigidbody rb;
    Vector3 direction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        mainModule = particleSystem.main;
    }

    private void Update()
    {
        direction = (judyTrasform.position - transform.position + Vector3.up).normalized;
        rb.velocity += direction * Time.deltaTime * 15;
        if (transform.position.y < .5f)
            rb.velocity += Vector3.up * Time.deltaTime * 5f;
    }

    private void OnEnable()
    {
        soulSeized = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            gameObject.SetActive(false);
            Instantiate(soulSeizeReturn, transform.position, Quaternion.identity);
            mainModule.startColor = startColor;

            if (soulSeized)
            {
                judyControl.AddSoul();
                judyControl.Heal(1);
            }

            return;
        }

        if(other.gameObject.layer == 11)
        {
            if (soulSeized)
                return;
            soulSeized = true;
            Instantiate(soulSeizeExplosion, transform.position, Quaternion.AngleAxis(-90f, Vector3.right));
            mainModule.startColor = soulSeizedColor;
            rb.velocity = Vector3.zero;
            return;
        }
    }
}
