using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UIElements;
using VideoPlayerAsset.UI;

public class SoundButton : MonoBehaviour
{
    [SerializeField] private string _activeStyle;
    [SerializeField] private string _nonActiveStyle;
    
    private VisualElement _root;
    private VisualElement _soundButtons;
    private VisualElement _soundArea;
    private Button _muteButton;
    private Button _soundButton;
    
    private VisualElement _valumeSliderArea;
    private Slider _slider;

    private VideoControlsUI _videoControlsUI;
    
    public void Initialize(VisualElement root, VideoControlsUI videoControlsUI)
    {
        _root = root;
        _videoControlsUI = videoControlsUI;
        _soundButton = _root.Q<Button>("Button_Sound");
        _muteButton = _root.Q<Button>("Button_Mute");
        _soundArea = _root.Q<VisualElement>("Sound");
        _valumeSliderArea = _root.Q<VisualElement>("ValumeSliderArea");
        _slider = _valumeSliderArea.Q<Slider>();
        _soundButtons = _root.Q<VisualElement>("SoundButtons");
        
        Register();
    }
    
    private void Register()
    {
        _muteButton.clicked += ButtonSound;
        _soundButton.clicked += ButtonMute;
        
        _soundArea.RegisterCallback<MouseEnterEvent>(evt => SetActiveSoundSlider(_activeStyle, _nonActiveStyle, _valumeSliderArea));
        _soundButtons.RegisterCallback<MouseLeaveEvent>(evt => SetActiveSoundSlider(_nonActiveStyle, _activeStyle, _valumeSliderArea));
        
        // Устанавливаем диапазон значений слайдера от 0 до 1
        _slider.lowValue = 0f;
        _slider.highValue = 1f;
        
        _slider.RegisterValueChangedCallback(evt =>
        {
            SliderValueChanged(evt.newValue);
        });
    }

    private void ButtonSound()
    {
        _muteButton.style.display = DisplayStyle.None;
        _soundButton.style.display = DisplayStyle.Flex;
        
        _videoControlsUI.CurrentVideo.Mute(false);
        // _slider.value = _videoControlsUI.CurrentVideo.VideoPlayer.GetAudioSampleRate(0);
    }

    private void ButtonMute()
    {
        _muteButton.style.display = DisplayStyle.Flex;
        _soundButton.style.display = DisplayStyle.None;
        
        _videoControlsUI.CurrentVideo.Mute(true);
        _slider.value = _slider.lowValue;
    }

    private void SetActiveSoundSlider(string newStyle, string pastStyle, VisualElement element)
    {
        element.RemoveFromClassList(pastStyle);
        element.AddToClassList(newStyle);
    }
    
    private void SliderValueChanged(float evt)
    {
        if (evt == 0)
        {
            ButtonMute();
        }
        else
        {
            ButtonSound();
            _videoControlsUI.CurrentVideo.VideoPlayer.SetDirectAudioVolume(0, evt);
        }
    }
    
    private void Unregister()
    {
        _soundButton.clicked -= ButtonSound;
        _muteButton.clicked -= ButtonMute;
    }
}
