using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementAnimation : MonoBehaviour {

    [SerializeField] GameObject particleObject;
    [SerializeField] float xSpeed = 100;

    float animationTime = 3f; // Seconds
    float elapsedTime = 0f;
    RectTransform rectTransform;
    int particleAmount;
    MyParticle[] particles;
    

	// Use this for initialization
	void Start ()
    {
        rectTransform = GetComponent<RectTransform>();
        InstantiateParticles();
	}
	
	// Update is called once per frame
	void Update ()
    {
        ParticleAnimation();
	}

    void ParticleAnimation()
    {
        if (elapsedTime < animationTime)
        {
            foreach (MyParticle particle in particles)
            {
                UpdateSingleParticle(particle);
            }
        }
        else
        {
            DestroyParticles();
        }
        elapsedTime += Time.deltaTime;
    }

    void UpdateSingleParticle(MyParticle particle)
    {
        RectTransform partTransform = particle.gameObject.GetComponent<RectTransform>();
        partTransform.position = new Vector2(partTransform.position.x, partTransform.position.y) + (particle.velocity * Time.deltaTime);
        Color color = particle.gameObject.GetComponent<Image>().color;
        color.a = 1 - (elapsedTime / animationTime);
        particle.gameObject.GetComponent<Image>().color = color;
    }

    void InstantiateParticles()
    {
        particleAmount = Random.Range(7, 13);
        particles = new MyParticle[particleAmount];
        for (int i = 0; i < particles.Length; i++)
        {
            particles[i] = new MyParticle(Instantiate(particleObject, rectTransform),
                new Vector2(Random.Range(50f, 100f) * xSpeed, 0f));
            particles[i].gameObject.transform.localPosition = new Vector3(0,0,0);
        }
    }

    void DestroyParticles()
    {
        for(int i = 0; i < particles.Length; i++)
        {
            if(particles[i].gameObject != null)
                Destroy(particles[i].gameObject);
        }
    }
}

class MyParticle
{
    public GameObject gameObject;
    public Vector2 velocity;

    public MyParticle(GameObject gameObject, Vector2 velocity)
    {
        this.gameObject = gameObject;
        this.velocity = velocity;
    }
}