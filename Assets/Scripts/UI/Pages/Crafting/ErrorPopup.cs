using DG.Tweening;
using StairwayGamesTest.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StairwayGamesTest.UI.Pages.Crafting
{
    public class ErrorPopup : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI messageText;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Image image;

        private RectTransform _rectTransform;
        
        private Tween _popupPunchTween;
        private Tween _popupScaleTween;
        private Sequence _popupColorSequence;
        
        private Vector2 _initialPosition;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _initialPosition = _rectTransform.anchoredPosition;
        }

        public void ShowError(string message)
        {
            gameObject.SetActive(true);
            messageText.text = message;
            
            _popupPunchTween?.Kill();
            _popupColorSequence?.Kill();
            _popupScaleTween?.Kill();

            canvasGroup.alpha = 1f;
            
            _rectTransform.anchoredPosition = _initialPosition;
            image.color = new Color32(255, 138, 138, 255);

            _rectTransform.localScale = Vector2.one * 0.9f;

            _popupColorSequence = DOTween.Sequence();

            _popupScaleTween = _rectTransform.DOScale(Vector2.one, 0.2f);
            _popupPunchTween = _rectTransform.DOPunchPosition(Vector2.right * 15f, 0.4f);
            _popupColorSequence.Append(image.DOColor(Color.white, 0.5f));
            _popupColorSequence.AppendInterval(1f);
            _popupColorSequence.Append(canvasGroup.DOFade(0f, 0.5f));
            _popupColorSequence.AppendCallback(() => gameObject.SetActive(false));
            
            AudioEngine.PlaySfx("rollover6");
        }
    }
}