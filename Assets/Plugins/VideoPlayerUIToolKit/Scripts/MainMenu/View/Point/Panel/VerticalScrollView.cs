using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class VerticalScrollView : ScrollView
{
    public new class UxmlFactory: UxmlFactory <VerticalScrollView, UxmlTraits> {}
    
    public bool IsMouseContains { get; set; }
    public VerticalScrollView() 
    {
        verticalScroller.AddToClassList("scrollBarAppointmentJudge");

        verticalScroller.style.width = 10;
        verticalScroller.style.minWidth = 10;
        
        verticalScroller.highButton.style.display = DisplayStyle.None;
        verticalScroller.lowButton.style.display = DisplayStyle.None;

        verticalScroller.slider.style.width = 10;
        verticalScroller.slider.style.minWidth = 10;
        verticalScroller.slider.style.marginTop = 0;
        verticalScroller.slider.style.marginBottom = 0;

        VisualElement border = verticalScroller.slider.Q<VisualElement>(name: "unity-dragger-border");
        border.style.marginLeft = 0;
        VisualElement tracker = verticalScroller.slider.Q<VisualElement>(name: "unity-tracker");
        if (tracker != null)
        {
            Color softGray = new Color32(230, 230, 230, 255); 
            SetVisualElement(tracker, softGray);
            
            VisualElement dragger = verticalScroller.Q<VisualElement>("unity-dragger");
            if (dragger != null)
            {
                dragger.style.width = 10;
                dragger.style.left = 0;
                Color softBlue = new Color32(155, 155, 155, 255); 
                SetVisualElement(dragger, softBlue);
            }
        }
        foreach (var child in Children())
            child.pickingMode = PickingMode.Ignore;
        
        RegisterCallback<MouseEnterEvent>(_ => IsMouseContains = true);
        RegisterCallback<MouseLeaveEvent>(_ => IsMouseContains = false);
    }
    
    private void SetVisualElement(VisualElement visualElement, Color color)
    {
        
        visualElement.style.backgroundColor = new StyleColor(color);
        
        visualElement.style.borderTopLeftRadius = 5;
        visualElement.style.borderTopRightRadius = 5;
        visualElement.style.borderBottomLeftRadius = 5;
        visualElement.style.borderBottomRightRadius = 5;
            
        visualElement.style.borderBottomWidth = 0;
        visualElement.style.borderTopWidth = 0;
        visualElement.style.borderLeftWidth = 0;
        visualElement.style.borderRightWidth = 0;
    }
}