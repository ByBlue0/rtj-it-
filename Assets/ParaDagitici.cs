using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ParaDagitici : MonoBehaviour
{
    [Header("Para Prefabları")]
    public GameObject para5Prefab;
    public GameObject para10Prefab;
    public GameObject para50Prefab;
    public GameObject para100Prefab;

    [Header("Dağıtım Noktaları")]
    public Transform spawnNoktasi;              // Kırmızı alan
    public Transform[] hedefNoktalar;           // Yeşil alanlar

    [Header("UI")]
    public Text toplamTutarText;

    private int toplamTutar = 0;
    private int hedefIndex = 0;
    private List<GameObject> aktifParalar = new List<GameObject>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            List<int> odemeListesi = new List<int> { 50, 50, 10, 5 }; // Örnek müşteri: 115 TL
            MusteriParaVerdi(odemeListesi);
        }
    }

    public void MusteriParaVerdi(List<int> odemeListesi)
    {
        // Önceki paraları temizle
        foreach (GameObject para in aktifParalar)
        {
            Destroy(para);
        }

        aktifParalar.Clear();
        toplamTutar = 0;
        hedefIndex = 0;
        toplamTutarText.text = "0 TL";

        StartCoroutine(ParalariDagit(odemeListesi));
    }

    IEnumerator ParalariDagit(List<int> odemeListesi)
    {
        foreach (int miktar in odemeListesi)
        {
            GameObject prefab = GetPrefabByAmount(miktar);
            if (prefab != null)
            {
                GameObject para = Instantiate(prefab, spawnNoktasi.position, Quaternion.identity);

                // 👇 EKLENECEK SATIR
                para.GetComponent<ParaSurukle>().orijinalPrefab = prefab;

                aktifParalar.Add(para);

                Transform hedef = hedefNoktalar[hedefIndex % hedefNoktalar.Length];
                hedefIndex++;

                StartCoroutine(ParayiGotur(para.transform, hedef.position));

                toplamTutar += miktar;
                toplamTutarText.text = toplamTutar + " TL";

                yield return new WaitForSeconds(0.2f);
            }
        }
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

    GameObject GetPrefabByAmount(int miktar)
    {
        switch (miktar)
        {
            case 5: return para5Prefab;
            case 10: return para10Prefab;
            case 50: return para50Prefab;
            case 100: return para100Prefab;
            default: return null;
        }
    }
}
