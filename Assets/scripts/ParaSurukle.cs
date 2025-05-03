using UnityEngine;

public class ParaSurukle : MonoBehaviour
{
    private bool surukleniyor = false;
    private Vector3 offset;

    public GameObject orijinalPrefab;

    void OnMouseDown()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - new Vector3(mousePos.x, mousePos.y, 0);
        surukleniyor = true;
    }

    void OnMouseUp()
    {
        surukleniyor = false;

        Collider2D[] carpisanlar = Physics2D.OverlapPointAll(transform.position);
        foreach (var col in carpisanlar)
        {
            // Silici bölgeye temas ettiyse
            if (col.GetComponent<ParaSiliciBolge>() != null)
            {
                ParaBilgi bilgi = GetComponent<ParaBilgi>();
                if (bilgi != null)
                {
                    ParaDagitici dagitici = FindObjectOfType<ParaDagitici>();
                    if (dagitici != null)
                        dagitici.ParaDegerindenCikar(bilgi.miktar);
                }

                Destroy(gameObject);
                break;
            }
        }
    }

    void Update()
    {
        if (surukleniyor)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x, mousePos.y, 0) + offset;
        }
    }
}
