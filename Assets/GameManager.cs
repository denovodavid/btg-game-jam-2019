using UnityEngine;
using UnityEngine.Playables;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance
    {
        get => _instance = _instance != null ? _instance : FindObjectOfType<GameManager>();
    }

    public PlayableDirector director;
    public GameUI gameUI;
    public Animator boatAnimator;
    public PlayerCharacter playerCharacter;
    public Boat boat;
    public bool win = false;
    public float waterSpeed = 0.03f;
    public float windSpeed = 0f;
    public float reefWindSpeed = 70f;
    public float maxWindSpeed = 100f;
    public float boatWaterLevel = 0f;
    public float maxBoatWaterLevel = 100f;
    public float boatWaterLevel01 { get => boatWaterLevel / maxBoatWaterLevel; }
    public float windSpeed01 { get => windSpeed / maxWindSpeed; }
    public bool shouldReefSails { get => windSpeed >= reefWindSpeed; }
    public float levelProgress01 { get => (float)(director.time / director.duration); }

    private void Update()
    {
        boatWaterLevel = Mathf.Clamp(boatWaterLevel, 0, maxBoatWaterLevel);

        if (boatWaterLevel >= maxBoatWaterLevel)
        {
            // sink boat
            boatAnimator.SetTrigger("Sink");
            GameOver();
        }

        director.playableGraph.GetRootPlayable(0).SetSpeed(boat.boatSpeed01);

        if (!win && director.time >= director.duration)
        {
            Win();
        }
    }

    public void Win()
    {
        Debug.Log("WIN!");
        win = true;
        gameUI.Win();
        StartCoroutine(ReturnToMenu());
    }

    public void GameOver()
    {
        playerCharacter.Die();
        director.Pause();
        waterSpeed = 0f;
        gameUI.GameOver();
        StartCoroutine(ReturnToMenu());
    }

    IEnumerator ReturnToMenu()
    {
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene(0);
    }
}
