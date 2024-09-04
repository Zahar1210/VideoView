using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Title : MonoBehaviour
{
    [SerializeField] private string _titleText;
    
    public void Initialize(VisualElement root)
    {
        Label title = root.Q<Label>();
        title.text = _titleText;
    }
}
