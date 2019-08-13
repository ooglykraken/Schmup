using UnityEngine;
using System.Collections;

public class Blaster : MonoBehaviour {
	
	public Transform owner;
	
	public int duration = 45;
	
	public float movementSpeed = 16f;
	
	public void Awake(){
		Move();
	}
	
	private void Move(){
		if(GetComponent<Rigidbody>())
			GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, movementSpeed);
	}
	
	public void Update(){
		
		Vector3 cameraRelativePosition = Camera.main.WorldToViewportPoint(transform.position);
	
		if(cameraRelativePosition.y < 0 || cameraRelativePosition.y > 1){
			Destroy(gameObject);
		}
	}
	
	public void FixedUpdate(){
	
		if(duration <= 0){
			Destroy(gameObject);
		}
		duration -= 1;
	}
	
	public void OnCollisionEnter(Collision c){
		// Debug.Log(c.gameObject.tag);
		if(c.gameObject.tag == "Shield" && owner == Movement.Instance().transform){
			
		} else {
			Destroy(gameObject);
		}
	}
	
	private static Blaster instance = null;
	
	public static Blaster Instance(){
		if(instance == null){
			instance = (new GameObject("Blaster")).AddComponent<Blaster>();
		}
		return instance;
	}
}
