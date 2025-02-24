using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketGameProtocol;
using XLua;

[LuaCallCSharp]
public class BaseRequest : MonoBehaviour
{
    protected RequestCode requestCode;
    protected ActionCode actionCode;

    public ActionCode GetActionCode
    {
        get { return actionCode; }
    }

    public virtual void Start()
    {
        if (!GameFace.GetGameFace.requestManager.requestDict.ContainsKey(actionCode))GameFace.GetGameFace.AddRequest(this);
    }

    public virtual void OnDestroy()
    {
        GameFace.GetGameFace.RemoveRequest(actionCode);
    }

    public virtual void OnResponse(MainPack pack)
    {

    }

    public virtual void SendRequest(MainPack pack)
    {
        GameFace.GetGameFace.Send(pack);
    }
}
