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
    public float maxBoatWaterLevel = 100f;
    public float boatWaterLevel01 { get => boatWaterLevel / maxBoatWaterLevel; }

    private void Update()
    {
        boatWaterLevel = Mathf.Clamp(boatWaterLevel, 0, maxBoatWaterLevel);
    }
}
