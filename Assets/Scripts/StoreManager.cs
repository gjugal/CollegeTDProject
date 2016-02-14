using UnityEngine;
using System.Collections;

public class StoreManager : MonoBehaviour {

	public void ChangeLevel(int i)
    {
        UIManager.UM.LoadScene(i);
    }
}
