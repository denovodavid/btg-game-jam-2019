using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public float speed = 0.05f;
    public SpriteRenderer boat;
    public GameObject bucket;
    public ParticleSystem bucketParticles;
    Bounds boatBounds;
    bool holdingBucket = false;

    private void Start()
    {
        boatBounds = new Bounds(boat.bounds.center, boat.bounds.size);
        boatBounds.Expand(-1.5f);
    }

    private void Update()
    {
        // move left
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            var newPos = transform.position + Vector3.left * speed;
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
            var newPos = transform.position + Vector3.right * speed;
            var closestPoint = boatBounds.ClosestPoint(newPos);
            if (Mathf.Approximately(closestPoint.x, newPos.x))
            {
                transform.position = newPos;
            }
            GetComponent<SpriteRenderer>().flipX = false;
        }

        // pick up
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (holdingBucket)
            {
                Debug.Log("Drop Bucket");
                bucket.transform.SetParent(null);
                holdingBucket = false;
            }
            else if (GetComponent<BoxCollider2D>().OverlapPoint(bucket.transform.position))
            {
                Debug.Log("Pick up Bucket");
                bucket.transform.SetParent(transform);
                holdingBucket = true;
            }
        }

        // interact
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (holdingBucket)
            {
                Debug.Log("Use Bucket");
                if (GameManager.instance.boatWaterLevel > 0)
                {
                    GameManager.instance.boatWaterLevel -= 5f;
                    bucketParticles.Play();
                }
            }
        }
    }
}
