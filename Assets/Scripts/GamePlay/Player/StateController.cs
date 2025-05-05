using UnityEngine;

public class StateController : MonoBehaviour
{
    private PlayerState _currentPlayerState = PlayerState.Idle;
    //Oyuncunun mevcut durumunu tutar ve baþlangýçta ki Idle (boþta) olarak ayarlanmýþtýr.
    private void Start()
    //Unity'de bir nesne etkinleþtirildiðinde ilk çaðrýlan metottur.
    {
        ChangeState(PlayerState.Idle);
        //Bu kodda, oyuncunun baþlangýç durumu Idle olarak ayarlanýr.
    }
    public void ChangeState(PlayerState newPlayerState)
    //Oyuncunun durumunu deðiþtirmek için kullanýlýr.
    {

        if (_currentPlayerState == newPlayerState)
        //Yeni durum, mevcut durumla aynýysa iþlem yapýlmaz.
        {
            return;
        }
        _currentPlayerState = newPlayerState;
        //Farklýysa (_currentPlayerState) güncellenir.
    }

    public PlayerState GetCurrentState()
    //Mevcut oyuncu durumunu döndürür
    {
        return _currentPlayerState;
        //Bu, diðer sýnýflarýn oyuncunun durumunu kontrol etmesine olanak tanýr.
    }
} 