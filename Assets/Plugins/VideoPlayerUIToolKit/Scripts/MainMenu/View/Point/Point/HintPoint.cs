using UnityEngine;
using UnityEngine.UIElements;

public class HintPoint : Point
{
    [SerializeField] private InfoPanel _infoPanel;
    [SerializeField] private InfoPanelActivity _infoPanelActivity;
    [SerializeField] private string _panelName;
    private Camera _mainCamera;
    private VisualElement _panel;
    
    void OnEnable()
    {
        Debug.LogError("initiallize");
        Initialize();
    }

    private void Initialize()
    {
        _mainCamera = Camera.main;
        _panel = UIDocument.rootVisualElement.Q<VisualElement>(_panelName);
        AddRelocatePoint();
        _infoPanel.Initialize(UIDocument, _panel, _infoPanelActivity);

        InvokeRepeating(nameof(UpdatePosition), 0, 0.001f);
    }

    void UpdatePosition()
    {
        Vector3 screenPosition = _mainCamera.WorldToScreenPoint(transform.position);
        if (screenPosition.z > 0)
        {
            screenPosition.y = Screen.height - screenPosition.y;
            PointElement.style.left = screenPosition.x - PointElement.resolvedStyle.width / 2;
            PointElement.style.top = screenPosition.y - PointElement.resolvedStyle.height / 2;
        }
    }
    
    private void AddRelocatePoint()
    {
        if (PointElement != null)
        {
            SetActive(false);
        }
        Button point = new Button();
        point.AddToClassList("hintPoint");
        PointElement = point;
        UIDocument.rootVisualElement.Add(PointElement);
        PointElement.name = Random.Range(0, 45).ToString();
        PointElement.clicked += OnClick;
    }
    
    public void OnClick()
    {
        if (_panel != null)
        {
            _infoPanelActivity.SetActive(true);
        }
        Debug.LogError("открываем панель для инфы");
    }

    public override void SetActive(bool isActive)
    {
        PointElement.style.display = (isActive) ? DisplayStyle.Flex : DisplayStyle.None;
    }
}
