using UnityEngine;

public class Wave : MonoBehaviour
{
    public float speed = 0.1f;

    private void Update()
    {
        transform.Translate(Vector3.left * GameManager.instance.waterSpeed);
    }
}
