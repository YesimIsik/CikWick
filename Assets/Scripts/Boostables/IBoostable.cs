public interface IBoostable //Bu arayüzün adý IBoostable. Ýsmin baþýndaki I harfi, C#’ta genellikle bir arayüz olduðunu belirtmek için kullanýlýr.
                            //IBoostable demek, bu arayüzü uygulayan (implement eden) bir sýnýfýn “boost”lanabilir (güçlendirilebilir) bir þey olduðunu belirtir.
{
    void Boost(PlayerController playerController);// Metot, iþlem yapýlacak olan oyuncuyu temsil eden bir PlayerController nesnesi alýr.
}
