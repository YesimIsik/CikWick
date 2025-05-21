
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
        public const string IS_SLIDIN_ACTIVE = "IsSlidingActive";
        //IS_SLIDIN_ACTIVE adýnda bir sabit dize tanýmlanýr ve deðeri "IsSlidingActive" olarak atanýr.Bu aktif kayma durumunu kontrol eden Animator parametresinin adýdýr.
    }
    public struct OtherAnimations
    //Bu bir struct (yapý) tanýmýdýr. Sýnýfa benzer ama genellikle veri taþýyýcý ve sabit koleksiyonlar için kullanýlýr.
    //OtherAnimations: Yapýnýn adý. Animasyonlarla ilgili sabit deðerleri saklamak için kullanýlýyor.
    {
        public const string IS_SPATULA_JUMPING = "IsSpatulaJumping";
        //Bu bir sabit string deðeridir."IsSpatulaJumping": Unity Animator’daki bir Bool parametresi ismini temsil ediyor.
    }

    public struct AppleTypes
    //Elma türleri ile ilgili sabitleri barýndýrmak için tanýmlanmýþ bir yapý.
    {
        public const string RED_APPLE = "RedApple";
        //Bu sabit, kodda RedApple kelimesinin tekrar tekrar yazýlmasýný önler.
        //"RedApple": Bu deðer örneðin tag, name, type, ya da prefab seçimi gibi yerlerde kullanýlýyor.
        public const string GREEN_APPLE = "GreenApple";
        //Ayný mantýkla, yeþil elma nesnesi için tanýmlanmýþ bir sabit string’dir.
        public const string YELLOW_APPLE = "YellowApple";
        //Sarý elma nesnesi için string sabittir. Kodun diðer yerlerinde "YellowApple" yazmak yerine, AppleTypes.YELLOW_APPLE kullanýlýr.

    }
}
