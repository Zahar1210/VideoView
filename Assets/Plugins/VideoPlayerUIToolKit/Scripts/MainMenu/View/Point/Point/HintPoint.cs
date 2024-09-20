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
            PointElement.MainPoint.style.left = screenPosition.x - PointElement.MainPoint.resolvedStyle.width / 2;
            PointElement.MainPoint.style.top = screenPosition.y - PointElement.MainPoint.resolvedStyle.height / 2;
        }
    }
    
    private void AddRelocatePoint()
    {
        PointElement.MainPoint?.RemoveFromHierarchy();

        Button point = new Button
        {
            style =
            {
                height = 40,
                width = 40,
                overflow = Overflow.Visible,
            }
        };
        point.AddToClassList("parentElement");
        PointElement.MainPoint = point;

        Debug.LogError(PointElement.AnimationParamsArray.Length);
        foreach (var pointAnimation in PointElement.AnimationParamsArray)
        {
            Debug.Log(pointAnimation);
            VisualElement pointElement = new VisualElement
            {
                pickingMode = PickingMode.Ignore
            };
            pointElement.AddToClassList("hintPoint");
            Debug.Log(pointElement);
            pointAnimation.Point = pointElement;
            PointElement.MainPoint.Add(pointElement);
            
            pointElement.RegisterCallback<GeometryChangedEvent>(evt => StartAnimation(pointAnimation));
        }
        
        UIDocument.rootVisualElement.Add(PointElement.MainPoint);
        PointElement.MainPoint.clicked += OnClick;
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
        PointElement.MainPoint.style.display = (isActive) ? DisplayStyle.Flex : DisplayStyle.None;
    }
    
       public void StartAnimation(AnimationParams pointElement)
    {
        pointElement.Point.schedule.Execute(evt => Animation(pointElement)).Every(16);
        pointElement.Point.UnregisterCallback<GeometryChangedEvent>(evt => StartAnimation(pointElement));
    }

    public override void Animation(AnimationParams animationParams)
    {
        if (animationParams.AnimationIndex >= animationParams.TargetIndex)
        {
            animationParams.AnimationIndex = animationParams.StartIndex;
        }

        animationParams.AnimationIndex += Time.deltaTime * animationParams.Speed;
        float value = Mathf.Clamp01(animationParams.AnimationIndex / animationParams.TargetIndex);
        float smoothValue = Mathf.SmoothStep(0f, 1f, value);
        
        animationParams.Point.style.scale = new Scale(new Vector2(smoothValue, smoothValue));
        
        float thicknessPercentage = GetProgressInPercent(smoothValue, animationParams.StartIndex, animationParams.TargetIndex);
        float newThickness = CalculateThickness(thicknessPercentage, animationParams.MinThickness, animationParams.MaxThickness);
        animationParams.Point.style.opacity = CalculateThickness(thicknessPercentage, animationParams.MinOpacity, animationParams.MaxOpacity);
        SetBorderScale(newThickness, animationParams.Point);
    }

    private void SetBorderScale(float scale, VisualElement element)
    {
        element.style.borderBottomWidth = scale;
        element.style.borderTopWidth = scale;
        element.style.borderLeftWidth = scale;
        element.style.borderRightWidth = scale;
    }
    
    private float GetProgressInPercent(float currentValue, float minValue, float maxValue)
    {
        return Mathf.InverseLerp(minValue, maxValue, currentValue) * 100f;
    }
    
    private float CalculateThickness(float percentage, float minThickness, float maxThickness)
    {
        return percentage < 50f ? Mathf.Lerp(minThickness, maxThickness, percentage / 50f) : Mathf.Lerp(maxThickness, minThickness, (percentage - 50f) / 50f);
    }
}
