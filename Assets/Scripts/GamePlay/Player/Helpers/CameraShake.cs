using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }
    // Singleton desenine uygun olarak, bu s�n�f�n global bir �rne�ini tutar.Instance sayesinde di�er scriptler kolayca bu s�n�fa eri�ebilir:


    private CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin;//Cinemachine kameran�n g�r�lt� (noise) efektini kontrol eden bile�eni tutar.  Bu bile�en sayesinde titreme efekti olu�turulabilir.
    private float _shakeTimer;//Titreme s�resi geri say�m i�in kullan�l�r.
    private float _shakeTimerTotal;//Toplam titreme s�resi (ba�lang��ta atan�r)
    private float _startingIntensity;// Ba�lang�� �iddetini saklar; titremenin sonunda s�f�ra d���r�lmek i�in kullan�l�r.

    private void Awake()//Unity taraf�ndan sahne y�klendi�inde otomatik olarak �a�r�l�r.
    {
        Instance = this;//Singleton kurulumunu yapar. Instance de�i�keni bu scriptin �al��t��� objeyi i�aret eder.



        _cinemachineBasicMultiChannelPerlin = GetComponent<CinemachineBasicMultiChannelPerlin>();//Ayn� GameObject�te bulunan CinemachineBasicMultiChannelPerlin bile�enini al�r.
    }


    private IEnumerator CameraShakeCoroutine(float intensity, float time, float delay)//delay: Efektin ba�lamas�ndan �nceki bekleme s�resi.
    {
        yield return new WaitForSeconds(delay);//Belirtilen s�re kadar beklenir. B�ylece gecikmeli bir titreme ba�lat�labilir.
        _cinemachineBasicMultiChannelPerlin.AmplitudeGain = intensity;//Titremenin �iddeti AmplitudeGain ile ayarlan�r.
        _shakeTimer = time;
        _shakeTimerTotal = time;
        _startingIntensity = intensity;
        //Gerekli zamanlay�c�lar ve ba�lang�� �iddeti ayarlan�r. Bu de�erler Update() metodunda titremenin biti�ini kontrol etmek i�in kullan�l�r.
    }
    public void ShakeCamera(float intensity, float time, float delay = 0f)//Ba�ka scriptlerin �a��rabilece�i bir fonksiyondur.
    {
        StartCoroutine(CameraShakeCoroutine(intensity, time, delay));//Belirtilen parametrelerle coroutine ba�lat�l�r ve kamera titremeye ba�lar.
    }


    private void Update()
    {
        if (_shakeTimer > 0f)//E�er bir titreme aktifse (_shakeTimer s�f�rdan b�y�kse), s�re azalt�l�r.
        {
            _shakeTimer -= Time.deltaTime;//Zamanlay�c�y� frame s�resine g�re azalt�r.



            if (_shakeTimer <= 0f)//Titreme s�resi bitti�inde bu blok �al���r.
            {
                _cinemachineBasicMultiChannelPerlin.AmplitudeGain =
                    Mathf.Lerp(_startingIntensity, 0f, 1 - (_shakeTimer / _shakeTimerTotal));
                //Titreme bittikten sonra AmplitudeGain de�eri s�f�ra d���r�l�r.
            }
        }
    }
}
