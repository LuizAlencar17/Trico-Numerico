using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour {

	public int damage=1;       //dano da bala
	public float Velocidade;        //Velocidade da bala
	public float tempoDuracao; //Tempo de destruição da bala

	// Use this for initialization
	void Start () {
		//Destroi balla
		Destroy(gameObject, tempoDuracao);
	}
	
	// Update is called once per frame
	void Update () {
		//Direção da bala
		transform.Translate (Vector2.right*Velocidade*Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player")
		{
			Jogador jogador = other.gameObject.GetComponent<Jogador>();
			jogador.DamagePlayer();
		}

		if (gameObject.tag != "Enemy")
		{
			Destroy(gameObject);
		}

	}
}