using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {
	public Animator anim;
	public List<Button> Botoes;

	public string Cena;

	public Sprite somAtivado;
	public Sprite somDesativado;

	public Button BtnSom;

	void Start(){
		anim = GetComponent<Animator> ();
	}
		
	void Update () {
		if(PlayerPrefs.GetString("musica")!="desativado"){
			BtnSom.image.sprite = somAtivado;
		}else{
			BtnSom.image.sprite = somDesativado;
		}
	}

	public void Ajuda () {
		PlayerPrefs.SetString ("UltimaCena", SceneManager.GetActiveScene().name);
		//continuar
		ChamaCena ("Ajuda");
	}

	public void AtivaSom () {
		Musica musica = GameObject.Find("Sons").GetComponent<Musica>();
		
		if(PlayerPrefs.GetString("musica")=="desativado"){
			musica.AtivaMusica();
		}else
			musica.DesativaMusica();
	}

	public void ChamaCena (string other) {
		Cena = other;
		SceneManager.LoadScene (Cena);

	}

	public void PausarJogo(){
		if (anim.GetBool ("Pause") == false) {
			anim.SetBool ("Pause", true);
			for(int i=0; i<Botoes.Count; i++){
				Botoes [i].interactable = true;
			}
		} else {
			anim.SetBool ("Pause", false);
			for(int i=0; i<Botoes.Count; i++){
				Botoes [i].interactable = false;
			}
		}
	}
}