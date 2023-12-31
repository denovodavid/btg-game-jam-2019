﻿using UnityEngine;
using System.Collections;

public class PlayerCharacter : MonoBehaviour
{
    public float speed = 0.1f;
    public float bucketSpeed = 0.03f;
    public Boat boat;
    public GameObject bucket;
    public GameObject reefer;
    public ParticleSystem bucketParticles;
    Bounds boatBounds;
    bool holdingBucket = false;
    Animator animator;
    bool dead = false;
    bool bucketReady = true;
    public AudioClip bucketSplash;
    public AudioClip bucketSFX;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        boatBounds = boat.GetComponent<SpriteRenderer>().bounds;
        boatBounds = new Bounds(boatBounds.center, boatBounds.size);
        boatBounds.Expand(-1.5f);
    }

    private void Update()
    {
        if (dead) return;
        var localSpeed = holdingBucket ? bucketSpeed : speed;
        // move left
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            var newPos = transform.position + Vector3.left * localSpeed * Time.deltaTime * 70;
            var closestPoint = boatBounds.ClosestPoint(newPos);
            if (Mathf.Approximately(closestPoint.x, newPos.x))
            {
                transform.position = newPos;
            }
            GetComponent<SpriteRenderer>().flipX = true;
        }

        // move right
        if (Input.GetKey(KeyCode.RightArrow))
        {
            var newPos = transform.position + Vector3.right * localSpeed * Time.deltaTime * 70;
            var closestPoint = boatBounds.ClosestPoint(newPos);
            if (Mathf.Approximately(closestPoint.x, newPos.x))
            {
                transform.position = newPos;
            }
            GetComponent<SpriteRenderer>().flipX = false;
        }

        // pick up
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (holdingBucket)
            {
                Debug.Log("Drop Bucket");
                bucket.transform.SetParent(null);
                holdingBucket = false;
                GetComponent<AudioSource>().clip = bucketSFX;
                GetComponent<AudioSource>().Play();
            }
            else if (GetComponent<BoxCollider2D>().OverlapPoint(bucket.transform.position))
            {
                Debug.Log("Pick up Bucket");
                bucket.transform.SetParent(transform);
                holdingBucket = true;
                GetComponent<AudioSource>().clip = bucketSFX;
                GetComponent<AudioSource>().Play();
            }
            else if (GetComponent<BoxCollider2D>().OverlapPoint(reefer.transform.position))
            {
                Debug.Log("Reef Sails");
                boat.ToggleSails();
            }
        }

        if (holdingBucket)
        {
            StartCoroutine(UseBucket());
        }
    }

    public void Die()
    {
        dead = true;
        // if (holdingBucket)
        // {
        //     bucket.transform.SetParent(null);
        //     holdingBucket = false;
        // }
        animator.SetTrigger("Die");
    }

    IEnumerator UseBucket()
    {
        Debug.Log("Use Bucket");
        if (bucketReady && GameManager.instance.boatWaterLevel > 0)
        {
            GameManager.instance.boatWaterLevel -= 5f;
            bucketParticles.Play();
            GetComponent<AudioSource>().clip = bucketSplash;
            GetComponent<AudioSource>().Play();
            bucketReady = false;
            yield return new WaitForSeconds(1f);
            bucketReady = true;
        }
    }
}
