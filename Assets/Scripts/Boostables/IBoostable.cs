public interface IBoostable //Bu aray�z�n ad� IBoostable. �smin ba��ndaki I harfi, C#�ta genellikle bir aray�z oldu�unu belirtmek i�in kullan�l�r.
                            //IBoostable demek, bu aray�z� uygulayan (implement eden) bir s�n�f�n �boost�lanabilir (g��lendirilebilir) bir �ey oldu�unu belirtir.
{
    void Boost(PlayerController playerController);// Metot, i�lem yap�lacak olan oyuncuyu temsil eden bir PlayerController nesnesi al�r.
}
