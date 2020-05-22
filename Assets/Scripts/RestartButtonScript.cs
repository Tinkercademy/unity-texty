using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartButtonScript : MonoBehaviour
{
	
	private Button button;
	
    // Start is called before the first frame update
    void Start()
    {
        button = gameObject.GetComponent<Button>();
		button.onClick.AddListener(Restart);
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
