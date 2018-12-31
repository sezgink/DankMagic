using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class RiderCaptainControl : MonoBehaviour {

	Transform Player;
    //public List<GameObject> createdFireballs;
    public List<GameObject> createdFireballs;
	public float longRange;
	public float meleeRange;
	public float seeRange;
	NavMeshAgent nma;
	Animator animator;
	RawImage rawImage;
    public GameObject fireball;
    public GameObject rotatingFireball;
    bool deployed = false;
    GameObject createdFireball;
    float greatCounter = 0f;
	bool alive = true;
	bool attacking = false;
    public float greatPeriod;
    CleaverFireball cf;


    float c1;
    float c2;
	int health;
	int souls;


	public int rangeAttackPeriod;
	public int meleeAttackPeriod;
	// Use this for initialization
	void Start () {
		Player = GameObject.FindGameObjectWithTag ("Player").transform;
		nma = GetComponent<NavMeshAgent> ();
		animator = GetComponent<Animator> ();
		health = 150;
		RawImage[] images = GetComponentsInChildren<RawImage>();
		foreach(var image in images) {
			if (image.gameObject.name == "HealthForeground")
				rawImage = image;
		}
        createdFireballs = new List<GameObject>();
		//rawImage = GameObject.Find ("HealthForeground").GetComponent<RawImage>();
	}

    public void Fireball()
    {
        Instantiate(fireball, transform.position + transform.forward * 1.5f + Vector3.up * 2f, Quaternion.identity);
    }
    
    public void DeployRotatingBall() {
        //print("Deploy rotating ball");
        /*
        if (deployed)
        {
            if (createdFireballs.Count > 0)
            {
                GameObject fball = createdFireballs[0];
                print(GameObject.ReferenceEquals(fball,createdFireball));
                
                
                fball.GetComponent<CleaverFireball>().ThrowFireball();
                //createdFireballs.Remove(fball);
            }
            
            
        }
        else
        {
            
            var fireballI = (GameObject)Instantiate(rotatingFireball, transform.position + transform.forward * 1.5f + Vector3.up * 2f, Quaternion.identity);

            createdFireballs.Add(fireball);
            createdFireball = fireballI;
            
        }
        */
        Instantiate(rotatingFireball, transform.position + transform.forward * 1.5f + Vector3.up * 2f, Quaternion.identity);

    }

    // Update is called once per frame
    void Update () {
		if (alive) {
            /*
			if ((Player.position - transform.position).magnitude <= meleeRange) {
				nma.destination = Player.position;
				meleeAttack ();

                animator.SetBool("isGreatSpellcasting", false);
                animator.SetBool ("isRunning", false);
				nma.speed = 1;

			} else if ((Player.position - transform.position).magnitude <= longRange) {
				nma.destination = Player.position;
				rangeAttack ();

                animator.SetBool("isSpellcasting", false);
				animator.SetBool ("isRunning", false);
				nma.speed = 1;
			}
			else if ((Player.position - transform.position).magnitude <= seeRange) {
				nma.destination = Player.position;
				nma.speed = 5;
				animator.SetBool ("isAttacking", false);
				animator.SetBool ("isRunning", true);
			} else {
				animator.SetBool ("isGreatSpellcasting", false);
				animator.SetBool ("isSpellcasting", false);
				animator.SetBool ("isRunning", false);
				nma.speed = 0;
			}
            */

            if ((Player.position - transform.position).magnitude <= seeRange)
            {
                if (greatCounter > greatPeriod) {
                    rangeAttack();
                }
                nma.destination = Player.position;
                if (!attacking)
                    nma.speed = 3;
                else
                    nma.speed = 0;
                animator.SetBool("isAttacking", false);
                animator.SetBool("isRunning", true);
                if ((Player.position - transform.position).magnitude <= meleeRange) {
                    meleeAttack();
                }
            }
            greatCounter += Time.deltaTime;
            c1 += Time.deltaTime;
			c2 += Time.deltaTime;
		}
	}
	void meleeAttack() {
		if (c1 > meleeAttackPeriod) {
			animator.SetBool ("isSpellcasting", true);
			attacking = true;
			c1 = 0;
		}
	}
	void rangeAttack() {
		if (c2 > rangeAttackPeriod) {
			animator.SetBool ("isGreatSpellcasting", true);
			attacking = true;
			c2 = 0;
		}
	}
	public void stopAttack() {
		//animator.SetBool ("isAttacking",false);
		attacking = false;

	}
    public void endSpellcast()
    {
        //print ("end attack");
        animator.SetBool("isSpellcasting", false);
        stopAttack();

    }
    public void endGreatSpellcast()
    {
        //print ("end attack");
        animator.SetBool("isGreatSpellcasting", false);
        stopAttack();

       // deployed = !deployed;

    }
    public void takeDamage(int amount) {
		stopAttack ();
		animator.SetTrigger ("Hit");
		health -= amount;
        if (health < 0)
            health = 0;
		rawImage.rectTransform.localScale = new Vector3(health / 150f,1,1) ;
		//print (health);
		if(health<1)
			death();

	}

	void death() {
		animator.SetTrigger ("Death");
		alive = false;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fire")
        {
           
                if (other.GetComponent<CleaverFireball>().owner != 2)
                    takeDamage(40);
            

            
        }
    }
    //void OnCollisionEnter(Collision col) {
    //	Collider c = col.contacts [0].thisCollider;
    //	foreach(var cs in col.contacts) {
    //		if (col.gameObject.tag == "Player") {
    //			print (cs.thisCollider.gameObject.name);
    //			if (cs.thisCollider.gameObject.tag == "Weapon") {
    //				//col.gameObject.GetComponent<CharacterController>().damage(value);

    //			}
    //		}
    //	}
    //	print(c.gameObject.name);
    //}
}
