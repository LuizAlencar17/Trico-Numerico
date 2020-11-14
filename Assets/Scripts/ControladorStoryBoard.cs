using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControladorStoryBoard : MonoBehaviour {

	public List<Sprite> chat; 
	public SpriteRenderer Conversa;
	private int indice;

	void Start () {
		indice = 0;
	}
	
	void Update () {
		Conversa.sprite = chat [indice];
	}

	public void Proxima(){
		if (indice <= chat.Count - 2) {
			indice++;
		} else {
			ControladorCenas.Cena = "Fase1";
			SceneManager.LoadScene (ControladorCenas.Cena);
		}
	}

	public void ChamaCena (string other) {
		ControladorCenas.Cena = other;
		SceneManager.LoadScene (ControladorCenas.Cena);

	}

}
