using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;


public class LoginManager : MonoBehaviour
{
    [Header("Supabase Config")]
    public string supabaseUrl = "https://ujsljeyrsxniajsqizar.supabase.co";
    public string supabaseAnonKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InVqc2xqZXlyc3huaWFqc3FpemFyIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NDg4ODYyODMsImV4cCI6MjA2NDQ2MjI4M30.BU8FpcYIloxFRiE7tkOHnV9t8km7Hc9t1J3pTT_HHK0";

    [Header("Input Fields")]
    public TMP_InputField emailInputField;
    public TMP_InputField passwordInputField;


    private string accessToken = "";
    public GameObject loginPanel;
    public GameObject toolbarPanel;
    public GameObject memoryManager;
    public GameObject memoryBubblePrefab;

    public string userPassword;
    public string userEmail;


    void Start()
    {
        loginPanel.SetActive(true);

        toolbarPanel.SetActive(false);
        memoryManager.SetActive(false);
        memoryBubblePrefab.SetActive(false);
        CenterLoginPanel();

    }

    void CenterLoginPanel()
    {
        Transform cam = Camera.main.transform;
        loginPanel.transform.position = cam.position + cam.forward * 1.2f;
        loginPanel.transform.rotation = Quaternion.LookRotation(cam.forward);
    }

    public void OnLoginButtonPressed()
    {
        userEmail = emailInputField.text.Trim();
        userPassword = passwordInputField.text;

        if (string.IsNullOrEmpty(userEmail) || string.IsNullOrEmpty(userPassword))
        {
            Debug.LogWarning("Email and password must not be empty.");
            return;
        }

        StartCoroutine(LoginUser());
    }


    public void OnSignUpButtonPressed()
    {
        userEmail = emailInputField.text;
        userPassword = passwordInputField.text;
        StartCoroutine(SignupUser());
    }

    IEnumerator SignupUser()
    {
        string signUpUrl = $"{supabaseUrl}/auth/v1/signup";
        string jsonBody = $"{{\"email\":\"{userEmail}\",\"password\":\"{userPassword}\"}}";
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);

        UnityWebRequest request = new UnityWebRequest(signUpUrl, "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("apikey", supabaseAnonKey);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Signup failed: " + request.error);
            Debug.LogError(request.downloadHandler.text);
        }
        else
        {
            Debug.Log("logging in");
            StartCoroutine(LoginUser());
        }
    }

    IEnumerator LoginUser()
    {
        string loginUrl = $"{supabaseUrl}/auth/v1/token?grant_type=password";
        string jsonBody = $"{{\"email\":\"{userEmail}\",\"password\":\"{userPassword}\"}}";
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);

        UnityWebRequest request = new UnityWebRequest(loginUrl, "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("apikey", supabaseAnonKey);

        yield return request.SendWebRequest();

        string responseText = request.downloadHandler.text;

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Login request failed: " + request.error);
            Debug.LogError("Response: " + responseText);
            yield break;
        }

        if (!responseText.Contains("access_token"))
        {
            Debug.LogWarning("Access token not found in response.");
            yield break;
        }

        AuthResponse response = null;
        try
        {
            response = JsonUtility.FromJson<AuthResponse>(responseText);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to parse JSON: " + e.Message);
            yield break;
        }

        accessToken = response.access_token;
        PlayerPrefs.SetString("access_token", accessToken);

        if (loginPanel && toolbarPanel && memoryBubblePrefab && memoryManager)
        {
            loginPanel.SetActive(false);
            toolbarPanel.SetActive(true);
            memoryBubblePrefab.SetActive(true);
            memoryManager.SetActive(true);
        }
        else
        {
            Debug.LogWarning("UI references not set properly.");
        }
    }





    [System.Serializable]
    private class AuthError
    {
        public int code;
        public string error_code;
        public string msg;
    }


    [System.Serializable]
    private class AuthResponse
    {
        public string access_token;
        public string token_type;
        public string expires_in;
        public string refresh_token;
        public string user;
    }


}
