using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public float speed = 0.05f;
    public SpriteRenderer boat;
    Bounds boatBounds;

    private void Start()
    {
        boatBounds = new Bounds(boat.bounds.center, boat.bounds.size);
        boatBounds.Expand(-1.5f);
    }

    private void Update()
    {
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

        // snap position

        float gridScale = 24f;
        var currentPos = transform.position;
        transform.position = new Vector3(
            Mathf.Round(currentPos.x * gridScale) / gridScale,
            Mathf.Round(currentPos.y * gridScale) / gridScale,
            Mathf.Round(currentPos.z * gridScale) / gridScale
        );
    }
}
