using UnityEngine;
using UnityEngine.UIElements;
using VideoPlayerAsset.UI;
using VideoPlayerAsset.Video;

public class AllInOrderVideoButton : MonoBehaviour
{
    [SerializeField] private Video[] _videoController;
    private int _i;
    
    public void Initialize(VisualElement root, VideoControlsUI videoControlsUI, VideoPlayerActivity videoPlayerActivity) 
    {
        Button allInOrderButton = root.Q<Button>("OrderButton");
        allInOrderButton.clicked += () => OnAllInOrderClick(videoControlsUI, videoPlayerActivity);
    }

    public void OnAllInOrderClick(VideoControlsUI videoControlsUI, VideoPlayerActivity videoPlayerActivity)
    {
        videoControlsUI.InOrder = true;
        if (_videoController.Length > _i)
        {
            videoControlsUI.ChangeCurrentVideoController(_videoController[_i]);
            videoPlayerActivity.SetActive(true);
            _i++;
        }
        else
        {
            videoControlsUI.InOrder = false;
            _i = 0;
        }
    }
}
