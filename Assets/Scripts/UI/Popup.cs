using UnityEngine;
using System.Collections;

public class Popup : MonoBehaviour {
	
	private void Retry(){
		Application.LoadLevel(Application.loadedLevel);
	}
	
	private void ReturnToMainMenu(){
		Application.LoadLevel("MainMenu");
	}
	
	private void Close(){
		Destroy(gameObject);
		Destroy(this);
	}

private static Popup instance = null;
	
	public static Popup Instance(){
		if(instance == null){
			
		}
		
		return instance;
	}
	
}
