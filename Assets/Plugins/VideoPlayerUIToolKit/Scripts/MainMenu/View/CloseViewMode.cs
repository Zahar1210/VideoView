using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CloseViewMode : MonoBehaviour
{
    [SerializeField] private ViewModeActivity _viewModeActivity;
    [SerializeField] private VideoModeActivity _videoModeActivity;
    
    public void Initialize(VisualElement root)
    {
        Button close = root.Q<Button>("ViewCloseButton");
        if (close != null)
        {
            close.clicked += OnCloseClick;
        }
    }

    private void OnCloseClick()
    {
        _videoModeActivity.SetActive(true);
        _viewModeActivity.SetActive(false);
    }
}
