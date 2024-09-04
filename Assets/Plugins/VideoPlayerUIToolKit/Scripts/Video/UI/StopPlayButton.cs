using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using VideoPlayerAsset.UI;

public class StopPlayButton : MonoBehaviour
{
    private VisualElement _root;
    private Button _stopButton;
    private Button _playButton;

    private VideoControlsUI _videoControlsUI;
    
    public void Initialize(VisualElement root, VideoControlsUI videoControlsUI)
    {
        _root = root;
        _videoControlsUI = videoControlsUI;
        _playButton = _root.Q<Button>("Button_Play");
        _stopButton = _root.Q<Button>("Button_Pause");
        
        Register();
    }
    
    private void Register()
    {
        _playButton.clicked += ButtonPlay;
        _stopButton.clicked += ButtonPause;
    }
    
    private void Unregister()
    {
        _playButton.clicked -= ButtonPlay;
        _stopButton.clicked -= ButtonPause;
    }

    private void ButtonPlay()
    {
        _playButton.style.display = DisplayStyle.None;
        _stopButton.style.display = DisplayStyle.Flex;

        _videoControlsUI.CurrentVideo.PlayVideo();
    }

    private void ButtonPause()
    {
        _playButton.style.display = DisplayStyle.Flex;
        _stopButton.style.display = DisplayStyle.None;

        _videoControlsUI.CurrentVideo.PauseVideo();
    }
}
