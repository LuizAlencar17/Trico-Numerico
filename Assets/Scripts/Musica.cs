using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Musica : MonoBehaviour {

	public AudioSource musica;
	public AudioSource click;

	void Start(){
		DontDestroyOnLoad(gameObject);
		
		if(PlayerPrefs.GetString("musica")=="desativado"){
			musica.Stop();
		}else
		if(PlayerPrefs.GetString("musica")=="ativado"){
			musica.Play();
		}else
			musica.Play();


		if(PlayerPrefs.GetString("nome") != "" && PlayerPrefs.GetString("idade") != ""){
            SceneManager.LoadScene("Menu");
        }else{
			SceneManager.LoadScene("PlayerIdentification");
		}
	}

	public void AtivaMusica(){
		PlayerPrefs.SetString("musica","ativado");
		musica.Play();
	}

	public void DesativaMusica(){
		PlayerPrefs.SetString("musica","desativado");
		musica.Stop();
	}

	public void TocaClick(){
		if(PlayerPrefs.GetString("musica")!="desativado"){
			click.PlayOneShot(click.clip);
		}
	}
}
