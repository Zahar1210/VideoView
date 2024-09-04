using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ClosePanelButton : MonoBehaviour
{
    public void Initialize(VisualElement root, InfoPanelActivity infoPanelActivity)
    {
        Button closePanelButton = root.Q<Button>("CloseButton");
        if (closePanelButton != null)
        {
            closePanelButton.clicked += () => infoPanelActivity.SetActive(false);
        }
    }
}
