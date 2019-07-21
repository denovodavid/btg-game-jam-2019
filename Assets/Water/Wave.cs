using UnityEngine;

public class Wave : MonoBehaviour
{
    public float waveDamage = 20f;
    bool goingDown = false;
    private void Update()
    {
        transform.Translate((Vector3.left + (goingDown ? Vector3.down * 0.25f : Vector3.zero)) * GameManager.instance.waterSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Equals("Boat"))
        {
            GameManager.instance.boatWaterLevel += waveDamage;
            GetComponent<AudioSource>().Play();
            goingDown = true;
            Debug.Log("WAVE HIT");
        }
    }
}
