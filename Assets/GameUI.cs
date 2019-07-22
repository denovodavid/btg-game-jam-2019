using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameUI : MonoBehaviour
{
    public Image boatWaterLevel;
    public Image windLevel;
    public Boat boat;
    public Image sailhealth;
    public GameObject windWarning;
    public Animator gameOverAnimator;
    public CanvasGroup whiteScreen;
    public Slider levelProgress;
    public Animator winAnimator;
    public TMPro.TextMeshProUGUI speedText;

    private void Update()
    {
        boatWaterLevel.fillAmount = GameManager.instance.boatWaterLevel01;
        windLevel.fillAmount = GameManager.instance.windSpeed01;
        sailhealth.fillAmount = boat.sailHealth01;
        windWarning.SetActive(GameManager.instance.shouldReefSails);
        levelProgress.value = GameManager.instance.levelProgress01;
        speedText.SetText(boat.boatSpeed.ToString("#") + " km/h");
    }

    public void Win()
    {
        winAnimator.SetTrigger("Win");
    }

    public void GameOver()
    {
        gameOverAnimator.SetTrigger("GameOver");
    }

    public void ScreenFlash()
    {
        StartCoroutine(FlashScreen());
    }

    IEnumerator FlashScreen()
    {
        whiteScreen.alpha = 1;
        yield return new WaitForSeconds(1/60*3);
        whiteScreen.alpha = 0;
    }
}
