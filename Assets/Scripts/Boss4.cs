using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss4 : MonoBehaviour
{
    Boss boss;
    public bool criou_clone;
    public Transform local_de_criacao;

    // Start is called before the first frame update
    void Start()
    {
        criou_clone = false;
        boss = GetComponent<Boss>();
        StartCoroutine("CriaClone");
    }

    IEnumerator CriaClone()
    {
        //Diminui o tempo de teleporte
        if (boss.vida < boss.vida_maxima / 2 && !criou_clone)
        {
            criou_clone = true;
            //Cria clone
            GameObject clone = Instantiate(gameObject, local_de_criacao.position, local_de_criacao.rotation);
            clone.transform.SetParent(local_de_criacao.parent);
            clone.transform.localScale = gameObject.transform.localScale;

            clone.GetComponent<Boss4>().enabled = false;

            Boss boss_clone = clone.GetComponent<Boss>();

            while (true)
            {
                yield return new WaitForSeconds(0.05f);
                boss_clone.vida = boss.vida;
            }

            
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine("CriaClone");
    }
}
