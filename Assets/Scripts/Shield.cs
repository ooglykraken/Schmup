using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {
	
	public SphereCollider shieldCollider;
	
	public MeshRenderer shieldModel;
	
	public int shieldCooldown;
	private int cooldownTimer;
	
	public bool isActive;
	
	public void Awake(){
		shieldCooldown = 55;
		shieldCollider = GetComponent<SphereCollider>();
		
		shieldModel = GetComponent<MeshRenderer>();
		
		Deactivate();
		cooldownTimer = 10;
		
		
		
	}
	
	public void Start(){
		
	}
	
	public void Update(){
		if(cooldownTimer <= 0){
			Activate();
		} else {
			cooldownTimer--;
		}
		
		transform.eulerAngles = Vector3.zero;
	}
	
	public void OnCollisionEnter(Collision c){	
		switch(c.gameObject.tag){
			case("Enemy"):
				Deactivate();
				break;
			case("Blaster"):
				if(c.gameObject.GetComponent<Blaster>().owner != transform.parent){
					Deactivate();
				}
				break;
			default:
				break;
		}
	}
	
	public void Activate(){
		isActive = true;
		shieldCollider.enabled = true;
		shieldModel.enabled = true;
	}
	
	public void Deactivate(){
		cooldownTimer = shieldCooldown;
		isActive = false;
		shieldCollider.enabled = false;
		shieldModel.enabled = false;
		Debug.Log("Shields Down!");
	}
}
