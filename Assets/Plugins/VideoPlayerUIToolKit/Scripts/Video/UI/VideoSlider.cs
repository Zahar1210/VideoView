using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Video;
using VideoPlayerAsset.UI;

public class VideoSlider : MonoBehaviour
{
    public VisualElement Root { get; set; }
    public VideoSliderManipulator VideoSliderManipulator { get; set; }
    
    [SerializeField] private UIDocument _uiDocument;
    [SerializeField] private VideoControlsUI _videoControlsUI;
    

    private VideoSliderManipulator _videoSliderManipulatorCached;
    
    public void Initialize(VisualElement root)
    {
        Root = root;
        VideoSliderManipulator = new VideoSliderManipulator(_videoControlsUI, Root);
    }
    
    private void Update()
    {
        if (_videoControlsUI.CurrentVideo != null && _videoControlsUI.CurrentVideo.VideoPlayer.isPlaying)
            VideoSliderManipulator.SliderProgress();
    }
    
    public void SliderPointerDown(PointerDownEvent evt)
    {
        VideoSliderManipulator.PointerDown();
    }
    
    public void VideoLoopPointReached(VideoPlayer source)
    {
        if (!_videoControlsUI.CurrentVideo.VideoPlayer.isLooping)
        {
            _videoControlsUI.CurrentVideo.VideoPlayer.Pause();

            Root.Q<Button>("Button_Play").style.display = DisplayStyle.Flex;
            Root.Q<Button>("Button_Pause").style.display = DisplayStyle.None;
        }
    }
}
