using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class VideoPlayerActivity : MonoBehaviour
{
    private VisualElement _root;
    
    public void OnEnable()
    {
        VisualElement root = gameObject.GetComponent<UIDocument>().rootVisualElement;
        _root = root.Q<VisualElement>("VideoPlayer");
    }

    public void SetActive(bool isActive)
    {
        _root.style.display = (isActive) ? DisplayStyle.Flex : DisplayStyle.None;
    }
}
