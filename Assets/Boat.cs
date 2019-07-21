using UnityEngine;

public class Boat : MonoBehaviour
{
    public GameObject sail;
    public GameObject sailReefed;
    public bool sailIsReefed;
    public float sailHealth = 100f;
    public float boatSpeed = 0f;
    public float maxBoatSpeed = 10f;
    public float boatAcceleration = 0.5f;
    public float boatDecelearation = 0.5f;
    public float sailHealth01 { get => sailHealth / 100f; }
    public float boatSpeed01 { get => boatSpeed / maxBoatSpeed; }
    private void Update()
    {
        sail.SetActive(!sailIsReefed);
        sailReefed.SetActive(sailIsReefed);
        if (GameManager.instance.shouldReefSails && !sailIsReefed)
        {
            sailHealth -= 10 * Time.deltaTime;
        }

        if (sailHealth <= 0f)
        {
            // TODO: trigger sail anim
            GameManager.instance.GameOver();
        }

        boatSpeed += (sailIsReefed ? -boatDecelearation : boatAcceleration) * Time.deltaTime;
        boatSpeed = Mathf.Clamp(boatSpeed, 1, maxBoatSpeed);
    }
}
