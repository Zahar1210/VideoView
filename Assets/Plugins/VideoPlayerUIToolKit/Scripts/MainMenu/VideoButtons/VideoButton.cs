using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using VideoPlayerAsset.UI;
using VideoPlayerAsset.Video;

public class VideoButton : MonoBehaviour
{
    [SerializeField] private VideoButtonData[] _videoButton;
    public VideoButtonData[] VideoData => _videoButton; 
    
    public void Initialize(VisualElement root, VideoControlsUI videoControlsUI, VideoPlayerActivity videoPlayerActivity) 
    {
        foreach (var buttonInfo in _videoButton)
        {
            Button videoButton = root.Q<Button>(buttonInfo.DocumentButtonName);
            if (videoButton != null)
            {
                videoButton.clicked += () => OnVideoButtonClick(buttonInfo.Video, videoControlsUI, videoPlayerActivity);
            }
        }
    }

    private void OnVideoButtonClick(Video video, VideoControlsUI videoControlsUI, VideoPlayerActivity videoPlayerActivity) 
    {
        Debug.LogError(video);
        videoControlsUI.ChangeCurrentVideoController(video);
        videoPlayerActivity.SetActive(true);
    }
    
    [System.Serializable]
    public class VideoButtonData
    {
        public Video Video;
        public string DocumentButtonName;
    }
}
