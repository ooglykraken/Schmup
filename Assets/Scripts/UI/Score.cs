using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

	public int playerScore;
	
	public TextMesh textMesh;
	
	public bool isChanged;
	
	public void Awake(){
		textMesh = GetComponent<TextMesh>();
	}
	
	public void Update(){
		// switch(playerScore){
			// case 1000:
				// Gameplay.Instance().lives++;
				// break;
			// case 2000:
				// Gameplay.Instance().lives++;
				// break;
			// default:
				// break;
		// }
	
		textMesh.text = "Score: " + playerScore;
	}
	
	private static Score instance = null;
	
	public static Score Instance(){
		if (instance == null)
			instance = GameObject.FindObjectOfType<Score>();
		return instance;
	}
	
}
