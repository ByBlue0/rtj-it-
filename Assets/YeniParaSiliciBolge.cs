using UnityEngine;
using System.Collections;

public class YeniParaSiliciBolge : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Sadece ParaSurukle bileþeni olan objeleri hedef al
        ParaSurukle para = other.GetComponent<ParaSurukle>();
        if (para != null)
        {
            StartCoroutine(SilVeToplamdanDus(other.gameObject));
        }
    }

    IEnumerator SilVeToplamdanDus(GameObject para)
    {
        yield return new WaitForSeconds(1f);

        ParaBilgi bilgi = para.GetComponent<ParaBilgi>();
        if (bilgi != null)
        {
            ParaDagitici dagitici = FindObjectOfType<ParaDagitici>();
            if (dagitici != null)
            {
                dagitici.ParaDegerindenCikar(bilgi.miktar);
            }
        }

        Destroy(para);
    }
}
