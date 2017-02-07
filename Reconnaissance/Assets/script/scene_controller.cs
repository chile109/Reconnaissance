using UnityEngine;
using UnityEngine.SceneManagement;

public class scene_controller : MonoBehaviour {
    public string scene_ID = "success";
    public static int scene_index;
    socket_console socket_console;

    public void NextMovie()
    {        
        SceneManager.LoadSceneAsync(scene_ID);
    }
    public void GetName()
    {
        SceneManager.LoadSceneAsync(getFile.fileName);
    }

    public void Right()
    {
        scene_index = Application.loadedLevel;
        if(scene_index != 5)
        SceneManager.LoadSceneAsync(scene_index + 1);

    }

    public void Left()
    {
        scene_index = Application.loadedLevel;
        if (scene_index != 0)
            SceneManager.LoadSceneAsync(scene_index - 1);

    }

    public void Back()
    {
        socket_console = GameObject.Find("SocketController(Clone)").GetComponent<socket_console>();
        socket_console.TimeOut();
        SceneManager.LoadSceneAsync("title");
    }
}
