using UnityEngine;
using System.Collections;

public class StartMenuManager : MonoBehaviour {

    public void ChangeLevel(int i)
    {
        UIManager.UM.LoadScene(i);
    }
}
