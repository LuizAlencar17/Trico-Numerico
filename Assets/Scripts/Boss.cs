using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour {
	//Vida do inimigo
	public float vida;
	//Feedback da vida
	public Slider sliderVida;
	//Velocidade de movimentação
	public float  velocidade;
	public int vida_maxima = 300;
	//Jogador
	public GameObject jogador;
	//Tempo que ele anda de um lado para outro
	public float tempoNaDirecao=4;
	//Posição inicial do inimigo
	public Vector3 posicaoInicial;
	//Tamanho do caminho que o Boss anda
	public float  tamanhoDoCaminho=6;
	//Direção inicial da movimentação
	public float DirecaoX=1;
	//Variavel para espelhar a imagem do inimigo
	private SpriteRenderer flip;

	public static int indice=0;

	public List <string> Calculos;
	public List <string> VetorCalculos;
	public List <string> VetorRespostas;

	public Text textoOperacao;
	public Text textoBoss;

	public static string resposta_atual;
	public static string operacao_atual;

	//Feedback que indica q o jogador errou
	public GameObject falha;

	public bool aumentaVidaUmaVez;
	public bool podeAndar;

	void Start () {
		aumentaVidaUmaVez = false;
		podeAndar = true;
		posicaoInicial = gameObject.transform.position;

		indice = 0;

		CalculosRespostas ();
		flip = GetComponent<SpriteRenderer>();

		StartCoroutine (EscolheValorParaBoss());
	}
	
	void FixedUpdate () {
		sliderVida.value = vida;
		operacao_atual = textoOperacao.text;
		resposta_atual = textoBoss.text;
		TextoDaOperacao();
		AndaPelaLateral();
		InimigoFicaFurioso();
	}

	void TextoDaOperacao(){
		if(indice<VetorRespostas.Count && vida>1){
			textoOperacao.text = VetorCalculos[indice];
		}else{
			indice = 0;
		}
	}
	//Permite o inimigo ficar com comportamentos mais agressivos
	void InimigoFicaFurioso(){
		//Aumenta a velocidade do inimigo
		if(vida<= vida_maxima / 2 && vida>1){
			velocidade=Random.Range(1,4);
		}
		//Regenera um pedaço de sua vida apenas uma unica vez
		if(vida<=100 && vida>1){
			if(!aumentaVidaUmaVez){
				StartCoroutine(RegeneraVida());
				aumentaVidaUmaVez=true;
			}
		}
	}

	public void TomaDanoDoTrico(){
		float dano = vida_maxima / VetorRespostas.Count;
		vida-= dano;
	}

	void AndaPelaLateral(){
		if(vida>1f)
		{
			if (podeAndar)
			{
				//Movimenta corpo do obeto
				transform.Translate(new Vector3(DirecaoX * velocidade * Time.deltaTime,0,0));  // sempre movimentando
			}
		}
		else{
			//Inimigo morre
			Morreu();
		}
		
	}

	public void Morreu(){
		Animator animador = gameObject.GetComponent<Animator>();
		Rigidbody2D fisica = gameObject.GetComponent<Rigidbody2D>();
		textoOperacao.text = "Completo";
		//Desabilita a animação
		animador.enabled = false;
		//Desativa a gravidade do inimigo
		fisica.gravityScale = 1;

		//Tenta buscar no nome da proxima fase através dos index da scena posterior
		string proximaFase = NameFromIndex(SceneManager.GetActiveScene().buildIndex + 1);
		PlayerPrefs.SetInt(proximaFase, 1);

		StartCoroutine(CarregaProximaFase(proximaFase));
		
	}

	IEnumerator CarregaProximaFase(string fase)
	{
		yield return new WaitForSeconds(1.5f);
		SceneManager.LoadScene(fase);
	}

	//Captura o nome de uma Scene através de index
	string NameFromIndex(int BuildIndex)
	{
		string path = SceneUtility.GetScenePathByBuildIndex(BuildIndex);
		int slash = path.LastIndexOf('/');
		string name = path.Substring(slash + 1);
		int dot = name.LastIndexOf('.');
		return name.Substring(0, dot);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			//Send data
			SendData.Send(operacao_atual, resposta_atual, "","", "enemy");
			//Colisão com o jogador
			Jogador player = other.GetComponent<Jogador> ();
			player.DamagePlayer ();
			player.DamagePlayer ();
		}else
		if (other.gameObject.tag == "Bullet"){ //Colisão com o tricô
			

			if (textoBoss.text == VetorRespostas[indice]) {
				//Send data
				SendData.Send(operacao_atual, resposta_atual, "yes", "", "Fire");

				Jogador.indice += 30;
				Destroy (other.gameObject);
				TomaDanoDoTrico();
				indice++;
			} else {
				//Send data
				SendData.Send(operacao_atual, resposta_atual, "no", "", "Fire");

				GameObject cloneErro = Instantiate (falha, transform.position, transform.rotation);
				Jogador.indice -= 50;

				cloneErro.transform.SetParent(transform);
				StartCoroutine (ErrouResposta(cloneErro));

			}
		}

		//O objeto que entrou em colisão é o limite até onde o boss pode andar?
		if (other.gameObject.tag == "Limite")
		{
			Debug.Log("Limite");
			//Muda direção do boss
			DirecaoX = DirecaoX * -1;
			//Espelha a imagem do boss
			flip.flipX = !flip.flipX;
		}
	}

	IEnumerator ErrouResposta(GameObject other){
	
		for (int i = 0; i < 5; i ++) {
			falha.SetActive (false);
			yield return new WaitForSeconds (0.04f);
			falha.SetActive (true);
			yield return new WaitForSeconds (0.04f);
		}

		Destroy (other.gameObject);
	}

	IEnumerator EscolheValorParaBoss(){
		
		textoBoss.text = VetorRespostas[Random.Range(0,VetorRespostas.Count)];

		yield return new WaitForSeconds (2f);

		StartCoroutine (EscolheValorParaBoss());
	}

	IEnumerator RegeneraVida(){
		for(int i=0; i<7; i++){
			vida+=10;
			Debug.Log("Regenerando");
			yield return new WaitForSeconds (0.4f);
		}
	}

	void CalculosRespostas(){
		for (int j,i = 0; i < Calculos.Count; i++) {
			string palavra = Calculos [i];
			string numero = palavra [0].ToString();
			string resposta = "";

			for(j=0; numero != ":"; j++){
				numero = palavra [j].ToString();
				if (numero != ":") {
					resposta += numero;
				}
			}

			try{
				numero=palavra [j].ToString() + palavra [j+1].ToString();
			}catch{
				numero = palavra [j].ToString ();
			}

			VetorCalculos.Add (resposta.ToString());
			VetorRespostas.Add (numero.ToString());
		}
	}

}
