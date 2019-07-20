using UnityEngine;

public class Wave : MonoBehaviour
{
    private void Update()
    {
        transform.Translate(Vector3.left * GameManager.instance.waterSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Equals("Boat"))
        {
            GameManager.instance.boatWaterLevel += 10f;
            Debug.Log("WAVE HIT");
        }
    }
}
