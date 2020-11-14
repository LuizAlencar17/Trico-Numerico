using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelecaoFases : MonoBehaviour {

	public Button[] Fases;

	void Start () {
		//A fase 1 está sempre disponivel
		PlayerPrefs.SetInt ("Fase1", 1);
		PlayerPrefs.SetInt ("Fase2", 1);

		for(int i=0; i<Fases.Length; i++){
			if (PlayerPrefs.GetInt ("Fase" + (i).ToString ()) == 1) {
				Fases [i].interactable = true;
			} else {
				Fases [i].interactable = false;
			}
		}
	}

}
