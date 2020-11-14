using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerInformation : MonoBehaviour
{
    public Button botaoConfirmar;

    public InputField inputNome;
    public InputField inputIdade;
    public InputField inputCodigo;
    public Dropdown inputGenero;

    void Update()
    {
        if(
            inputNome.text.Length >1 && 
            inputIdade.text.Length >= 1 && 
            inputGenero.options[inputGenero.value].text.Length >= 1 &&
            int.Parse(inputIdade.text) < 150
        ){
            botaoConfirmar.interactable = true;
        }else{
            botaoConfirmar.interactable = false;

        }
    }

    public void ConfirmarRegistro(){
        PlayerPrefs.SetString("nome",inputNome.text);
        PlayerPrefs.SetString("idade",int.Parse(inputIdade.text).ToString());
        PlayerPrefs.SetString("codigo",inputCodigo.text);
        PlayerPrefs.SetString("genero",inputGenero.options[inputGenero.value].text);
        SceneManager.LoadScene("Menu");
    }
}
