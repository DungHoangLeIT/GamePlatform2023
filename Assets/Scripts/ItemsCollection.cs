using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemsCollection : MonoBehaviour
{
    private float cherriesCounting = 0f;
    [SerializeField] private TMP_Text cherriesText;
    [SerializeField] private AudioSource cherriesAudioSource;

    private void Start()
    {
        cherriesText.text = "Cheries: 0";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            cherriesAudioSource.Play();
            Destroy(collision.gameObject);
            cherriesCounting++;
            cherriesText.text = "Cheries: " + cherriesCounting; 
        }
    }
}
