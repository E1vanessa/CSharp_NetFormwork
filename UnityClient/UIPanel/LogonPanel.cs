using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LogonPanel : MonoBehaviour
{
    public LoginRequest loginRequest;
    public TMP_InputField user, pass;
    public Button logonBtn;

    private void Start()
    {
        logonBtn.onClick.AddListener(OnLogonClik);    
    }

    private void OnLogonClik()
    {
        if (user.text == "" || pass.text == "")
        {
            Debug.LogWarning("账号或密码不能为空");
            return;
        }
        loginRequest.SendRequest(user.text, pass.text);
    }
}
