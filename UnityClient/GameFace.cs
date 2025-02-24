using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketGameProtocol;

public class GameFace : MonoBehaviour
{
    private ClientManager clientManager;
    public RequestManager requestManager;

    private static GameFace gameFace;
    public static GameFace GetGameFace
    {
        get{ return gameFace; }
    }

    void Awake()
    {
        gameFace = this;
        clientManager = new ClientManager(this);
        requestManager = new RequestManager(this);

        clientManager.OnInit();
        requestManager.OnInit();
    }

    // Update is called once per frame
    private void OnDestroy()
    {
        clientManager.OnDestroy();
        requestManager.OnDestroy();

    }

    public void Send(MainPack pack)
    {
        clientManager.Send(pack);
        print("ÒÑ·¢ËÍ");
    }

    public void HandleResponse(MainPack pack)
    {
        requestManager.HandleResponse(pack);
    }

    public void AddRequest(BaseRequest request)
    {
        requestManager.AddRequest(request);
    }

    public void RemoveRequest(ActionCode action)
    {
        requestManager.RemoveRequest(action);
    }
}
