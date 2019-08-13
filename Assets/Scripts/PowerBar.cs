using UnityEngine;
using System.Collections;

public class PowerBar : MonoBehaviour {

	public Vector3 originalScale;
	
	public Color originalColor;
	
	public void Awak(){
		originalScale = transform.localScale;
		originalColor = GetComponent<MeshRenderer>().material.color;
		
		transform.localScale = new Vector3(0f, transform.localScale.y, transform.localScale.z);
	}
	
	public void Update(){
		
	}
	
	private static PowerBar instance = null;
	
	public static PowerBar Instance(){
		if(instance = null){
			instance = GameObject.Find("PowerBar").GetComponent<PowerBar>();
		}
		
		return instance;
	}
}
