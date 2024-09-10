using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace VideoPlayerAsset.UI
{
    public class VideoRenderTextureUI : MonoBehaviour
    {
        [SerializeField] private UIDocument _uiDocument;
        private VisualElement VideoVe;
        
        private void OnEnable()
        {
            VideoVe = _uiDocument.rootVisualElement.Q<VisualElement>("VideoPlayer").Q<VisualElement>("Video");
        }

        public void SetRenderTexture(RenderTexture renderTexture)
        {
            var videoPlayerVe = VideoVe.style.backgroundImage.value;
            videoPlayerVe.renderTexture = renderTexture;
            VideoVe.style.backgroundImage = videoPlayerVe;
        }
    }
}