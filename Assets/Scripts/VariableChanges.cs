using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableChanges : MonoBehaviour
{
    public ProceduralAudio sirenAudio;
    private void Start()
    {
        sirenAudio = GetComponent<ProceduralAudio>();
    }

    public void changeSirenSpeed(float newSpeed)
    {
        sirenAudio.sirenSpeed = newSpeed;
    }
    public void changeSirenSpeedRatio(float newSpeed)
    {
        sirenAudio.sirenSpeedRatio = newSpeed;
    }
    public void changeSirenFrequency(float newFreq)
    {
        sirenAudio.frequency = newFreq;
    }
    public void changeSirenRange(float newRange)
    {
        sirenAudio.sirenRange = newRange;
    }
    public void changeSirenApproachSpeed(float newSpeed)
    {
        sirenAudio.sirenApproachSpeed = newSpeed;
    }
    public void resetOffset()
    {
        sirenAudio.sirenApproachOffset = 0f;
    }
    public void setMute(bool newSetting)
    {
        sirenAudio.isMute = newSetting;
    }
}
