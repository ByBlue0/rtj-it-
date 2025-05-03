using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HesapMakinesiButon : MonoBehaviour
{
    public string karakter; // Örnek: "1", "+", "=" gibi
    private HesapMakinesi hesapMakinesi;

    private void Start()
    {
        hesapMakinesi = FindObjectOfType<HesapMakinesi>();
    }

    private void OnMouseDown()
    {
        if (hesapMakinesi != null)
        {
            hesapMakinesi.ButonaBasildi(karakter);
        }
    }
}
