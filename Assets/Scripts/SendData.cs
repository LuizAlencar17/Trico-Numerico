using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class SendData : MonoBehaviour
{
    public static MonoBehaviour instance;
    public static float time;
    public static string BASE_URL = "https://teste-57287.firebaseio.com/";
    public static DatabaseReference reference;

    void Awake() {
        instance = this;
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(BASE_URL);
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        time = 0f;
		DontDestroyOnLoad(gameObject);
        //GeneretorData();
    }

    void Update(){
        time += Time.deltaTime;
    }

    class UserData {
        public string name;
        public string age;
        public string code;
        public string genre;
        public string question;
        public string answer;
        public string hit_question;
        public string current_time;
        public string level;
        public string died;
        public string collided_with_enemy;
        public string button_name;
    
        public UserData(
                string name,
                string age,
                string code,
                string genre,
                string question, 
                string answer, 
                string hit_question, 
                string current_time, 
                string level, 
                string died, 
                string collided_with_enemy, 
                string button_name
            ){
            this.name = name;
            this.age = age; 
            this.code = code;
            this.genre = genre;
            this.question = question;
            this.answer = answer;
            this.hit_question = hit_question;
            this.current_time = current_time;
            this.level = level;
            this.died = died;
            this.collided_with_enemy = collided_with_enemy;
            this.button_name = button_name;
        }
    }

    public static void Send(
            string question, 
            string answer, 
            string hit_question, 
            string died, 
            string collided_with_enemy, 
            string button_name = ""
        ){
        string user_device_id = SystemInfo.deviceUniqueIdentifier.ToString();
        string current_time = time.ToString();
        string level = SceneManager.GetActiveScene().name;
        string name = PlayerPrefs.GetString("nome");
        string age = PlayerPrefs.GetString("idade");
        string code = PlayerPrefs.GetString("codigo");
        string genre = PlayerPrefs.GetString("genero");

        UserData user = new UserData(
            name,
            age,
            code,
            genre,
            question, 
            answer, 
            hit_question, 
            current_time, 
            level, 
            died, 
            collided_with_enemy, 
            button_name
        );

        string json = JsonUtility.ToJson(user);

        if(code == ""){
            reference.Child("users").Child("without_code").Child(user_device_id).Push().SetRawJsonValueAsync(json);
        }else{
            reference.Child("users").Child(code).Child(user_device_id).Push().SetRawJsonValueAsync(json);
        }

        
    }

    void GeneretorData (){
        string user_device_id = SystemInfo.deviceUniqueIdentifier.ToString();
        string current_time = time.ToString();
        string level = "Fase";
        string name = "Teste";
        string age = "20";
        string code = "243243";
        string genre = "Masculino";

        List<string> died = new List<string>();
        died.Add("yes");
        died.Add("no");

        List<string> disparo = new List<string>();
        disparo.Add("");
        disparo.Add("[BTN] Atirar");

        for (int i = 0; i<2000; i++) {
            UserData user = new UserData(
                name,
                age,
                code,
                genre,
                "", 
                "", 
                died[Random.Range(0,2)], 
                current_time, 
                level+Random.Range(1,14), 
                died[Random.Range(0,2)], 
                died[Random.Range(0,2)], 
                disparo[Random.Range(0,2)]
            );

            string json = JsonUtility.ToJson(user);

            if(code == ""){
                reference.Child("users").Child("without_code").Child(user_device_id).Push().SetRawJsonValueAsync(json);
            }else{
                reference.Child("users").Child(code).Child(user_device_id).Push().SetRawJsonValueAsync(json);
            }
        }
    }
}