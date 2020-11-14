using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Jogador : MonoBehaviour {

	public float Velocidade;      //Velocidade do Jogador
	public static float move;		 //Controlador de velocidade do Jogador
	public float ForcaPulo;  //Força do Pulo
	public int QuantidadeBala; //Quantidade de tiros
	public bool noChao;   //Verifica se esta no chão
	public bool pulando = false;    //Verifica se esta pulando
	public bool isAlive = true;      //Verifica se esta vivo 
	public static int vidas;               //Vidas do jogador
	public bool invunerabilidade = false; //invunerabilidade do jogador
	AudioSource somPulo; //Som de pulo
	AudioSource somTiro;
	public float proximoTiro;   //Quantidade de tiros
	public float tempoTiro;   //Intervalo de tempo entre tiros
	public GameObject Bala; //Bala
	public List<Sprite> SpritesVidas;
	public Image FbVidas;

	public Slider BarraVida;
	public static int indice;

	public Transform origemBala; //Lugar onde a bala é criada
	public Transform checkChao; //Local de verificação se o jogador toca no chão
	SpriteRenderer sprite;		  //Controladora do SpriteRenderer do jogador
	Animator anim;				  //Controladora de animção do jogador
	Rigidbody2D rb2d;			  //Controladora do Rigidbody(fisica) do jogador

	public GameObject handle;
	public Vector3 posicao_inicial;

	void Awake(){
		posicao_inicial = transform.position;
		SendData.time = 0f;
		FloatingJoystick.Ativado = false;

		indice=300;
		StartCoroutine (IndiceVida());
		vidas = 3;
		pulando = false;
		//Pega componentes
		sprite = GetComponent<SpriteRenderer> ();
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();
		somPulo = GetComponent<AudioSource> ();
		somTiro = GameObject.Find ("Main Camera").GetComponent<AudioSource> ();
	}

	void MovimentosPeloTeclado(){
		move = Input.GetAxis("Horizontal");

		if (Input.GetAxis("Jump") > 0){
			Pulo();
        }
		if (Input.GetAxis("Fire3") > 0){
			Fire();
        }
	}

	void FixedUpdate(){
		if (isAlive) {
			//Teste para gravar video de gameplay
			//MovimentosPeloTeclado();
			//Adiciona velocidade
			rb2d.velocity = new Vector2 (Velocidade * move, rb2d.velocity.y);
			//Vira jogador
			if ((move > 0f && sprite.flipX) || (move < 0f && !sprite.flipX)) {
				Flip ();
			}
			//Muda animaçoes
			anim.SetBool ("Pulo", pulando);
			anim.SetFloat ("Velocidade", Mathf.Abs (move));
		}
		FbVidas.sprite = SpritesVidas[vidas];
		if(vidas < 1){
			//Morte
			isAlive = false;
			anim.SetBool ("Morto", true);
			StartCoroutine (ReloadLevel());
		}
	}

	public void Pulo(){
		//Pula
		if (!pulando && isAlive) {
			if(PlayerPrefs.GetString("musica")!="desativado"){
				somPulo.Play ();
			}

			pulando = true;
			rb2d.AddForce (new Vector2 (0f, ForcaPulo));

			Invoke ("PuloAux",0.9f);
		}
	}

	void PuloAux(){
		pulando = false;
	}

	public void Fire(){

		if (Time.time > proximoTiro && QuantidadeBala>0) {
			//Animação de tiro
			QuantidadeBala--;

			if(PlayerPrefs.GetString("musica")!="desativado"){
				somTiro.Play ();
			}

			proximoTiro = Time.time+tempoTiro;
			//Cria tiro
			GameObject cloneBala = Instantiate (Bala, origemBala.position, origemBala.rotation);

			//Vira a bala
			if (sprite.flipX) {
				cloneBala.transform.eulerAngles = new Vector3 (0,0,180);
			}
		}

	}
	void Flip (){
		//Vira jogador e local onde bala é criada
		sprite.flipX = !sprite.flipX;
		if (!sprite.flipX) {
			origemBala.position = new Vector3 (this.transform.position.x + 0.48f, origemBala.position.y, origemBala.position.z);

		} else {
			origemBala.position = new Vector3 (this.transform.position.x - 0.48f, origemBala.position.y, origemBala.position.z);

		}
	}

	IEnumerator Damage(){
		//Jogador fica invuneravel
		invunerabilidade = true;
		//Jogador pisca
		for (int i = 0; i < 5; i ++) {
			sprite.color = Color.red;
			yield return new WaitForSeconds (0.2f);
			sprite.color = Color.white;
			yield return new WaitForSeconds (0.2f);
		}
		sprite.color = Color.white;
		//Jogador fica vuneravel
		invunerabilidade = false;
	}

	public void DamagePlayer(){

		if(isAlive){
			//Ativa a vunerabilidade
			invunerabilidade = true;
			//Perde vida
			vidas--;

			StartCoroutine (Damage());

			if(vidas < 1){
				//Morte
				isAlive = false;
				anim.SetBool ("Morto", true);
				StartCoroutine (ReloadLevel());
			}
		}
	}

	 IEnumerator ReloadLevel(string message="")
	{
		yield return new WaitForSeconds(1.5f);
		//Send Data
		SendData.Send(Expressoes.questao,"","","yes", message);
		//Recarrega fase atual
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}

	void OnCollisionEnter2D(Collision2D obj){
		if (obj.transform.tag == "Plataforma") {
			transform.parent = obj.transform;
		}
		if (obj.transform.tag == "Vida") {
			if(vidas<3){
				vidas++;
			}
			Destroy (obj.gameObject);
		}
		if (obj.transform.tag == "Rio") {
			//Send Data
			isAlive = false;
			anim.SetBool ("Morto", true);
			StartCoroutine (ReloadLevel("Rio"));
		}


	}
	void OnCollisionExit2D(Collision2D obj){
		if (obj.transform.tag == "Plataforma") {
			transform.parent = null;
		}
	}

	IEnumerator IndiceVida(){
		if(indice < 5){
			indice = 300;
			vidas--;
		}
		if(indice > 300){
			indice = 300;
			if(vidas<3){
				vidas++;
			}
		}

		BarraVida.value = indice;
		yield return new WaitForSeconds (0.15f);
		indice--;
		StartCoroutine (IndiceVida());
	}

}