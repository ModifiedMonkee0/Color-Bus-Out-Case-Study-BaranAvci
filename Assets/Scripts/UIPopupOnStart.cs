using UnityEngine;
using DG.Tweening;

public class UIPopupOnStart : MonoBehaviour
{
    public RectTransform uiElement; // animasyon yapýlacak UI

    void Start()
    {
        // Baþta görünmez yap (isteðe baðlý)
        uiElement.localScale = Vector3.zero;

        // POP efektini yap: 0 ? 1.2x ? 1x
        uiElement.DOScale(1.2f, 0.3f)
                 .SetEase(Ease.OutBack)
                 .OnComplete(() => {
                     uiElement.DOScale(1f, 0.2f).SetEase(Ease.InOutSine);
                 });
    }
}
