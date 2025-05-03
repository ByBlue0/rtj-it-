using UnityEngine;
using TMPro; // TMP kullanýyorsan bunu unutma

public class Tarti : MonoBehaviour
{
    public TextMeshProUGUI agirlikText; // UI alanýna sahnedeki TMP text'i baðla

    private void OnTriggerEnter2D(Collider2D other)
    {
        // DemirPara scripti varsa onun aðýrlýðýný al
        DemirPara dp = other.GetComponent<DemirPara>();
        if (dp != null && agirlikText != null)
        {
            agirlikText.text = dp.agirlikGram.ToString("0.0") + " g";
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Tartýdan çýkýnca sýfýrla
        if (other.GetComponent<DemirPara>() != null && agirlikText != null)
        {
            agirlikText.text = "0.0 g";
        }
    }
}
