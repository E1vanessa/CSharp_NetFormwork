using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketGameProtocol;

public class LoginRequest : BaseRequest
{
    public override void Start()
    {
        requestCode = RequestCode.User;
        actionCode = ActionCode.Login;
        base.Start(); 
    }

    public override void OnResponse(MainPack pack)
    {
        switch (pack.Returncode)
        {
            case ReturnCode.Succeed:
                Debug.Log("注册成功");
                break;
            case ReturnCode.Fail:
                Debug.Log("注册失败");
                break;
        }
    }

    public void SendRequest(string user,string pass)
    {
        MainPack pack = new MainPack();
        pack.Requestcode = requestCode;
        pack.Actioncode = actionCode;
        LoginPack loginPack = new LoginPack();
        loginPack.Username = user;
        loginPack.Password = pass;
        pack.Loginpack = loginPack;
        base.SendRequest(pack);
    }
}
