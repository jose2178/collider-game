using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    
    void Start () {
        		
	}

    void Update()
    {
        
    }

    public void CambiarScena(int escena)
    {
        SceneManager.LoadScene(escena);
    }

}
