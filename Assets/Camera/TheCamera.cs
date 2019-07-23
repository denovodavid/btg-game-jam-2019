using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Cinemachine;

public class TheCamera : MonoBehaviour
{
    public PostProcessVolume postProcessVolume;
    ChromaticAberration chromaticAberration;

    private void Start()
    {
        if (postProcessVolume != null)
        {
            chromaticAberration = postProcessVolume.profile.GetSetting<ChromaticAberration>();
        }
    }

    public void Impact()
    {
        var impulseSource = GetComponent<CinemachineImpulseSource>();
        impulseSource.GenerateImpulse(Vector3.down * 3);
        StartCoroutine(chromaticFlash());
    }

    IEnumerator chromaticFlash()
    {
        chromaticAberration.intensity.value = 1f;
        yield return new WaitForSeconds(0.1f);
        chromaticAberration.intensity.value = 0f;
    }
}
