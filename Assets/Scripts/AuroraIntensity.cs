using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AuroraIntensity : MonoBehaviour
{
    [SerializeField] private Light2D auroraLight;
    [SerializeField] private float intensityMin;
    [SerializeField] private float intensityMax;
    [SerializeField] private float transitionTimeMin = 5f;
    [SerializeField] private float transitionTimeMax = 10f;

    private float targetIntensity;
    private float elapsedTime;
    private float transitionIntensityTime;
    [SerializeField] private float newIntensityChangeTime;

    private void Start()
    {
        StartCoroutine(ChangeColorRoutine());
    }

    private IEnumerator ChangeColorRoutine()
    {
        while (true) // Endlos-Schleife für kontinuierlichen Farbwechsel
        {
            targetIntensity = Random.Range(intensityMin, intensityMax);
            elapsedTime = 0f;
            transitionIntensityTime = Random.Range(transitionTimeMin, transitionTimeMax);
            

            while (elapsedTime < transitionIntensityTime)
            {
                auroraLight.intensity = Mathf.Lerp(auroraLight.intensity, targetIntensity, elapsedTime / transitionIntensityTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            yield return new WaitForSeconds(newIntensityChangeTime);
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
