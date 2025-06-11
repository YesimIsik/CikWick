using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    [HideInInspector]//Unity'de sesi oynatmak için kullanýlan bileþen.
    public AudioSource Source;
    public AudioClip AudioClip;//Oynatýlacak ses dosyasý 
    public SoundType SoundType;//Bu sesin türünü temsil eder 
    [Range(0f, 1f)] public float Volume;//Sesi ne kadar yüksek oynatacaðýný belirler. 
    [Range(.1f, 3f)] public float Pitch;
    //Range özelliði sayesinde Unity editöründe sürgülü bir kontrol ile ayarlanabilir.


    public bool Mute;//Bu seçenek iþaretliyse ses çalmaz
    public bool Loop;//Eðer true ise ses sonsuz döngüde oynar.
    public bool playOnAwake;
}