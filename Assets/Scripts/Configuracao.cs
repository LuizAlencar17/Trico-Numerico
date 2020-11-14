using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Configuracao : MonoBehaviour {
	public Sprite somAtivado;
	public Sprite somDesativado;

	public Button BtnSom;

	void Update () {
		if(PlayerPrefs.GetString("musica")=="desativado"){
			BtnSom.image.sprite = somDesativado;
		}else{
			BtnSom.image.sprite = somAtivado;
		}
	}

	public void AtivaSom () {
		Musica musica = GameObject.Find("Sons").GetComponent<Musica>();
		
		if(PlayerPrefs.GetString("musica")=="desativado"){
			musica.AtivaMusica();
		}else
			musica.DesativaMusica();
		
		musica.TocaClick();

	}

}
