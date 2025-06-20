using UnityEngine;
using DG.Tweening;

public class UIPopupOnStart : MonoBehaviour
{
    public RectTransform uiElement; // animasyon yap�lacak UI

    void Start()
    {
        // Ba�ta g�r�nmez yap (iste�e ba�l�)
        uiElement.localScale = Vector3.zero;

        // POP efektini yap: 0 ? 1.2x ? 1x
        uiElement.DOScale(1.2f, 0.3f)
                 .SetEase(Ease.OutBack)
                 .OnComplete(() => {
                     uiElement.DOScale(1f, 0.2f).SetEase(Ease.InOutSine);
                 });
    }
}
