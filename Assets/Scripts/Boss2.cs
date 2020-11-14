using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : MonoBehaviour
{
    public GameObject bala;
    public float tempo_de_disparo;
    public float tempo_de_disparo_reduzido;
    public Transform origem_bala;
    SpriteRenderer sprite;
    Boss boss;

    // Start is called before the first frame update
    void Start()
    {
        tempo_de_disparo_reduzido = tempo_de_disparo / 2;
        boss = GetComponent<Boss>();
        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(Atirar());
    }

    IEnumerator Atirar()
    {
        if (boss.vida < boss.vida_maxima/2)
        {
            tempo_de_disparo = tempo_de_disparo_reduzido;
        }
        
        //Dispara
        GameObject clone_bala = Instantiate(bala, origem_bala.position, origem_bala.rotation);

        SpriteRenderer sprite_disparo = clone_bala.GetComponent<SpriteRenderer>();
        sprite_disparo.flipX = !sprite.flipX;

        //Vira a bala
        if (sprite.flipX)
        {
            clone_bala.transform.eulerAngles = new Vector3(0, 0, 180);
            sprite_disparo.flipX = sprite.flipX;
        }
        yield return new WaitForSeconds(tempo_de_disparo);

        if(boss.vida>1f)
            StartCoroutine(Atirar());
    }
}
