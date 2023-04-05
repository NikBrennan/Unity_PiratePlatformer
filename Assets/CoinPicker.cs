using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPicker : MonoBehaviour
{
    public static float coins = 0;
    public AudioSource coinSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //coinSound.Play();
        if (collision.gameObject.tag == "Coin")
        {
            coins++;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Gem")
        {
            coins += 5;
            Destroy(collision.gameObject);
        }
    }
}
