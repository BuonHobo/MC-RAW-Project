using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRateSetter : MonoBehaviour
{
    void Awake()
    {
        Application.targetFrameRate = Screen.currentResolution.refreshRate;
    }
}
