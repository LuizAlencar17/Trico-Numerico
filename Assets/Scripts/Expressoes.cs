using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Expressoes : MonoBehaviour {

	public List <Inimigos> VetorInimigos;

	public List <string> Calculos;

	public List <string> VetorCalculos;
	public List <string> VetorRespostas;

	public static string resposta;
	public static string questao;

	public Text TextoExpressoes;

	public static int indice=0;

	void Start () {
		indice = 0;
		GerenciaCalculosERespsotas ();
	}
	
	void Update () {
		if (indice < VetorCalculos.Count) {
			TextoExpressoes.text = VetorCalculos [indice];
			resposta = VetorRespostas [indice];
		} else {
			TextoExpressoes.text = "Completo";
			TextoExpressoes.fontSize = 80;
		}
		questao = TextoExpressoes.text;
	}

	void GerenciaCalculosERespsotas(){
		for (int j,i = 0; i < VetorInimigos.Count; i++) {
			// Adiciona somente o calculo da expressão
			VetorCalculos.Add (Calculos [i].Split(':')[0].ToString());
			// Adiciona some a resposta do calculo
			VetorRespostas.Add (Calculos [i].Split(':')[1].ToString());
		}

		// Muda posição dos calculos e respostas
		EmbaralhaVetorDeCalculo();

		// Cada inimigo recebe seu valor
		for(int i=0; i<VetorInimigos.Count; i++){
			VetorInimigos [i].id = VetorRespostas [i];
		}

		// Realiza uma nova troca de valores, mas entre a posição dos inimigos
		for(int i=1; i<VetorInimigos.Count-1;i++){
			if (Random.Range (0, 2) == 1) {
				if (Random.Range (0, 2) == 1) {
					TrocaInimigos (VetorInimigos [i], VetorInimigos [i + 1]);
				} else {
					TrocaInimigos (VetorInimigos [i], VetorInimigos [i - 1]);
				}
			}
		}
	}

	void TrocaInimigos(Inimigos inimigo1, Inimigos inimigo2){
		string valorTemporario;
		valorTemporario = inimigo1.id;
		inimigo1.id = inimigo2.id;
		inimigo2.id = valorTemporario;
	}

	void EmbaralhaVetorDeCalculo(){
		for(int i=0; i<VetorCalculos.Count; i++){
			// Valor a ser somado com indice para trocar posições dos vetores
			int aleatorio = Random.Range (1,3);
			
			// Verifica se aleatorio + indice é maior que o vetor
			if ((aleatorio + i) >= VetorCalculos.Count) {
				aleatorio = 0;
			}

			// Troca posição dos calculos
			string calculoTemporario = VetorCalculos [i];
			VetorCalculos [i] = VetorCalculos [i + aleatorio];
			VetorCalculos [i + aleatorio] = calculoTemporario;
			
			// Troca posição das respostas
			string respostaTemporario = VetorRespostas [i];
			VetorRespostas [i] = VetorRespostas [i + aleatorio];
			VetorRespostas [i + aleatorio] = respostaTemporario;
		}
	}
}
