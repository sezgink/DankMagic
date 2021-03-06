﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterControl : MonoBehaviour {

    public GameObject gameOverMenu;
    public GameObject eventSystem;

    //public RawImage skillObject;
    public GameObject soulPanel;

	public float translateConstant;
	public float rotateConstant;
	public GameObject judyCam;
	Animator animator;
	public string floorTag;
	public float jumpingConstant;

    public Rigidbody spiritRigidbody;
    public GameObject fireball;

    public int health;
    public int spirits;
    public HUDManager hudManager;

    bool ableToQ = true;


	bool isJumping;
	bool onFloor;
	bool isRunning;
	Rigidbody rb;
	bool isAttacking = false;
	bool isSpellcasting = false;
	public bool isStunned = false;
    bool dealtDamage = false;
    public GameObject hitEffect;
    int proIndex;
    public bool ruhtasiAlindi;
    bool willCombo = false;
    public bool mortal = true;
    bool alive = true;
    float deadCounter = 0f;
    public float deadTime = 4f;
    bool fireUsed = false;
    int c5 = 0;
    float qCounter= 0f;

    public Collider swordCollider;
    // Use this for initialization

    private void Awake()
    {
        judyCam = GameObject.Find("JudyCam");
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Start () {

        health = 100;
        spirits = 2;

        hudManager.UpdateHealth();
        hudManager.UpdateSpiritBar();
        swordCollider.enabled = false;
        dealtDamage = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (alive)
        {
            if (!isStunned)
            {
                var x = Input.GetAxis("Horizontal");
                var y = Input.GetAxis("Vertical");

                //print ( judyCam.transform.eulerAngles.y);
                Vector3 lo = new Vector3(transform.eulerAngles.x, judyCam.transform.eulerAngles.y, transform.eulerAngles.z);
                Vector3 rotat = Vector3.Lerp(transform.localRotation.eulerAngles, lo, 1f);
                transform.localRotation = Quaternion.Euler(rotat);
                if (!isAttacking && !isSpellcasting)
                {
                    transform.Translate(Vector3.forward * y * translateConstant * Time.deltaTime / 1.2f);
                    transform.Translate(Vector3.right * x * translateConstant * Time.deltaTime / 1.2f);
                }
                //transform.Rotate (Vector3.up*x*rotateConstant*Time.deltaTime);

                if (y * y > 0.5)
                {
                    animator.SetBool("isRunning", true);
                    animator.SetBool("isRightMove", false);
                }
                else if (x * x > 0.5)
                {
                    animator.SetBool("isRunning", false);
                    animator.SetBool("isRightMove", true);

                }
                else
                {
                    animator.SetBool("isRunning", false);
                    animator.SetBool("isRightMove", false);
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    jump();
                }
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (isAttacking)
                        willCombo = true;
                    else
                        attack();
                }
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    spellcast(0);
                }
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    spellcast(1);
                }

                if (Input.GetKeyDown(KeyCode.R))
                {
                    gameObject.SetActive(false);
                    gameObject.SetActive(true);
                    Start();
                }
                if (c5 > 30) {
                    if (ruhtasiAlindi)
                    {
                        hudManager.FireRefresh();
                        c5 = 0;
                    }
                }
                if (ableToQ == false)
                {
                    if (qCounter > 2.5f)
                    {
                        qCounter = 0;

                        activateQ();
                    }
                }
                c5++;
                qCounter += Time.deltaTime;
            }
        }
        else {
            deadCounter += Time.deltaTime;
            if (deadCounter > deadTime)
                GameOver();
        }
	}
    void activateQ() {
        ableToQ = true;
       // skillObject.color = Color.cyan;
        soulPanel.SetActive(true);
        //print("Activated Q");

    }
    void attack() {
		if (!isAttacking) {
			if (onFloor) {
				isAttacking = true;
				animator.SetBool ("isAttacking", true);
                swordCollider.enabled = true;
                dealtDamage = false;
			}
		}
	}
	void spellcast(int i) {
        if (ruhtasiAlindi)
        {
            if (!isSpellcasting && !spiritRigidbody.gameObject.activeInHierarchy)
            {
                if (i == 0 && !spiritRigidbody.gameObject.activeInHierarchy && ableToQ)
                {
                    if (onFloor)
                    {
                        proIndex = 0;
                        isSpellcasting = true;

                        animator.SetBool("isSpellcasting", true);
                        qCounter = 0;
                        ableToQ = false;
                        soulPanel.SetActive(false);
                    }
                }
                else if (i == 1 && spirits > 0)
                {
                    if (onFloor)
                    {
                        spirits--;
                        proIndex = 1;
                        isSpellcasting = true;
                        animator.SetBool("isSpellcasting", true);
                        hudManager.FireUsed();
                    }
                }
                

            }
        }
	}
	void jump() {
		if (onFloor) {
			rb.velocity += Vector3.up * jumpingConstant;
            animator.SetBool("isJumping", true);
			//print ("jumping");
		}
			
	}
    void die()
    {
        if (mortal)
        {
            animator.SetTrigger("isDead");
            alive = false;
            //GetComponent<CapsuleCollider>().enabled = false;
            
        }
    }
	void OnCollisionEnter(Collision col) {
		//print (col.gameObject.tag);
		if (col.gameObject.tag == floorTag)
        {
            animator.SetBool("isJumping", false);
            onFloor = true;
		}
	}
	void OnCollisionExit(Collision col) {
		//print (col.gameObject.tag);
		if (col.gameObject.tag == floorTag) {
			onFloor = false;
		}
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 11 && swordCollider.enabled && !dealtDamage)
        {
            dealtDamage = true;
            if (other.tag == "Rider")
            {
                other.GetComponent<RiderControl>().takeDamage(30);
                Instantiate(hitEffect, swordCollider.transform.position, Quaternion.identity);
            }
            if (other.tag == "Captain")
            {
                other.GetComponent<RiderCaptainControl>().takeDamage(30);
                Instantiate(hitEffect, swordCollider.transform.position, Quaternion.identity);
            }
        }
        if (other.gameObject.tag == "Fire")
        {
            Heal(-40);
        }
    }
    public void endAttack() {
		//print ("end attack");
		isAttacking = false;
		animator.SetBool ("isAttacking", false);
        swordCollider.enabled = false;

    }
    public void willAttackSustain() {
        if (!willCombo)
            endAttack();
        dealtDamage = false;
        willCombo = false;

    }
	public void endSpellcast() {
		//print ("end attack");
		isSpellcasting = false;
		animator.SetBool ("isSpellcasting", false);

	}
	public void takeDamage() {
		isStunned = true;
		animator.SetTrigger ("isDamaging");
	}
	public void endStun() {
        //print("End of stun");
		isStunned = false;
	}

    public void createProjectile()
    {
        if (proIndex == 0)
        {
            spiritRigidbody.transform.position = transform.position + transform.forward * 1f + Vector3.up * 1.2f;
            spiritRigidbody.gameObject.SetActive(true);
            spiritRigidbody.velocity = transform.forward * 20;
        }
        else
        {
            GameObject go = Instantiate(fireball, transform.position + transform.forward * 1.5f + Vector3.up * 1.3f, Quaternion.identity);
            Rigidbody fireRB = go.GetComponent<Rigidbody>();
            fireRB.velocity = transform.forward * 15;
            spirits--;
            hudManager.UpdateSpiritBar();
        }
    }

    public void Heal(int amount)
    {
        health += amount;
        if(amount < 0)
        {
            Instantiate(hitEffect, transform.position + Vector3.up + transform.forward * .5f, Quaternion.identity);
            var ran = Random.Range(0, 20);
            if (ran > 12)
            {
                takeDamage();
            }
        }
        if (health > 100)
            health = 100;

        if(health <= 0)
        {
            health = 0;
            die();
        }

        hudManager.UpdateHealth();
    }

    public void AddSoul()
    {
        spirits++;
        if (spirits > 10)
            spirits = 10;

        hudManager.UpdateSpiritBar();
    }
    public void GameOver() {
        //gameOverMenu.SetActive(true);
        eventSystem.GetComponent<EventManager>().pretargetRetry();
        Time.timeScale = 0;
    }
}
