using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VideoPlayerAsset.UI;

public class VideoModeActivity : MonoBehaviour
{
    public bool IsVideoModeActive => _videoControlsUI.gameObject.activeSelf;
    [SerializeField] private GameObject _videoControlsUI;

    public void SetActive(bool isActive)
    {
        _videoControlsUI.gameObject.SetActive(isActive);
    }
}