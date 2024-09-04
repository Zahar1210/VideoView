using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InfoPanel : MonoBehaviour
{
    [SerializeField] private Title _title;
    [SerializeField] private ClosePanelButton _closePanelButton; 
    
    public void Initialize(UIDocument uiDocument, VisualElement panelName, InfoPanelActivity infoPanelActivity)
    {
        VisualElement root = uiDocument.rootVisualElement;
        
        _title.Initialize(panelName);
        infoPanelActivity.Initialize(panelName, root);
        _closePanelButton.Initialize(root, infoPanelActivity);
    }
}
