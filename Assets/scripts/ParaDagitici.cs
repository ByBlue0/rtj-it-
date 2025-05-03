using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ParaDagitici : MonoBehaviour
{
    [System.Serializable]
    public class ParaBirim
    {
        public string ad;
        public List<GameObject> demirParalar;
        public List<GameObject> kagitParalar;
    }

    [Header("Tüm Para Birimleri")]
    public List<ParaBirim> paraBirimleri;

    [Header("Dağıtım Noktaları")]
    public Transform spawnNoktasi;
    public Transform[] hedefNoktalar;

    [Header("UI")]
    public Text toplamTutarText;

    [Header("Dönüşüm Oranları")]
    public ParaDonusumOranlari paraOranlari;

    private int hedefIndex = 0;
    private List<GameObject> aktifParalar = new List<GameObject>();
    private float toplamTutar = 0f;
    private string hedefBirim = "";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            DagitRastgeleMusteriParasi();
        }
    }

    void DagitRastgeleMusteriParasi()
    {
        if (paraBirimleri.Count == 0 || musteriler.Count == 0) return;

        // Mevcut paraları temizle
        foreach (GameObject para in aktifParalar)
        {
            Destroy(para);
        }
        aktifParalar.Clear();
        hedefIndex = 0;
        toplamTutar = 0;
        toplamTutarText.text = "0";

        // Rastgele müşteri çağır
        GameObject musteriGO = Instantiate(
            musteriler[Random.Range(0, musteriler.Count)],
            musteriSpawnNoktasi.position,
            Quaternion.identity
        );

        MusteriVerisi veri = musteriGO.GetComponent<MusteriVerisi>();
        if (veri == null || veri.verilecekParalar.Count == 0) return;

        hedefBirim = veri.birimAdi;
        StartCoroutine(ParalariDagit(veri.birimAdi, veri.verilecekParalar));
    }

    IEnumerator ParalariDagit(string kaynakBirim, List<GameObject> paralar)
    {
        foreach (GameObject prefab in paralar)
        {
            GameObject para = Instantiate(prefab, spawnNoktasi.position, Quaternion.identity);

            var ps = para.GetComponent<ParaSurukle>();
            if (ps != null)
                ps.orijinalPrefab = prefab;

            var bilgi = para.GetComponent<ParaBilgi>();
            if (bilgi != null)
            {
                float cevrilenMiktar = Cevir(bilgi.miktar, kaynakBirim, hedefBirim);
                bilgi.miktar = Mathf.RoundToInt(cevrilenMiktar); // float → int dönüşümü
                toplamTutar += cevrilenMiktar;
            }

            aktifParalar.Add(para);

            Transform hedef = hedefNoktalar[hedefIndex % hedefNoktalar.Length];
            hedefIndex++;

            StartCoroutine(ParayiGotur(para.transform, hedef.position));
            yield return new WaitForSeconds(0.2f);
        }

        toplamTutarText.text = toplamTutar + " " + hedefBirim;
    }

    IEnumerator ParayiGotur(Transform para, Vector3 hedef)
    {
        float zaman = 0f;
        Vector3 baslangic = para.position;

        while (zaman < 1f)
        {
            zaman += Time.deltaTime * 2f;
            para.position = Vector3.Lerp(baslangic, hedef, zaman);
            yield return null;
        }
    }

    float Cevir(float miktar, string kaynak, string hedef)
    {
        foreach (var d in paraOranlari.donusumListesi)
        {
            if (d.kaynakBirim == kaynak && d.hedefBirim == hedef)
            {
                return miktar * d.oran;
            }
        }

        Debug.LogWarning($"Dönüşüm oranı bulunamadı: {kaynak} → {hedef}");
        return 0;
    }

    public void ParaDegeriEkle(float miktar)
    {
        toplamTutar += miktar;
        toplamTutarText.text = toplamTutar + " " + hedefBirim;
    }

    public void ParaDegerindenCikar(float miktar)
    {
        toplamTutar -= miktar;
        toplamTutar = Mathf.Max(0, toplamTutar);
        toplamTutarText.text = toplamTutar + " " + hedefBirim;
    }

    [Header("Müşteriler")]
    public List<GameObject> musteriler;
    public Transform musteriSpawnNoktasi;
}
