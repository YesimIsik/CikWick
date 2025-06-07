using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverController : MonoBehaviour, IPointerEnterHandler
{
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Instance.Play(SoundType.ButtonHoverSound);
    }
}
