using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
	
	private MeshRenderer model;
	
	public void Awake(){
		model = GetComponent<MeshRenderer>();
	}
	public void Update(){
		int scale = Movement.Instance().hitpoints;
		
		switch(scale){
			case 1:
				model.material.color = Color.red;
				break;
			case 2:
				model.material.color = Color.yellow;
				break;
			case 3:
				model.material.color = Color.green;
				break;
			default:
				// this is 0 health
				break;
		}
		model.transform.localScale = new Vector3(scale, model.transform.localScale.y, model.transform.localScale.z);
	}
}
