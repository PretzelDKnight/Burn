using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVisualizer : MonoBehaviour
{
    [Range(1.0f, 4500.0f)]
    public float multiplier;
    public int minRange = 0;
    public int maxRange = 64;
    private SkinnedMeshRenderer skinnedMeshRenderer;
    private float prevAvg = 0.0f;
    float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime * 0.05f;

        float[] spectrum = new float[64];
        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);

        if (maxRange < minRange)
            maxRange = minRange + 1;

        minRange = Mathf.Clamp(minRange, 0, 63);
        maxRange = Mathf.Clamp(maxRange, 0, 63);

        float avg = 0;

        for (int i = minRange; i < maxRange; i++)
            avg += Mathf.Abs(spectrum[i]);

        avg = avg / (float)Mathf.Abs(maxRange - minRange);

        if (avg - prevAvg > 0.0012f) // avg is > than prevAvg
        {
            avg = prevAvg + 0.0012f;
            avg = Mathf.Lerp(prevAvg, avg, time);
        }
        else if (avg - prevAvg < -0.0012f) // avg is < than prevAvg
        {
            avg = prevAvg - 0.0012f;
            avg = Mathf.Lerp(prevAvg, prevAvg - 0.001f, time);
        }

        skinnedMeshRenderer.SetBlendShapeWeight(0, avg * multiplier);
        prevAvg = avg;
    }
}
