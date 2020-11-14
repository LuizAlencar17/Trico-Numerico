using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

	public Text TextoJoystick;
	public Text TextoBotaoAtirar;
	public Text TextoBotaoPular;

	public Jogador Jogador;

	public GameObject Joystick;
	public GameObject BotaoAtirar;
	public GameObject BotaoPular;

	public GameObject SetaJoystick;
	public GameObject SetaAtirar;

	public GameObject[] PopUpsSecundarios;

	public int IndicePopUps;
	public bool inicia_tutorial;

	void Start () {
		inicia_tutorial = true;

		if(PlayerPrefs.GetInt("tutorial")!=1){
			IndicePopUps = 0;
			EscondeElementos();
			
			//Etapa 1: Joystick aparece
			TextoJoystick.gameObject.SetActive(true);
			Joystick.gameObject.SetActive(true);
			SetaJoystick.gameObject.SetActive(true);

			PlayerPrefs.SetInt("tutorial",1);
		}else{
			EscondeElementos();
			gameObject.SetActive(false);
		}
	}

	public void FixedUpdate(){
		if(Mathf.Abs(Jogador.posicao_inicial.x - Jogador.gameObject.transform.position.x)>3 && inicia_tutorial){
			inicia_tutorial = false;
			ComoAtirar();
		}
	}
	
	public void ComoAtirar(){
		EscondeElementos();

		//Etapa 2: Botão Atirar aparece
		Joystick.gameObject.SetActive(true);
		SetaJoystick.gameObject.SetActive(true);

		TextoBotaoAtirar.gameObject.SetActive(true);
		BotaoAtirar.gameObject.SetActive(true);
		SetaAtirar.gameObject.SetActive(true);

	}

	public void ProximoPopUp(){
		EscondeElementos();
		if(IndicePopUps<PopUpsSecundarios.Length){
			PopUpsSecundarios[IndicePopUps].SetActive(true);
			IndicePopUps++;
		}else{
			Debug.Log("Fim do Tutorial");
		}
	}

	public void ComoPular(){
		EscondeElementos();

		//Etapa 2: Botão Atirar aparece
		BotaoPular.gameObject.SetActive(true);
		TextoBotaoPular.gameObject.SetActive(true);

		gameObject.SetActive(false);
	}

	public void EscondeElementos(){
		//Todos ficam invisiveis
		TextoJoystick.gameObject.SetActive(false);
		TextoBotaoAtirar.gameObject.SetActive(false);
		TextoBotaoPular.gameObject.SetActive(false);

		Joystick.gameObject.SetActive(false);
		BotaoAtirar.gameObject.SetActive(false);
		BotaoPular.gameObject.SetActive(false);
		
		SetaJoystick.gameObject.SetActive(false);
		SetaAtirar.gameObject.SetActive(false);

		//Oculta os PopUps de Intrução
		for(int i=0; i<PopUpsSecundarios.Length; i++){
			PopUpsSecundarios[i].SetActive(false);
		}
	}
}
