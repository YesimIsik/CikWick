
using System;
using System.ComponentModel.Design.Serialization;
//Bu ad alan� genellikle tasar�m zaman�nda kullan�lan bile�enleri i�erir

public class Consts
//Consts ad�nda bir genel (public) s�n�f tan�mlan�r.Bu s�n�f, sabit de�erleri (constants) tutmak i�in kullan�l�r.

{
    public struct PlayerAnimations
    //PlayerAnimations ad�nda bir genel (public) yap� (struct) tan�mlan�r. Bu yap�, oyuncu animasyonlar�yla ilgili sabit de�erleri gruplamak i�in kullan�l�r.
    {
        public const string IS_MOVING = "IsMoving";
        //IS_MOVING ad�nda bir sabit dize tan�mlan�r ve de�eri "IsMoving" olarak atan�r.Bu Animator Controller i�inde tan�mlanan bir boolean parametrenin ad�n� temsil eder.
        public const string IS_JUMPING = "IsJumping";
        //IS_JUMPING ad�nda bir sabit dize tan�mlan�r ve de�eri "IsJumping" olarak atan�r.Bu z�plama animasyonunu kontrol eden Animator parametresinin ad�d�r.
        public const string IS_SLIDING = "IsSliding";
        //IS_SLIDING ad�nda bir sabit dize tan�mlan�r ve de�eri "IsSliding" olarak atan�r.Bu kayma animasyonunu kontrol eden Animator parametresinin ad�d�r.
        public const string IS_SLIDIN_ACTIVE = "IsSlidingActive";
        //IS_SLIDIN_ACTIVE ad�nda bir sabit dize tan�mlan�r ve de�eri "IsSlidingActive" olarak atan�r.Bu aktif kayma durumunu kontrol eden Animator parametresinin ad�d�r.
    }
    public struct OtherAnimations
    //Bu bir struct (yap�) tan�m�d�r. S�n�fa benzer ama genellikle veri ta��y�c� ve sabit koleksiyonlar i�in kullan�l�r.
    //OtherAnimations: Yap�n�n ad�. Animasyonlarla ilgili sabit de�erleri saklamak i�in kullan�l�yor.
    {
        public const string IS_SPATULA_JUMPING = "IsSpatulaJumping";
        //Bu bir sabit string de�eridir."IsSpatulaJumping": Unity Animator�daki bir Bool parametresi ismini temsil ediyor.
    }

    public struct AppleTypes
    //Elma t�rleri ile ilgili sabitleri bar�nd�rmak i�in tan�mlanm�� bir yap�.
    {
        public const string RED_APPLE = "RedApple";
        //Bu sabit, kodda RedApple kelimesinin tekrar tekrar yaz�lmas�n� �nler.
        //"RedApple": Bu de�er �rne�in tag, name, type, ya da prefab se�imi gibi yerlerde kullan�l�yor.
        public const string GREEN_APPLE = "GreenApple";
        //Ayn� mant�kla, ye�il elma nesnesi i�in tan�mlanm�� bir sabit string�dir.
        public const string YELLOW_APPLE = "YellowApple";
        //Sar� elma nesnesi i�in string sabittir. Kodun di�er yerlerinde "YellowApple" yazmak yerine, AppleTypes.YELLOW_APPLE kullan�l�r.

    }
}
