using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic Instance { get; private set; }//Bu sýnýfýn tek bir örneðini (instance) diðer sýnýflarýn kullanabilmesi için public static bir deðiþken. Bu sayede BackgroundMusic.Instance ile her yerden eriþilebilir.

     private AudioSource _audioSource;//Unity’nin ses oynatma bileþeni. 

    private void Awake() 
    {
        _audioSource = GetComponent<AudioSource>();//Bu GameObject'teki AudioSource bileþenini alýr.

        if (Instance != null)
        {
            Destroy(gameObject);//Eðer bu sýnýfýn bir örneði zaten varsa, yeni oluþan bu nesneyi yok eder. Böylece yalnýzca bir müzik çalýcý olur.
        } 
        else
        {
            Instance = this;//Ýlk örnekse, kendisini Instance olarak atar ve DontDestroyOnLoad ile sahne deðiþiminde yok edilmemesini saðlar.
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void SetMusicMute(bool isMuted)
    {
        _audioSource.mute = isMuted;//Müzik sesi açýlýr veya kapatýlýr. true verilirse ses tamamen susturulur.
    }

    public void PlayBackgroundMusic(bool isMusicPlaying)
    {//Eðer isMusicPlaying true ve þu an müzik çalmýyorsa müzik baþlatýlýr.

        //Eðer false ise müzik durdurulur.
        if (isMusicPlaying && !_audioSource.isPlaying) _audioSource.Play();
        else if (!isMusicPlaying) _audioSource.Stop();
    }
}
