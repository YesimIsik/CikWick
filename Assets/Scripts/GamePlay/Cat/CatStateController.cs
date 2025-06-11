using UnityEngine;

public class CatStateController : MonoBehaviour
{
    [SerializeField] private CatState _currentCatState = CatState.Walking;//Kedinin �u anki durumunu tutar.Ba�lang�� de�erini Walking (y�r�yor) olarak ayarlamam�z� sa�lar.

    private void Start()
    {
        ChangeState(CatState.Walking);//Ba�lang��ta kedinin durumunu Walking olarak ayarlar.
    }


    public void ChangeState(CatState newState)//D��ar�dan �a�r�larak kedinin durumunu de�i�tirmek i�in kullan�lan fonksiyondur.
    {
        if (_currentCatState== newState) { return; }//E�er zaten istenen durumdaysa, hi�bir �ey yapmadan ��kar.
        _currentCatState = newState;//�u anda ba�ka bir �ey yapm�yor, ama ileride bu k�sma animasyon tetikleme, ses oynatma, h�z de�i�tirme gibi i�lemler eklenebilir.


    }

    public CatState GetCurrentState()
    {
        return _currentCatState;//�u anki durumu (state) d�nd�ren fonksiyondur.


    }
}
