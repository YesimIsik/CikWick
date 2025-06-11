using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    //Singleton yap�s�yla tek bir �rnek olu�turulur.

   // Di�er s�n�flar bu �rne�e AudioManager.Instance ile eri�ebilir.
    
        [Header("Sounds")]
    public Sound[] Sounds;//Her Sound nesnesi: ses klibi, ses tipi, ses seviyesi, loop gibi ayarlar� i�erir 

    private void Awake() //Oyun ba�lad���nda AudioSource bile�enleri dinamik olarak eklenir ve yap�land�r�l�r.
    {
        Instance = this;//Singleton �rne�i olarak kendini atar.

        foreach (Sound s in Sounds)//Her ses i�in yeni bir AudioSource bile�eni eklenir ve yap�land�r�l�r.
                                   //Bu sayede her ses ba��ms�z bir �ekilde oynat�labilir.
        {
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.AudioClip;
            s.Source.volume = s.Volume;
            s.Source.pitch = s.Pitch;
            s.Source.mute = s.Mute;
            s.Source.loop = s.Loop; 
            s.Source.playOnAwake = s.playOnAwake;
        }
    }

    public void Play(SoundType soundType)//Belirli bir SoundType t�r�ndeki sesi �alar.
    {
        Sound s = Array.Find(Sounds, sound => sound.SoundType == soundType);//Dizide uygun ses nesnesi aran�r.
        if (s == null)//Ses bulunamazsa uyar� verilir.
        {
            Debug.LogWarning($"Sound with type {soundType} not found in AudioManager.");
            return;
        }

    
        s.Source.Play();//Ses �al�n�r.
    }

    public void Stop(SoundType soundType)//Belirli bir sesi durdurur
    {
        Sound s = Array.Find(Sounds, sound => sound.SoundType == soundType);
        if (s == null)

          
        {
            Debug.LogWarning($"Sound with type {soundType} not found in AudioManager.");
            return;
        }
        //Belirli bir sesi durdurur (�rne�in: loop eden arka plan m�zi�i).
        s.Source.Stop();
    }

    public void SetSoundEffectsMute(bool isMuted)
    {
        foreach (Sound s in Sounds)
        {
            s.Source.mute = isMuted;
        }
    }////T�m ses kaynaklar�n� sessize al�r veya sesi a�ar.

    // Kullan�c� ayarlar�nda ses efektlerini a� / kapa yapmak i�in kullan�l�r.
}
