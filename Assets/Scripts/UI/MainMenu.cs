using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	private void OnMouseDown(string argument){
		switch(argument){
			case "StartGame":
				StartGame();
				break;
			case "Instructions":
				Instructions();
				break;
			default:
				break;
		}
	}
	
	private void StartGame(){
		Application.LoadLevel("Shmup");
	}
	
	private void Instructions(){
		Popup p = Instantiate(Resources.Load("UI/Popup", typeof(Popup)) as Popup) as Popup; 
	}
}
