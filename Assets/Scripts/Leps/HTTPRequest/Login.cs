using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    // email, password
    [SerializeField] TMP_InputField email;
    [SerializeField] TMP_InputField password;
    [SerializeField] TextMeshProUGUI textFailed;

    public async void onLogin()
    {
        var _email = email.text;
        var _password = password.text;
        // Gọi API
        // UnityWebRequest POST
        var url = "https://fpoly-hcm.herokuapp.com/api/auth/login";
        var form = new WWWForm();
        form.AddField("email", _email);
        form.AddField("password", _password);

        var result = await PostAPI(url, form);
        if (result)
        {
            //textFailed.gameObject.SetActive(true);
        }
        else
        {
            //textFailed.gameObject.SetActive(false);
            SceneManager.LoadScene("Map1");
        }
        // neu thanh cong chuyen sang scne

    }
    public void onCancel()
    {

    }

    public async Task<bool> PostAPI(string url, WWWForm form)
    {
        try
        {
            using var www = UnityWebRequest.Post(url, form);
            var operation = www.SendWebRequest();
            while (!operation.isDone) await Task.Yield();
            if (www.result != UnityWebRequest.Result.Success)
                Debug.Log(www.error);
            var result = www.downloadHandler.text;
            var model = LoginResponseModel.CreateFromJSON(result);
            return model.error;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return default;
        }
    }

    [System.Serializable]
    public class LoginResponseModel
    {
        public bool error;
        public int statusCode;
        public static LoginResponseModel CreateFromJSON(string jsonString)
        {
            return JsonUtility.FromJson<LoginResponseModel>(jsonString);
        }
    }
}
