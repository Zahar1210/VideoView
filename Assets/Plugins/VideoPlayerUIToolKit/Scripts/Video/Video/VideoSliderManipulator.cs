using UnityEngine;
using UnityEngine.UIElements;
using VideoPlayerAsset.Video;

namespace VideoPlayerAsset.UI
{
    public class VideoSliderManipulator : Manipulator
    {
        private bool _pointerDown;
        private bool _scrubbed;

        private VisualElement Root { get; set; }
        private VisualElement SliderArea => Root.Q<VisualElement>("SliderArea"); 
        private readonly VideoControlsUI _videoController;

        public VideoSliderManipulator(VideoControlsUI videoController, VisualElement root)
        {
            Root = root;
            
            _videoController = videoController;

            RegisterCallbacksOnTarget();
        }

        protected sealed override void RegisterCallbacksOnTarget()
        {
            SliderArea.Q("unity-drag-container").RegisterCallback<PointerUpEvent>(PointerUp);       // PointerUp is consumed by slider so it has to be registered here.
            Root.RegisterCallback<ChangeEvent<float>>(SliderValueChanged);
        }

        protected override void UnregisterCallbacksFromTarget()
        {
            SliderArea.Q("unity-drag-container").UnregisterCallback<PointerUpEvent>(PointerUp);
            Root.UnregisterCallback<ChangeEvent<float>>(SliderValueChanged);
        }
        
        public void PointerDown()
        {
            _scrubbed = true;
            _pointerDown = true;
        }

        private async void PointerUp(PointerUpEvent evt)
        {
            _scrubbed = false;
            await System.Threading.Tasks.Task.Delay(150);
            _pointerDown = false;
        }

        private void SliderValueChanged(ChangeEvent<float> evt)
        {
            if (!_scrubbed) 
                return;
            var scrubToTime = evt.newValue * _videoController.CurrentVideo.VideoPlayer.length;
            _videoController.CurrentVideo.Scrub(scrubToTime);
        }

        public void SliderProgress()
        {
            if (_pointerDown)
                return;

            var time = _videoController.CurrentVideo.VideoPlayer.time;
            Root.Q<Slider>("Slider_Progress").value = (float)time / (float)_videoController.CurrentVideo.VideoPlayer.length;
        }
    }
}