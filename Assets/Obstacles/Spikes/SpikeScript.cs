using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    public int damage = 100;
    BoxCollider2D BoxCollider2D { get; set; }
    public AudioSource spikeSound;


    private void Awake()
    {
        BoxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var collider = collision.collider;
        if(collider.tag != "Player")
            return;
        var player = collision.gameObject.GetComponent<CharacterController2D>();
        player.PlayerHealth -= damage;
        spikeSound.Play();
    }
}
