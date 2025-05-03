using UnityEngine;

public class ParaSurukle : MonoBehaviour
{
    private bool surukleniyor = false;
    private Vector3 offset;

    public GameObject orijinalPrefab; // Sadece bu prefab ile eşleşirse silinecek

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
            ParaSiliciBolge bolge = col.GetComponent<ParaSiliciBolge>();
            if (bolge != null && bolge.kabulEttigiPrefab == orijinalPrefab)
            {
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
