using DG.Tweening;
using TMPro;
using UnityEngine;

public class EggCounterUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text _eggCounterText;//TextMeshPro yazý alaný, yumurta sayýsýný gösterecek

    [Header("Color")]//Tüm yumurtalar toplandýðýnda:
    [SerializeField] private Color _eggCounterColor;//Yazýnýn rengi bu renge dönüþür.
    [SerializeField] private float _colorDuration;
    [SerializeField] private float _scaleDuration;//Ölçek (scale) animasyonu yapýlýr.

    private RectTransform _eggCounterRectTransform;//UI üzerindeki yazýnýn pozisyonu, ölçeði gibi bilgileri tutar.

      private void Awake()
    {
        _eggCounterRectTransform = _eggCounterText.gameObject.GetComponent<RectTransform>();//TextMeshPro nesnesinin RectTransform bileþenini alýr.
    }//Bu sayede yazýnýn ölçeðini deðiþtirmek mümkün olur.

      public void SetEggCounterText(int counter,int max)
    {//Toplanan yumurta sayýsýný x / max formatýnda ekrana yazar.
        _eggCounterText.text = counter.ToString() + "/" + max.ToString();
    }

    public void SetEggCompleted()
    {
        _eggCounterText.DOColor(_eggCounterColor, _colorDuration);
        _eggCounterRectTransform.DOScale(1.2f, _scaleDuration).SetEase(Ease.OutBack);
    }
}
//DOTween kullanýlarak:Yazýnýn rengi yumuþak bir geçiþle _eggCounterColor’a dönüþür.
//Yazý büyütülerek dikkat çekici hale gelir (Ease.OutBack = yay gibi esneyip yavaþça oturan animasyon).



