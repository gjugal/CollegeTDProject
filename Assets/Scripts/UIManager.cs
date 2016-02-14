using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : MonoBehaviour {

    public static UIManager UM;
    public enum Scene {START_MENU, GAME, SHOP};
    private Scene currentScene;

    void Awake()
    {
        if (!UM)
        {
            UM = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentScene = Scene.START_MENU;
    }



    public void LoadScene(int scene) {
        if (scene == Constants.GAME_SCENE)
        {
            SceneManager.LoadScene("FirstScene");
            currentScene = Scene.GAME;
        }
        else if (scene == Constants.START_MENU_SCENE)
        {
            SceneManager.LoadScene("StartMenu");
            currentScene = Scene.START_MENU;
        }
        else if (scene == Constants.SHOP_MENU_SCENE)
        {
            SceneManager.LoadScene("BuyScene");
            currentScene = Scene.GAME;
        }
    }

    public static class Constants
    {
        public const int START_MENU_SCENE = 0;
        public const int SHOP_MENU_SCENE = 1;
        public const int GAME_SCENE = 2;
    }

}
