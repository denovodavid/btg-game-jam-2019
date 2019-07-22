using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScoller : MonoBehaviour
{
    public Transform waters;
    public float speed = 0.1f;
    public int pixelWidth = 320;
    public int objectLength = 4;
    float width = 0;

    private void Awake()
    {
        width = (float)pixelWidth / 24f;
    }

    private void Update()
    {
        waters.Translate(Vector3.left * GameManager.instance.waterSpeed * Time.deltaTime * 70);
        if (waters.localPosition.x < 0)
        {
            waters.localPosition = new Vector3(width, 0, 0);
        }
    }
}
