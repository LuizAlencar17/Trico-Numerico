using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plataforma : MonoBehaviour {

	public float velocidade;  // velocidade do inimigo
	public float duracaoDirecao;  // duração para adar em uma direção
	private float tempoNaDirecao;  // quanto tempo ele está anadando nesta direção
	public int DirecaoX;

	public bool horizontal;

	void Update () {
		MovePlataforma ();
	}

	void MovePlataforma(){

		if(horizontal){
			transform.Translate(new Vector3(DirecaoX * velocidade * Time.deltaTime,0,0));  // sempre movimentando
		}else{
			transform.Translate(new Vector3(0,DirecaoX * velocidade * Time.deltaTime,0));  // sempre movimentando
		}

		tempoNaDirecao += Time.deltaTime;  // incrementação do tempo

		if (tempoNaDirecao >= duracaoDirecao) {  // quando o tempo estourar 
			tempoNaDirecao = 0;  // zera o tempo
			DirecaoX = DirecaoX * -1;
		}
	}
		
}
