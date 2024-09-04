using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using VideoPlayerAsset.UI;

public class MuteSoundButton : MonoBehaviour
{
    private VisualElement _root;
    private Button _muteButton;
    private Button _soundButton;

    private VideoControlsUI _videoControlsUI;
    
    public void Initialize(VisualElement root, VideoControlsUI videoControlsUI)
    {
        _root = root;
        _videoControlsUI = videoControlsUI;
        _soundButton = _root.Q<Button>("Button_Sound");
        _muteButton = _root.Q<Button>("Button_Mute");
        
        Register();
    }
    
    private void Register()
    {
        _soundButton.clicked += ButtonSound;
        _muteButton.clicked += ButtonMute;
    }
    
    private void Unregister()
    {
        _soundButton.clicked -= ButtonSound;
        _muteButton.clicked -= ButtonMute;
    }

    private void ButtonSound()
    {
        _soundButton.style.display = DisplayStyle.None;
        _muteButton.style.display = DisplayStyle.Flex;

        _videoControlsUI.CurrentVideo.Mute(false);
    }

    private void ButtonMute()
    {
        _soundButton.style.display = DisplayStyle.Flex;
        _muteButton.style.display = DisplayStyle.None;

        _videoControlsUI.CurrentVideo.Mute(true);
    }
}
