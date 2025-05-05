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
        public const string IS_SLIDING_ACTIVE = "IsSlidingActive";
        //IS_SLIDIN_ACTIVE ad�nda bir sabit dize tan�mlan�r ve de�eri "IsSlidingActive" olarak atan�r.Bu aktif kayma durumunu kontrol eden Animator parametresinin ad�d�r.
    }

    public struct AppleTypes
    {
        public const string RED_APPLE = "RedApple";
        public const string YELLOW_APPLE = "YellowApple";
        public const string GREEN_APPLE = "GreenApple";

    }

}


