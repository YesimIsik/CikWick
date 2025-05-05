using UnityEngine;

public class StateController : MonoBehaviour
{
    private PlayerState _currentPlayerState = PlayerState.Idle;
    //Oyuncunun mevcut durumunu tutar ve ba�lang��ta ki Idle (bo�ta) olarak ayarlanm��t�r.
    private void Start()
    //Unity'de bir nesne etkinle�tirildi�inde ilk �a�r�lan metottur.
    {
        ChangeState(PlayerState.Idle);
        //Bu kodda, oyuncunun ba�lang�� durumu Idle olarak ayarlan�r.
    }
    public void ChangeState(PlayerState newPlayerState)
    //Oyuncunun durumunu de�i�tirmek i�in kullan�l�r.
    {

        if (_currentPlayerState == newPlayerState)
        //Yeni durum, mevcut durumla ayn�ysa i�lem yap�lmaz.
        {
            return;
        }
        _currentPlayerState = newPlayerState;
        //Farkl�ysa (_currentPlayerState) g�ncellenir.
    }

    public PlayerState GetCurrentState()
    //Mevcut oyuncu durumunu d�nd�r�r
    {
        return _currentPlayerState;
        //Bu, di�er s�n�flar�n oyuncunun durumunu kontrol etmesine olanak tan�r.
    }
} 