using UnityEngine;

public class ParaSiliciBolge : MonoBehaviour
{
    public GameObject kabulEttigiPrefab;
    public Transform spawnNoktasi; // Paranýn nerede oluþacaðýný kontrol etmek için (isteðe baðlý)

    private void OnMouseDown()
    {
        if (kabulEttigiPrefab != null)
        {
            Vector3 spawnPozisyonu = spawnNoktasi != null ? spawnNoktasi.position : transform.position + Vector3.up * 1f;

            GameObject para = Instantiate(kabulEttigiPrefab, spawnPozisyonu, Quaternion.identity);

            // Eðer prefabta ParaSurukle scripti varsa orijinalPrefab'ý ayarla
            ParaSurukle ps = para.GetComponent<ParaSurukle>();
            if (ps != null)
            {
                ps.orijinalPrefab = kabulEttigiPrefab;
            }

            Debug.Log("Yeni para oluþturuldu: " + kabulEttigiPrefab.name);
        }
    }
}
