using UnityEngine;

public class ParaEfekti : MonoBehaviour
{
    public SpriteRenderer maviSprite;  // Mavi sprite (gizli çizgiler)
    public float gorunurlukHizi = 1f;  // Þeffaflýðýn artýþ hýzý

    private bool iceride = false;
    private float alphaDegeri = 0f;

    void Update()
    {
        if (iceride && alphaDegeri < 1f)
        {
            alphaDegeri += Time.deltaTime * gorunurlukHizi;
            alphaDegeri = Mathf.Clamp01(alphaDegeri);
            maviSprite.color = new Color(1f, 1f, 1f, alphaDegeri);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("IsikAlani"))
        {
            iceride = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("IsikAlani"))
        {
            iceride = false;
            alphaDegeri = 0f;
            maviSprite.color = new Color(1f, 1f, 1f, 0f);
        }
    }
    void OnMouseDrag()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, mousePos.y, 0f);
    }

}
