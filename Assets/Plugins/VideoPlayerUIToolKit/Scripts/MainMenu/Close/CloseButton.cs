using UnityEngine;
using UnityEngine.UIElements;

public class CloseButton : MonoBehaviour
{
    public void Initialize(VisualElement root)
    {
        var closeButton = root.Q<Button>("CloseButton");
        if (closeButton != null)
        {
           closeButton.clicked += OnCloseButtonClick;
        }
    }

    private void OnCloseButtonClick()
    {
        Debug.LogError("close button clicked");
        Application.Quit();
    }
}
