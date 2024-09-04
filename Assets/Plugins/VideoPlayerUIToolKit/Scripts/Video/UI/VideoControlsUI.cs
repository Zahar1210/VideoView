using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Video;

namespace VideoPlayerAsset.UI
{
    [RequireComponent(typeof(UIDocument))]
    public class VideoControlsUI : MonoBehaviour
    {
        public Video.Video CurrentVideo { get; private set; }
        public bool InOrder { get; set; }
        
        [SerializeField] private Video.Video _startVideo;

        [SerializeField] private LoopNoLoopButton _loopNoLoopButton;
        [SerializeField] private MuteSoundButton _soundButton;
        [SerializeField] private StopPlayButton _stopPlayButton;
        [SerializeField] private VideoSlider _videoSlider;

        
        private void OnEnable()
        {
            VisualElement root = gameObject.GetComponent<UIDocument>().rootVisualElement;
            _videoSlider.Initialize(root);
            
            // ChangeCurrentVideoController(_startVideo);
            
            _loopNoLoopButton.Initialize(root, this);
            _soundButton.Initialize(root, this);
            _stopPlayButton.Initialize(root, this);
        }
        
        public void ChangeCurrentVideoController(Video.Video video)
        {
            // Убедитесь, что текущий видеоплеер остановлен и очищен
            if (CurrentVideo != null)
            {
                CurrentVideo.StopVideo();
                if (CurrentVideo != null)
                {
                    CurrentVideo.VideoPlayer.loopPointReached -= _videoSlider.VideoLoopPointReached;
                
                    _videoSlider.Root.Q<VisualElement>("SliderArea").UnregisterCallback<PointerDownEvent>(_videoSlider.SliderPointerDown, TrickleDown.TrickleDown);
                    _videoSlider.Root.Q<Slider>("Slider_Progress").RemoveManipulator(_videoSlider.VideoSliderManipulator);
                }
                CurrentVideo.VideoPlayer.prepareCompleted -= OnVideoPrepared;
                CurrentVideo?.gameObject.SetActive(false);
            }

            // Активируем новый видеоконтроллер
            CurrentVideo = video;
            CurrentVideo?.gameObject.SetActive(true);

            // Подготовка нового видео
            if (CurrentVideo != null)
            {
                Debug.LogError("инициализируем новую");
                CurrentVideo.VideoPlayer.Prepare();
                CurrentVideo.VideoPlayer.prepareCompleted += OnVideoPrepared;
            }

            if (CurrentVideo != null)
            {
                CurrentVideo.VideoPlayer.loopPointReached += _videoSlider.VideoLoopPointReached;
                
                _videoSlider.Root.Q<VisualElement>("SliderArea").RegisterCallback<PointerDownEvent>(_videoSlider.SliderPointerDown, TrickleDown.TrickleDown);
                _videoSlider.Root.Q<Slider>("Slider_Progress").RemoveManipulator(_videoSlider.VideoSliderManipulator);
            }
        }
        
        private void OnVideoPrepared(VideoPlayer vp)
        {
            CurrentVideo?.PlayVideo();
        }
    }
}