using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class TheCamera : MonoBehaviour
{

    private void LateUpdate()
    {
        Camera.main.transform.rotation = Quaternion.identity;
    }
    public void Impact()
    {
        var impulseSource = GetComponent<Cinemachine.CinemachineImpulseSource>();
        impulseSource.GenerateImpulse(Vector3.down * 3);
        StartCoroutine(chromaticFlash());
    }

    IEnumerator chromaticFlash()
    {
        var pp = GetComponent<Cinemachine.PostFX.CinemachinePostProcessing>();
        pp.Profile.GetSetting<ChromaticAberration>().intensity.value = 1f;
        yield return new WaitForSeconds(0.1f);
        pp.Profile.GetSetting<ChromaticAberration>().intensity.value = 0f;
    }
}
