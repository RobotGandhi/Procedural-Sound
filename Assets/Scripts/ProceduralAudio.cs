using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioLowPassFilter))]
public class ProceduralAudio : MonoBehaviour
{
    private float sampling_frequency = 48000;

    [Range(0f, 1f)]
    public float noiseRatio = 0.005f;

    public float sirenSpeed = 1f;
    public float sirenRange = 100f;
    public float sirenSpeedRatio = 2f;
    public float sirenApproachSpeed = 0f;

    public float sirenApproachOffset = 0f;

    public bool isMute = false;

    //for noise part
    [Range(-1f, 1f)]
    public float offset;

    public float cutoffOn = 800;
    public float cutoffOff = 100;

    public bool cutOff;



    //for tonal part

    public float frequency = 440f;
    public float gain = 0.01f;

    private float increment;
    private float phase;

    private bool sirenSpeedUp = true;
    private float sirenIncrement;
    private float sirenPhase;

    System.Random rand = new System.Random();
    AudioLowPassFilter lowPassFilter;

    void Awake()
    {
        sampling_frequency = AudioSettings.outputSampleRate;

        lowPassFilter = GetComponent<AudioLowPassFilter>();
        Update();
    }


    //Sandbox
    void OnAudioFilterRead(float[] data, int channels)
    {
        // update increment in case frequency has changed
        sirenIncrement = frequency * 2f * Mathf.PI / sampling_frequency;
        float modifiedSpeed = sirenSpeed + sirenApproachOffset;

        //Update the values of the siren noise.
        sirenApproachOffset += sirenApproachSpeed * 0.001f;
        if (sirenSpeedUp) sirenIncrement *= sirenSpeedRatio;
        sirenPhase += sirenIncrement;
        if (sirenSpeedUp && (modifiedSpeed * sirenPhase > Mathf.PI * 1.5f || (modifiedSpeed * sirenPhase > Mathf.PI * 0.5 && modifiedSpeed * sirenPhase < Mathf.PI))) {
            sirenSpeedUp = false;
        }
        else sirenSpeedUp = true;
        if (modifiedSpeed * sirenPhase > 2 * Mathf.PI) {
            sirenPhase = 0;
        }
        float modFrequency = frequency + (sirenApproachOffset * 100) + sirenRange * (float)Mathf.Sin(modifiedSpeed * sirenPhase);
        //increment = frequency * 2f * Mathf.PI / sampling_frequency; //Pure sine
        increment = modFrequency * 2f * Mathf.PI / sampling_frequency; //Siren

        for (int i = 0; i < data.Length; i++)
        {

            //noise
            float noisePart = noiseRatio * (float)(rand.NextDouble() * 2.0 - 1.0 + offset);
            phase += increment;
            if (phase > 2 * Mathf.PI) phase = 0;

            //tone
            //tonalPart = (1f - noiseRatio) * (float)(gain * Mathf.Sign(Mathf.Sin(phase))); //Square
            float tonalPart = (1f - noiseRatio) * (float)Mathf.Sin(phase); //Sine
            //together
            data[i] = System.Convert.ToInt32(!isMute) * gain * (noisePart + tonalPart);

            // if we have stereo, we copy the mono data to each channel
            if (channels == 2)
            {
                data[i + 1] = data[i];
                i++;
            }
        }
    }

    void Update()
    {
        lowPassFilter.cutoffFrequency = cutOff ? cutoffOn : cutoffOff;
    }
}