using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HesapMakinesi : MonoBehaviour
{
    public TextMeshProUGUI textEkran; // Hesap makinesi ekraný
    
    private string ifade = "";

    private void Start()
    {
        textEkran.text = string.Empty;
    }


    public void ButonaBasildi(string karakter)
    {
        if (karakter == "=")
        {
            Hesapla();
        }
        else if (karakter == "AC")
        {
            ifade = "";
            textEkran.text = "";
        }
        else
        {
            ifade += karakter;
            textEkran.text = ifade;
        }
    }

    private void Hesapla()
    {
        try
        {
            var sonuc = new System.Data.DataTable().Compute(ifade, null);
            ifade = sonuc.ToString();
            textEkran.text = ifade;
        }
        catch
        {
            ifade = "";
            textEkran.text = "Hata";
        }
    }
}
