using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorCenas : MonoBehaviour {

	public static string Cena;

	public void Retornar(){

		if (PlayerPrefs.GetString ("UltimaCena") == "") {
			Cena = "Configuracao";
		} else 
			Cena = PlayerPrefs.GetString ("UltimaCena");
		
		SceneManager.LoadScene (Cena);
	}

	public void ChamaCena (string other) {
		try
		{
			SceneManager.LoadScene("Fase"+other);

		}catch{}
		SceneManager.LoadScene(other);
	}

	public void IrParaTutorial () {
		PlayerPrefs.SetInt("tutorial",0);

		Cena = "Fase1";

		SceneManager.LoadScene (Cena);
	}
	
}
