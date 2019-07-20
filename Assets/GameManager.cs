using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance
    {
        get => _instance = _instance != null ? _instance : FindObjectOfType<GameManager>();
    }

    public float waterSpeed = 0.03f;
    public float boatWaterLevel = 0f;
}
