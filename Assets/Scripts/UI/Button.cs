using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	public GameObject downTarget;
	
	public string downArgument;
	public string downFunction;
	
	private Vector3 buttonMax;
	private Vector3 buttonMin;
	
	public void OnMouseOver(){
		if(Input.GetMouseButtonDown(0)){
			if (downTarget) {
				if (downFunction.Length > 0) {
					if (downArgument.Length > 0)
						downTarget.SendMessage(downFunction, downArgument, SendMessageOptions.DontRequireReceiver);
					else
						downTarget.SendMessage(downFunction, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
	}
	
	public void Awake(){
		Bounds buttonBounds = gameObject.GetComponent<BoxCollider>().bounds;
		buttonMax = GameObject.Find("UI Camera").GetComponent<Camera>().ScreenToWorldPoint(buttonBounds.max); 
		buttonMin =  GameObject.Find("UI Camera").GetComponent<Camera>().ScreenToWorldPoint(buttonBounds.min); 
	}
	
	public void Update(){
		Vector3 cursorPosition =  GameObject.Find("UI Camera").GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
		
		if(hovering(cursorPosition)){
			if(Input.GetMouseButtonDown(0)){
				if (downTarget) {
					if (downFunction.Length > 0) {
						if (downArgument.Length > 0)
							downTarget.SendMessage(downFunction, downArgument, SendMessageOptions.DontRequireReceiver);
						else
							downTarget.SendMessage(downFunction, SendMessageOptions.DontRequireReceiver);
					}
				}
			}
		}
	}
	
	private bool hovering(Vector3 cursor){
		if(cursor.x <buttonMax.x && cursor.x > buttonMin.x && cursor.x <buttonMax.y && cursor.x > buttonMin.y){
			Debug.Log("Hovering over button");
			return true;
		}
		
		return false;
	}
	
}
