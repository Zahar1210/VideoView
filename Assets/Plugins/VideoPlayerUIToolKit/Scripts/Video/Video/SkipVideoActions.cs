using UnityEngine;
using VideoPlayerAsset.UI;

public class SkipVideoActions : MonoBehaviour
{
    [SerializeField] private KeyCode[] _keyCodes;
    [SerializeField] private VideoControlsUI _videoControlsUI;

    private void Update()
    {
        if (IsClick() && _videoControlsUI.CurrentVideo != null && _videoControlsUI.CurrentVideo.VideoPlayer.isPlaying)
        {
            _videoControlsUI.CurrentVideo.Scrub(_videoControlsUI.CurrentVideo.VideoPlayer.length - 0.1f);
        }
    }
    
    private bool IsClick()
    {
        foreach (var keyCode in _keyCodes) 
            if (Input.GetKeyDown(keyCode))
                return true;

        return false;
    }
}
