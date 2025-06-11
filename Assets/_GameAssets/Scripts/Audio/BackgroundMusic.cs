using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic Instance { get; private set; }//Bu s�n�f�n tek bir �rne�ini (instance) di�er s�n�flar�n kullanabilmesi i�in public static bir de�i�ken. Bu sayede BackgroundMusic.Instance ile her yerden eri�ilebilir.

     private AudioSource _audioSource;//Unity�nin ses oynatma bile�eni. 

    private void Awake() 
    {
        _audioSource = GetComponent<AudioSource>();//Bu GameObject'teki AudioSource bile�enini al�r.

        if (Instance != null)
        {
            Destroy(gameObject);//E�er bu s�n�f�n bir �rne�i zaten varsa, yeni olu�an bu nesneyi yok eder. B�ylece yaln�zca bir m�zik �al�c� olur.
        } 
        else
        {
            Instance = this;//�lk �rnekse, kendisini Instance olarak atar ve DontDestroyOnLoad ile sahne de�i�iminde yok edilmemesini sa�lar.
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void SetMusicMute(bool isMuted)
    {
        _audioSource.mute = isMuted;//M�zik sesi a��l�r veya kapat�l�r. true verilirse ses tamamen susturulur.
    }

    public void PlayBackgroundMusic(bool isMusicPlaying)
    {//E�er isMusicPlaying true ve �u an m�zik �alm�yorsa m�zik ba�lat�l�r.

        //E�er false ise m�zik durdurulur.
        if (isMusicPlaying && !_audioSource.isPlaying) _audioSource.Play();
        else if (!isMusicPlaying) _audioSource.Stop();
    }
}
