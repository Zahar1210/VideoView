using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using VideoPlayerAsset.UI;

public class MainMenuInitializer : MonoBehaviour
{
    [SerializeField] private VideoControlsUI _videoControlsUI;
    [SerializeField] private VideoPlayerActivity _videoPlayerActivity;
    
    [SerializeField] private CloseButton _closeButton;
    [SerializeField] private AllInOrderVideoButton _allInOrderVideoButton;
    [SerializeField] private VideoButton _videoButton;
    [SerializeField] private ViewButton _viewButton;

    public void Start()
    {
        VisualElement root = gameObject.GetComponent<UIDocument>().rootVisualElement;
        
        _closeButton.Initialize(root);
        _allInOrderVideoButton.Initialize(root, _videoControlsUI, _videoPlayerActivity);
        _videoButton.Initialize(root, _videoControlsUI, _videoPlayerActivity);
        _viewButton.Initialize(root);
    }
}
