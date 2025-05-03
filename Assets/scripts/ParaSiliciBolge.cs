using UnityEngine;
using System.Collections.Generic;

public class ParaSiliciBolge : MonoBehaviour
{
    [Header("Oluşturulacak Prefablar")]
    public List<GameObject> kabulEttigiPrefablar;
    public Transform spawnNoktasi;

    private void OnMouseDown()
    {
        if (spawnNoktasi == null || kabulEttigiPrefablar == null || kabulEttigiPrefablar.Count == 0)
            return;

        GameObject secilenPrefab = kabulEttigiPrefablar[Random.Range(0, kabulEttigiPrefablar.Count)];
        Vector3 spawnPozisyonu = spawnNoktasi.position;

        GameObject para = Instantiate(secilenPrefab, spawnPozisyonu, Quaternion.identity);

        // Sürükleme scripti
        ParaSurukle ps = para.GetComponent<ParaSurukle>();
        if (ps != null)
            ps.orijinalPrefab = secilenPrefab;

        // Değer Ekle
        ParaBilgi bilgi = para.GetComponent<ParaBilgi>();
        if (bilgi != null)
        {
            ParaDagitici dagitici = FindObjectOfType<ParaDagitici>();
            if (dagitici != null)
                dagitici.ParaDegeriEkle(bilgi.miktar);
        }

        Debug.Log("Yeni para oluşturuldu: " + secilenPrefab.name);
    }
}
