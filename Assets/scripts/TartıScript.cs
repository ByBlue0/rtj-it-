using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TartıScript : MonoBehaviour
{
    public TextMeshPro text;

    private void Start()
    {
        text.text = string.Empty;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OrtakOzellik ortakOzellik = collision.gameObject.GetComponent<OrtakOzellik>();

        if (ortakOzellik != null)
        {
            text.text = ortakOzellik.esasAgirlik.ToString();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OrtakOzellik ortakOzellik = collision.gameObject.GetComponent<OrtakOzellik>();

        if (ortakOzellik != null)
        {
            text.text = string.Empty;
        }
    }
}
