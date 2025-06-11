using UnityEngine;

public class CatStateController : MonoBehaviour
{
    [SerializeField] private CatState _currentCatState = CatState.Walking;//Kedinin þu anki durumunu tutar.Baþlangýç deðerini Walking (yürüyor) olarak ayarlamamýzý saðlar.

    private void Start()
    {
        ChangeState(CatState.Walking);//Baþlangýçta kedinin durumunu Walking olarak ayarlar.
    }


    public void ChangeState(CatState newState)//Dýþarýdan çaðrýlarak kedinin durumunu deðiþtirmek için kullanýlan fonksiyondur.
    {
        if (_currentCatState== newState) { return; }//Eðer zaten istenen durumdaysa, hiçbir þey yapmadan çýkar.
        _currentCatState = newState;//Þu anda baþka bir þey yapmýyor, ama ileride bu kýsma animasyon tetikleme, ses oynatma, hýz deðiþtirme gibi iþlemler eklenebilir.


    }

    public CatState GetCurrentState()
    {
        return _currentCatState;//Þu anki durumu (state) döndüren fonksiyondur.


    }
}
