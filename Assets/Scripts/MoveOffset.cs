using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveOffset : MonoBehaviour {

	private Material background;
	public float speed;
	private float offSet;

	SpriteRenderer imagem;

	void Start () {
		background = GetComponent<Renderer> ().material;
		imagem = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		offSet += 0.001f;
		background.SetTextureOffset ("_MainTex", new Vector2 (offSet * speed, 0));
	}

}
