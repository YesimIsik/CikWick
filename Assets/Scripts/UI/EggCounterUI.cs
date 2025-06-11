using DG.Tweening;
using TMPro;
using UnityEngine;

public class EggCounterUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text _eggCounterText;//TextMeshPro yaz� alan�, yumurta say�s�n� g�sterecek

    [Header("Color")]//T�m yumurtalar topland���nda:
    [SerializeField] private Color _eggCounterColor;//Yaz�n�n rengi bu renge d�n���r.
    [SerializeField] private float _colorDuration;
    [SerializeField] private float _scaleDuration;//�l�ek (scale) animasyonu yap�l�r.

    private RectTransform _eggCounterRectTransform;//UI �zerindeki yaz�n�n pozisyonu, �l�e�i gibi bilgileri tutar.

      private void Awake()
    {
        _eggCounterRectTransform = _eggCounterText.gameObject.GetComponent<RectTransform>();//TextMeshPro nesnesinin RectTransform bile�enini al�r.
    }//Bu sayede yaz�n�n �l�e�ini de�i�tirmek m�mk�n olur.

      public void SetEggCounterText(int counter,int max)
    {//Toplanan yumurta say�s�n� x / max format�nda ekrana yazar.
        _eggCounterText.text = counter.ToString() + "/" + max.ToString();
    }

    public void SetEggCompleted()
    {
        _eggCounterText.DOColor(_eggCounterColor, _colorDuration);
        _eggCounterRectTransform.DOScale(1.2f, _scaleDuration).SetEase(Ease.OutBack);
    }
}
//DOTween kullan�larak:Yaz�n�n rengi yumu�ak bir ge�i�le _eggCounterColor�a d�n���r.
//Yaz� b�y�t�lerek dikkat �ekici hale gelir (Ease.OutBack = yay gibi esneyip yava��a oturan animasyon).



