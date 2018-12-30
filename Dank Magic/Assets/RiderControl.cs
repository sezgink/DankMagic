using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class RiderControl : MonoBehaviour {
	Transform Player;
	public float meleeRange;
	public float seeRange;
	NavMeshAgent nma;
	Animator animator;
	RawImage rawImage;

    public Collider swordCollider;

	bool alive = true;
	bool attacking = false;
    bool dealtDmg = false;

	int c1;
	int health;
	public int meleeAttackPeriod;
	// Use this for initialization
	void Start () {
		Player = GameObject.FindGameObjectWithTag ("Player").transform;
		nma = GetComponent<NavMeshAgent> ();
		animator = GetComponent<Animator> ();
		health = 10;
        RawImage[] images = GetComponentsInChildren<RawImage>();
        foreach (var image in images)
        {
            if (image.gameObject.name == "HealthForeground")
                rawImage = image;
        }
        swordCollider.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (alive) {
			if ((Player.position - transform.position).magnitude <= meleeRange) {
				meleeAttack ();

				animator.SetBool ("isRunning", false);
                nma.destination = Player.position;
                nma.speed = 1;

			} else if ((Player.position - transform.position).magnitude <= seeRange) {
				nma.destination = Player.position;
				nma.speed = 5;
				animator.SetBool ("isAttacking", false);
				animator.SetBool ("isRunning", true);
			} else {
				animator.SetBool ("isRunning", false);
				animator.SetBool ("isAttacking", false);
			}
			c1++;
		}
	}
	void meleeAttack() {
		if (c1 > meleeAttackPeriod) {
			animator.SetBool ("isAttacking", true);
			attacking = true;
			c1 = 0;
            nma.speed = 0;
            swordCollider.enabled = true;
            dealtDmg = false;
        }
	}
	public void stopAttack()
    {
        swordCollider.enabled = false;
        animator.SetBool ("isAttacking",false);
		attacking = false;
        nma.speed = 3.5f;
    }
	public void takeDamage(int amount) {
        dealtDmg = false;
		stopAttack ();
		animator.SetTrigger ("Impact");
		health -= amount;
		rawImage.rectTransform.localScale = new Vector3(health / 10f,1,1) ;
		print (health);
        nma.speed = 0;
        swordCollider.enabled = false;
        if (health < 1)
        {
            rawImage.gameObject.SetActive(false);
            death();
        }
		
	}
    public void exitStagger()
    {
        nma.speed = 3.5f;
    }
	void death() {
		animator.SetTrigger ("isDeath");
		alive = false;
    }
    public void SetDealtDamage()
    {
        dealtDmg = false;
    }
    public void destroyObject()
    {
        gameObject.SetActive(false);
    }
	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Player" && swordCollider.enabled && !dealtDmg) {
			//print (col.gameObject.name);
            CharacterControl cc = col.GetComponent<CharacterControl>();
            cc.Heal(-1);
            dealtDmg = true;
		}

        if(col.gameObject.layer == 9)
        {
            takeDamage(1);
        }

        if(col.gameObject.tag == "Fire")
        {
            takeDamage(7);
        }
	}
}
