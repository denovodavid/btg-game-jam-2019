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
    public bool gameOver = false;
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
    public AudioSource sfxSource;
    public AudioClip oceanSFX;
    public AudioSource musicSource;
    public AudioClip intro;
    public AudioClip shanty;
    public AudioClip winTheme;
    bool introPlayed = false;

    private void Awake()
    {
        sfxSource.clip = oceanSFX;
        sfxSource.Play();
    }

    private void Update()
    {
        boatWaterLevel = Mathf.Clamp(boatWaterLevel, 0, maxBoatWaterLevel);

        if (boatWaterLevel >= maxBoatWaterLevel)
        {
            // sink boat
            boatAnimator.SetTrigger("Sink");
            GameOver();
        }

        if (director.playableGraph.IsValid())
        {
            director.playableGraph.GetRootPlayable(0).SetSpeed(boat.boatSpeed01);
        }

        if (!win && director.time >= director.duration)
        {
            Win();
        }

        sfxSource.volume = windSpeed01 + 0.1f;
        musicSource.pitch = boat.boatSpeed01 * 0.5f + 0.5f;
        if (!gameOver && !win && !musicSource.isPlaying && boat.boatSpeed01 >= 1)
        {
            if (!introPlayed)
            {
                musicSource.clip = intro;
                musicSource.loop = false;
                introPlayed = true;
            }
            else
            {
                musicSource.clip = shanty;
                musicSource.loop = true;
            }
            musicSource.Play();
        }
    }

    public void Win()
    {
        Debug.Log("WIN!");
        win = true;
        musicSource.Stop();
        musicSource.loop = false;
        musicSource.clip = winTheme;
        musicSource.Play();
        gameUI.Win();
        StartCoroutine(ReturnToMenu());
    }

    public void GameOver()
    {
        gameOver = true;
        if (musicSource.isPlaying) musicSource.Stop();
        playerCharacter.Die();
        director.Pause();
        waterSpeed = 0f;
        gameUI.GameOver();
        StartCoroutine(ReturnToMenu());
    }

    IEnumerator ReturnToMenu()
    {
        yield return new WaitForSeconds(10);
        sfxSource.Stop();
        SceneManager.LoadScene(0);
    }
}
