using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using VideoPlayerAsset.UI;

public class LoopNoLoopButton : MonoBehaviour
{
    private VisualElement _root;
    private Button _noLoopButton;
    private Button _loopButton;

    private VideoControlsUI _videoControlsUI;
    
    public void Initialize(VisualElement root, VideoControlsUI videoControlsUI)
    {
        _root = root;
        _videoControlsUI = videoControlsUI;
        _loopButton = _root.Q<Button>("Button_Loop");
        _noLoopButton = _root.Q<Button>("Button_NoLoop");
        
        Register();
    }
    
    private void Register()
    {
        _loopButton.clicked += ButtonLoop;
        _noLoopButton.clicked += ButtonNoLoop;
    }
    
    private void Unregister()
    {
        _loopButton.clicked -= ButtonLoop;
        _noLoopButton.clicked -= ButtonNoLoop;
    }

    private void ButtonLoop()
    {
        _loopButton.style.display = DisplayStyle.None;
        _noLoopButton.style.display = DisplayStyle.Flex;

        _videoControlsUI.CurrentVideo.LoopVideo(false);
    }

    private void ButtonNoLoop()
    {
        _loopButton.style.display = DisplayStyle.Flex;
        _noLoopButton.style.display = DisplayStyle.None;

        _videoControlsUI.CurrentVideo.LoopVideo(true);
    }
}
