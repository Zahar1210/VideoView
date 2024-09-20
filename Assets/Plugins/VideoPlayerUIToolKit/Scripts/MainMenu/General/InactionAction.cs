using UnityEngine;
using VideoPlayerAsset.UI;

public class InactionAction : MonoBehaviour
{
    [SerializeField] private VideoControlsUI _videoControlsUI;
    [SerializeField] private ViewModeActivity _viewModeActivity;
    [SerializeField] private VideoModeActivity _videoModeActivity;
    
    public void OnInaction()
    {
        _videoModeActivity.SetActive(true);
        _videoControlsUI.OnStart();

        if (_viewModeActivity.IsViewModeActive)
        {
            _viewModeActivity.SetActive(false);
        }
    }
}
