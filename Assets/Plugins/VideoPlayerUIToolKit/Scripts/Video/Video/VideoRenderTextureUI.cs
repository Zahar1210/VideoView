using UnityEngine;
using UnityEngine.UIElements;

namespace VideoPlayerAsset.UI
{
    public class VideoRenderTextureUI : MonoBehaviour
    {
        [SerializeField] private UIDocument _uiDocument;
        private VisualElement VideoVe
        {
            get
            {
                return _videoVe ??= _uiDocument.rootVisualElement.Q<VisualElement>("VideoPlayer")
                    .Q<VisualElement>("Video");
            }
        }
        private VisualElement _videoVe;
        
        public void SetRenderTexture(RenderTexture renderTexture)
        {
            Debug.LogError("set");
            var videoPlayerVe = VideoVe.style.backgroundImage.value;
            videoPlayerVe.renderTexture = renderTexture;
            VideoVe.style.backgroundImage = videoPlayerVe;
        }
    }
}