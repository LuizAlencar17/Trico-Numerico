using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickButtons : MonoBehaviour,IPointerClickHandler {

	public void OnPointerClick(PointerEventData data){
		//Toca som de "click"
		//Não captura o pulo ainda
		try{
			Musica musica = GameObject.Find("Sons").GetComponent<Musica>();	
			musica.TocaClick();
		}catch{}
		SendData.Send("", "","", "", "", gameObject.name);
		
		//Debug.Log (gameObject.name);
	}
	
}
