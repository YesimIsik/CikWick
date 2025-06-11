using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    //Singleton yapýsýyla tek bir örnek oluþturulur.

   // Diðer sýnýflar bu örneðe AudioManager.Instance ile eriþebilir.
    
        [Header("Sounds")]
    public Sound[] Sounds;//Her Sound nesnesi: ses klibi, ses tipi, ses seviyesi, loop gibi ayarlarý içerir 

    private void Awake() //Oyun baþladýðýnda AudioSource bileþenleri dinamik olarak eklenir ve yapýlandýrýlýr.
    {
        Instance = this;//Singleton örneði olarak kendini atar.

        foreach (Sound s in Sounds)//Her ses için yeni bir AudioSource bileþeni eklenir ve yapýlandýrýlýr.
                                   //Bu sayede her ses baðýmsýz bir þekilde oynatýlabilir.
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

    public void Play(SoundType soundType)//Belirli bir SoundType türündeki sesi çalar.
    {
        Sound s = Array.Find(Sounds, sound => sound.SoundType == soundType);//Dizide uygun ses nesnesi aranýr.
        if (s == null)//Ses bulunamazsa uyarý verilir.
        {
            Debug.LogWarning($"Sound with type {soundType} not found in AudioManager.");
            return;
        }

    
        s.Source.Play();//Ses çalýnýr.
    }

    public void Stop(SoundType soundType)//Belirli bir sesi durdurur
    {
        Sound s = Array.Find(Sounds, sound => sound.SoundType == soundType);
        if (s == null)

          
        {
            Debug.LogWarning($"Sound with type {soundType} not found in AudioManager.");
            return;
        }
        //Belirli bir sesi durdurur (örneðin: loop eden arka plan müziði).
        s.Source.Stop();
    }

    public void SetSoundEffectsMute(bool isMuted)
    {
        foreach (Sound s in Sounds)
        {
            s.Source.mute = isMuted;
        }
    }////Tüm ses kaynaklarýný sessize alýr veya sesi açar.

    // Kullanýcý ayarlarýnda ses efektlerini aç / kapa yapmak için kullanýlýr.
}
