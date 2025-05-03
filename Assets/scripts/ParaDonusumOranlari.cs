using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "YeniParaDonusumOranlari", menuName = "Para/Para Dönüşüm Oranları")]
public class ParaDonusumOranlari : ScriptableObject
{
    [System.Serializable]
    public class Donusum
    {
        public string kaynakBirim;
        public string hedefBirim;
        public float oran; // Örn: 1 Balık = 2.5 Tavşan → oran = 2.5
    }

    public List<Donusum> donusumListesi;

    public float GetDonusumOrani(string kaynak, string hedef)
    {
        if (kaynak == hedef) return 1f;

        foreach (Donusum d in donusumListesi)
        {
            if (d.kaynakBirim == kaynak && d.hedefBirim == hedef)
            {
                return d.oran;
            }
        }

        Debug.LogWarning($"Dönüşüm oranı bulunamadı: {kaynak} -> {hedef}, 1.0 varsayılıyor.");
        return 1f;
    }
}
