using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3 : MonoBehaviour
{
    public GameObject jogador;
    public float tempo_de_teleporte;
    public float tempo_de_teleporte_reduzido;
    Boss boss;

    // Start is called before the first frame update
    void Start()
    {
        tempo_de_teleporte_reduzido = tempo_de_teleporte / 2;
        boss = GetComponent<Boss>();
        StartCoroutine("Teleporta", 6);
    }

    IEnumerator Teleporta()
    {
        Animator anim = GetComponent<Animator>();
        //Diminui o tempo de teleporte
        if (boss.vida < boss.vida_maxima/2)
        {
            tempo_de_teleporte = tempo_de_teleporte_reduzido;
        }


        //Desabilita animação e movimentação
        anim.enabled = false;
        boss.podeAndar = false;
        //Diminui tamanho do objeto
        for (int i=0; i<10; i++)
        {
            yield return new WaitForSeconds(0.1f);
            transform.localScale = new Vector3(
                transform.localScale.x/2, 
                transform.localScale.y/2, 
                transform.localScale.z/2
            );

        }

        //Captura a posição para se teleportar
        Vector3 position_para_teleportar = new Vector3(
            jogador.transform.position.x,
            transform.position.y,
            transform.position.z
        );

        //Vai para a posição capturada
        transform.position = position_para_teleportar;

        //Aumenta tamanho do objeto
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.1f);
            transform.localScale = new Vector3(
                transform.localScale.x * 2,
                transform.localScale.y * 2,
                transform.localScale.z * 2
            );

        }
        //Ativa animação e movimentação
        anim.enabled = true;
        boss.podeAndar = true;
        yield return new WaitForSeconds(tempo_de_teleporte);


        if (boss.vida>1f)
            StartCoroutine("Teleporta", tempo_de_teleporte);
    }
}
