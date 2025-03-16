using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class Aurora : MonoBehaviour
{
    [SerializeField] private Light2D auroraLight;
    [SerializeField] private Gradient gradient;
    [SerializeField] private float transitionTimeMin = 5f;
    [SerializeField] private float transitionTimeMax = 10f;

    private Color targetColor;
    private float elapsedTime;
    private float transitionTime;
    [SerializeField] private float newIntensityChangeTime;

    private void Start()
    {
        StartCoroutine(ChangeColorRoutine());
    }

    private IEnumerator ChangeColorRoutine()
    {
        while (true) // Endlos-Schleife für kontinuierlichen Farbwechsel
        {
            targetColor = gradient.Evaluate(Random.Range(0f, 1f));
            elapsedTime = 0f;
            transitionTime = Random.Range(transitionTimeMin, transitionTimeMax);
            

            while (elapsedTime < transitionTime)
            {
                auroraLight.color = Color.Lerp(auroraLight.color, targetColor, elapsedTime / transitionTime);
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
