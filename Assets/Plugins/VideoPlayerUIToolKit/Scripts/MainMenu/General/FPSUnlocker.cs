using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSUnlocker : MonoBehaviour
{
    [SerializeField] private int _targetFPS = 60;
    [SerializeField] [Range(0,2)] public int _vSyncCount = 0;

    private void Start()
    {
        UpdateSettings();
    }	
    
    private void OnValidate()
    {
        UpdateSettings();
    }

    private void UpdateSettings()
    {
        QualitySettings.vSyncCount = _vSyncCount;
        Application.targetFrameRate = _targetFPS;
    }
}
