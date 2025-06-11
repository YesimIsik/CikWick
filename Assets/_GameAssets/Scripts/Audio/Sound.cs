using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    [HideInInspector]//Unity'de sesi oynatmak i�in kullan�lan bile�en.
    public AudioSource Source;
    public AudioClip AudioClip;//Oynat�lacak ses dosyas� 
    public SoundType SoundType;//Bu sesin t�r�n� temsil eder 
    [Range(0f, 1f)] public float Volume;//Sesi ne kadar y�ksek oynataca��n� belirler. 
    [Range(.1f, 3f)] public float Pitch;
    //Range �zelli�i sayesinde Unity edit�r�nde s�rg�l� bir kontrol ile ayarlanabilir.


    public bool Mute;//Bu se�enek i�aretliyse ses �almaz
    public bool Loop;//E�er true ise ses sonsuz d�ng�de oynar.
    public bool playOnAwake;
}