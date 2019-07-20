using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Image boatWaterLevel;

    private void Update()
    {
        boatWaterLevel.fillAmount = GameManager.instance.boatWaterLevel01;
    }
}
