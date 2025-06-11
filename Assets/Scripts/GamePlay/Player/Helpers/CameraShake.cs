using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }
    // Singleton desenine uygun olarak, bu sýnýfýn global bir örneðini tutar.Instance sayesinde diðer scriptler kolayca bu sýnýfa eriþebilir:


    private CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin;//Cinemachine kameranýn gürültü (noise) efektini kontrol eden bileþeni tutar.  Bu bileþen sayesinde titreme efekti oluþturulabilir.
    private float _shakeTimer;//Titreme süresi geri sayým için kullanýlýr.
    private float _shakeTimerTotal;//Toplam titreme süresi (baþlangýçta atanýr)
    private float _startingIntensity;// Baþlangýç þiddetini saklar; titremenin sonunda sýfýra düþürülmek için kullanýlýr.

    private void Awake()//Unity tarafýndan sahne yüklendiðinde otomatik olarak çaðrýlýr.
    {
        Instance = this;//Singleton kurulumunu yapar. Instance deðiþkeni bu scriptin çalýþtýðý objeyi iþaret eder.



        _cinemachineBasicMultiChannelPerlin = GetComponent<CinemachineBasicMultiChannelPerlin>();//Ayný GameObject’te bulunan CinemachineBasicMultiChannelPerlin bileþenini alýr.
    }


    private IEnumerator CameraShakeCoroutine(float intensity, float time, float delay)//delay: Efektin baþlamasýndan önceki bekleme süresi.
    {
        yield return new WaitForSeconds(delay);//Belirtilen süre kadar beklenir. Böylece gecikmeli bir titreme baþlatýlabilir.
        _cinemachineBasicMultiChannelPerlin.AmplitudeGain = intensity;//Titremenin þiddeti AmplitudeGain ile ayarlanýr.
        _shakeTimer = time;
        _shakeTimerTotal = time;
        _startingIntensity = intensity;
        //Gerekli zamanlayýcýlar ve baþlangýç þiddeti ayarlanýr. Bu deðerler Update() metodunda titremenin bitiþini kontrol etmek için kullanýlýr.
    }
    public void ShakeCamera(float intensity, float time, float delay = 0f)//Baþka scriptlerin çaðýrabileceði bir fonksiyondur.
    {
        StartCoroutine(CameraShakeCoroutine(intensity, time, delay));//Belirtilen parametrelerle coroutine baþlatýlýr ve kamera titremeye baþlar.
    }


    private void Update()
    {
        if (_shakeTimer > 0f)//Eðer bir titreme aktifse (_shakeTimer sýfýrdan büyükse), süre azaltýlýr.
        {
            _shakeTimer -= Time.deltaTime;//Zamanlayýcýyý frame süresine göre azaltýr.



            if (_shakeTimer <= 0f)//Titreme süresi bittiðinde bu blok çalýþýr.
            {
                _cinemachineBasicMultiChannelPerlin.AmplitudeGain =
                    Mathf.Lerp(_startingIntensity, 0f, 1 - (_shakeTimer / _shakeTimerTotal));
                //Titreme bittikten sonra AmplitudeGain deðeri sýfýra düþürülür.
            }
        }
    }
}
