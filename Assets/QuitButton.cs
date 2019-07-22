using UnityEngine;

public class QuitButton : MonoBehaviour
{
    private void Awake()
    {
#if UNITY_WEBGL
        gameObject.SetActive(false);
#endif
    }
}
