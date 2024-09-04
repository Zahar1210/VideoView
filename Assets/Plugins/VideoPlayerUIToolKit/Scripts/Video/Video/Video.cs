using System;
using UnityEngine;
using UnityEngine.Video;
using VideoPlayerAsset.UI;

namespace VideoPlayerAsset.Video
{
    [RequireComponent(typeof(VideoPlayer))]
    public class Video : MonoBehaviour
    {
        [SerializeField] private VideoRenderTextureUI _videoRenderTextureUI;
        [SerializeField] private VideoPlayerActivity _videoPlayerActivity;
        [SerializeField] private VideoControlsUI _videoControlsUI;
        [SerializeField] private AllInOrderVideoButton _allInOrderVideoButton;
        [SerializeField] private VideoClip _videoClip;

        public VideoPlayer VideoPlayer => GetComponent<VideoPlayer>(); 

        public void OnEnable()
        {
            Debug.LogWarning("l;f,wrmfklmwlk;rmlkw");
            PrepareClip(_videoClip);
        }
        
        public void PlayVideo()
        {
            VideoPlayer.Play();
        }

        public void StopVideo()
        {
            VideoPlayer.Stop();
        }

        public void PauseVideo()
        {
            VideoPlayer.Pause();
        }

        public void LoopVideo(bool loop)
        {
            VideoPlayer.isLooping = loop;
        }
        
        public void PrepareClip(VideoClip clip)
        {
            VideoPlayer.url = null;  // Ensure VideoPlayer is set to play via VideoClip and not URL
            VideoPlayer.clip = clip;

            VideoPlayer.prepareCompleted += OnVideoPrepared;
            VideoPlayer.errorReceived += OnVideoError;
            VideoPlayer.loopPointReached += OnVideoEnd;
            VideoPlayer.Prepare();
        }

        private void OnVideoPrepared(VideoPlayer videoPlayer)
        {
            UnsubscribeVideoEvents(videoPlayer);
            
            var renderTexture = new RenderTexture((int)videoPlayer.width, (int)videoPlayer.height, 24);
            videoPlayer.targetTexture = renderTexture;
            _videoRenderTextureUI.SetRenderTexture(renderTexture);
            
            videoPlayer.Play();
            videoPlayer.Pause();
        }

        private void OnVideoError(VideoPlayer vp, string message)
        {
            Debug.LogError("VideoPlayer Error: " + message);
            UnsubscribeVideoEvents(vp);
        }

        public void OnVideoEnd(VideoPlayer videoPlayer)
        {
            if (_videoControlsUI.InOrder)
            {
                _allInOrderVideoButton.OnAllInOrderClick(_videoControlsUI, _videoPlayerActivity);
                return;
            }
            _videoPlayerActivity.SetActive(false);
        }

        private void UnsubscribeVideoEvents(VideoPlayer vp)
        {
            vp.prepareCompleted -= OnVideoPrepared;
            vp.errorReceived -= OnVideoError;
        }

        public void Mute(bool mute)
        {
            VideoPlayer.SetDirectAudioMute(0, mute);
        }

        public void Scrub(double time)
        {
            if (VideoPlayer.isLooping)
            {
                VideoPlayer.Stop();
                VideoPlayer.time = time;
                VideoPlayer.Play();
            }
            else
            {
                VideoPlayer.time = time;
            }
        }
    }
}