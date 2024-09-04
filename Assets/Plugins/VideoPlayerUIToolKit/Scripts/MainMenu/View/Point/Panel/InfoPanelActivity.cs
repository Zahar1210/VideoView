using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InfoPanelActivity : MonoBehaviour
{
    private VisualElement _panel;
    public VisualElement _root;
    
    public void Initialize(VisualElement panel, VisualElement root)
    {
        _panel = panel;
        _root = root;
    }

    public void SetActive(bool isActive)
    {
        _panel.style.display = (isActive) ? DisplayStyle.Flex : DisplayStyle.None;
        if (isActive)
        {
            _root.Insert(_root.childCount, _panel);
        }
    }
}