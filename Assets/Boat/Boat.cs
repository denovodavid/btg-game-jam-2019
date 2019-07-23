using UnityEngine;

public class Boat : MonoBehaviour
{
    public GameObject sail;
    public GameObject sailReefed;
    public bool sailIsReefed;
    public float sailHealth = 100f;
    public float maxSailHealth = 100f;
    public float boatSpeed = 0f;
    public float maxBoatSpeed = 10f;
    public float boatAcceleration = 0.5f;
    public float boatDecelearation = 0.5f;
    public float sailHealth01 { get => sailHealth / maxSailHealth; }
    public float boatSpeed01 { get => boatSpeed / maxBoatSpeed; }
    public AudioClip closeSail;
    public AudioClip openSail;
    bool boatDead = false;

    private void Update()
    {
        sail.SetActive(!sailIsReefed);
        sailReefed.SetActive(sailIsReefed);
        if (GameManager.instance.shouldReefSails && !sailIsReefed)
        {
            sailHealth -= 10 * Time.deltaTime;
        }

        if (!boatDead && sailHealth <= 0f)
        {
            boatDead = true;
            GetComponent<Animator>().SetTrigger("BreakSail");
            GetComponent<AudioSource>().clip = closeSail;
            GetComponent<AudioSource>().Play();
            GameManager.instance.GameOver();
        }

        boatSpeed += (sailIsReefed ? -boatDecelearation : boatAcceleration) * Time.deltaTime;
        boatSpeed = Mathf.Clamp(boatSpeed, 1, maxBoatSpeed);
    }

    public void ToggleSails()
    {
        GetComponent<AudioSource>().clip = sailIsReefed ? openSail : closeSail;
        GetComponent<AudioSource>().Play();
        sailIsReefed = !sailIsReefed;
    }
}
