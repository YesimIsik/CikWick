using System;
using System.ComponentModel.Design.Serialization;
//Bu ad alaný genellikle tasarým zamanýnda kullanýlan bileþenleri içerir

public class Consts
//Consts adýnda bir genel (public) sýnýf tanýmlanýr.Bu sýnýf, sabit deðerleri (constants) tutmak için kullanýlýr.

{
    public struct PlayerAnimations
    //PlayerAnimations adýnda bir genel (public) yapý (struct) tanýmlanýr. Bu yapý, oyuncu animasyonlarýyla ilgili sabit deðerleri gruplamak için kullanýlýr.
    {
        public const string IS_MOVING = "IsMoving";
        //IS_MOVING adýnda bir sabit dize tanýmlanýr ve deðeri "IsMoving" olarak atanýr.Bu Animator Controller içinde tanýmlanan bir boolean parametrenin adýný temsil eder.
        public const string IS_JUMPING = "IsJumping";
        //IS_JUMPING adýnda bir sabit dize tanýmlanýr ve deðeri "IsJumping" olarak atanýr.Bu zýplama animasyonunu kontrol eden Animator parametresinin adýdýr.
        public const string IS_SLIDING = "IsSliding";
        //IS_SLIDING adýnda bir sabit dize tanýmlanýr ve deðeri "IsSliding" olarak atanýr.Bu kayma animasyonunu kontrol eden Animator parametresinin adýdýr.
        public const string IS_SLIDING_ACTIVE = "IsSlidingActive";
        //IS_SLIDIN_ACTIVE adýnda bir sabit dize tanýmlanýr ve deðeri "IsSlidingActive" olarak atanýr.Bu aktif kayma durumunu kontrol eden Animator parametresinin adýdýr.
    }

    public struct AppleTypes
    {
        public const string RED_APPLE = "RedApple";
        public const string YELLOW_APPLE = "YellowApple";
        public const string GREEN_APPLE = "GreenApple";

    }

}


