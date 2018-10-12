﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Rigidbody2D))]
public class Attacker : MonoBehaviour
{
    // configuration parameters, consider SO
    [Range(1f, 10f)]
    [Tooltip("Average number of seconds between appearances")]
    [SerializeField] float seenEverySeconds;

    // private instance variables for state
    float currentSpeed;

    // cached references for readability
    GameObject currentTarget;
    LevelController levelController;

    void Start()
    {
        levelController = FindObjectOfType<LevelController>();
        levelController.AttackerSpawned();
    }

    void OnDestroy()
    {
        levelController.AttackerKilled();
    }

    // Update is called once per frame
    void Update ()
    {
		transform.Translate (Vector3.left * currentSpeed * Time.deltaTime);
		if (!currentTarget)
        {
            GetComponent<Animator>().SetBool("isAttacking", false);
		}
	}
	
    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        GameObject otherObject = otherCollider.gameObject;

        if (otherObject.GetComponent<Defender>())
        {
            Attack(otherObject);
        }
    }

	public void SetSpeed(float speed)
    {
		currentSpeed = speed;
	}
	
	// Called from the animator at time of actual blow
	public void  StrikeCurrentTarget(float damage)
    {
		if (currentTarget)
        {
			Health health = currentTarget.GetComponent<Health>();
            if (currentTarget.GetComponent<Health>())
            {
				health.DealDamage (damage);
			}
		}
	}
	
	public void Attack(GameObject target)
    {
        GetComponent<Animator>().SetBool("isAttacking", true);
        currentTarget = target;
	}

    public float GetSpawnsPerSecond()
    {
        return seenEverySeconds;
    }
}
