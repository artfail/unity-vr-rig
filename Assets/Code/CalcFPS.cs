using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CalcFPS : MonoBehaviour
{

    public TextMeshProUGUI outText;
    const int SampleCount = 60;
    private float[] samples = new float[SampleCount];
    private int samplesUsed = 0;
    private int nextSampleIndex = 0;
    void Update()
    {
        float sample = Time.unscaledDeltaTime;
        if (samplesUsed < SampleCount)
        {
            nextSampleIndex = samplesUsed;
            samples[samplesUsed++] = sample;
        }
        else
        {
            nextSampleIndex = (nextSampleIndex + 1) % SampleCount;
            samples[nextSampleIndex] = sample;
        }

        float sum = 0.0f;
        for (int i = 0; i < samplesUsed; i++)
        {
            sum += samples[i];
        }

        float aveSeconds = sum / ((float)samplesUsed);
        float fps = 1.0f / aveSeconds;

        outText.text = "FPS: " + fps.ToString("F1") + ",  MS: " + (aveSeconds * 1000.0f).ToString("N2");
    }
}
