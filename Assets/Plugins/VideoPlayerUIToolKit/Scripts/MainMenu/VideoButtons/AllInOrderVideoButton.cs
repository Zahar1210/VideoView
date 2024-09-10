using UnityEngine;
using UnityEngine.UIElements;
using VideoPlayerAsset.UI;
using VideoPlayerAsset.Video;

public class AllInOrderVideoButton : MonoBehaviour
{
    [SerializeField] private VideoButton _videoController;
    [SerializeField] private VideoPlayerActivity _videoPlayerActivity;
    private int _i;
    
    public void Initialize(VisualElement root, VideoControlsUI videoControlsUI, VideoPlayerActivity videoPlayerActivity) 
    {
        Button allInOrderButton = root.Q<Button>("OrderButton");
        allInOrderButton.clicked += () => OnAllInOrderClick(videoControlsUI, videoPlayerActivity);
    }

    public void OnAllInOrderClick(VideoControlsUI videoControlsUI, VideoPlayerActivity videoPlayerActivity)
    {
        videoControlsUI.InOrder = true;
        if (_videoController.VideoData.Length > _i)
        {
            videoControlsUI.ChangeCurrentVideoController(_videoController.VideoData[_i].Video);
            videoPlayerActivity.SetActive(true);
            _i++;
        }
        else if(videoControlsUI.InOrder)
        {
            _i = 0;
            videoControlsUI.InOrder = false;
            _videoPlayerActivity.SetActive(false);
        }
    }
}
