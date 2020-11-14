using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Porta : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player"){
			//Tenta buscar no nome da proxima fase através dos index da scena posterior
			string proximaFase = NameFromIndex(SceneManager.GetActiveScene().buildIndex+1);
			PlayerPrefs.SetInt (proximaFase, 1);
			SceneManager.LoadScene(proximaFase);
		}
	}
	//Captura o nome de uma Scene através de index
	private static string NameFromIndex(int BuildIndex){
		string path = SceneUtility.GetScenePathByBuildIndex(BuildIndex);
		int slash = path.LastIndexOf('/');
		string name = path.Substring(slash + 1);
		int dot = name.LastIndexOf('.');
		return name.Substring(0, dot);
	 }
}