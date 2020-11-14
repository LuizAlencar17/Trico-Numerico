using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inimigos : MonoBehaviour {
	Rigidbody2D rb2d;
	SpriteRenderer flip;

	public string id;
	public Text TextoNumero;

	public float velocidade;  // velocidade do inimigo
	public float duracaoDirecao;  // duração para adar em uma direção
	private float tempoNaDirecao;  // quanto tempo ele está anadando nesta direção

	public GameObject Falha;

	public int DirecaoX;

	// Use this for initialization
	void Start () {
		
		flip = GetComponent<SpriteRenderer>();
		rb2d = GetComponent<Rigidbody2D>();

	}
	
	// Update is called once per frame
	void Update () {

		TextoNumero.transform.position = transform.position;
		TextoNumero.text = id;

		MovePlataforma ();
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Bullet"){
			if (id == Expressoes.resposta) {
			//Send Data
			SendData.Send(Expressoes.questao, id, "yes", "", "Fire");
				Jogador.indice += 30;

				Expressoes.indice++;
				Destroy (other.gameObject);
				TextoNumero.gameObject.SetActive(false);
				Destroy (gameObject);


			} else {
				//Send Data
				SendData.Send(Expressoes.questao, id, "no", "", "Fire");

				GameObject cloneErro = Instantiate (Falha, transform.position, transform.rotation);
				Jogador.indice -= 50;

				cloneErro.transform.SetParent(transform);
			
				StartCoroutine (ErrouResposta(cloneErro));
			}
		}

		if (other.gameObject.tag == "Player") {
			Jogador player = other.GetComponent<Jogador> ();
			player.DamagePlayer ();
			//Send Data
			SendData.Send(Expressoes.questao, id, "", "", "enemy");
		}
	}

	void MovePlataforma(){
		transform.Translate(new Vector3(DirecaoX * velocidade * Time.deltaTime,0,0));  // sempre movimentando
		tempoNaDirecao += Time.deltaTime;  // incrementação do tempo

		if (tempoNaDirecao >= duracaoDirecao) {  // quando o tempo estourar 
			tempoNaDirecao = 0;  // zera o tempo
			DirecaoX = DirecaoX * -1;
			flip.flipX = !flip.flipX;
		}
	}

	IEnumerator ErrouResposta(GameObject other){
		
		for (int i = 0; i < 5; i ++) {
			Falha.SetActive (false);
			yield return new WaitForSeconds (0.04f);
			Falha.SetActive (true);
			yield return new WaitForSeconds (0.04f);
		}

		Destroy (other.gameObject);
	}

}