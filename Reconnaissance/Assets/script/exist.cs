using UnityEngine;
using System.Collections;

public class exist : MonoBehaviour {
    private socket_console socket_console;
    GameObject SocketController;
    // Use this for initialization
    void Awake () {
        if (GameObject.Find("SocketController(Clone)") == null)
        {
            SocketController = (GameObject)Instantiate(Resources.Load("SocketController"));
        }
        else
            SocketController = GameObject.Find("SocketController(Clone)");
    }
	

    public void movie_select(string index)
    {
        socket_console = SocketController.GetComponent<socket_console>();
        socket_console.play_movie(index);
    }

    public void go_title()
    {
        socket_console = SocketController.GetComponent<socket_console>();
        socket_console.TimeOut();
    }

}
