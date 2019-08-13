using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
	
	public GameObject explosion;
	private GameObject gun;
	private new GameObject collider;
	private GameObject model;
	
	private Vector3 target;
	
	public int hitpoints = 1;
	public int scorePoints = 20;
	
	private string style = "passive";
	
	private float tacticCooldown;
	private float timeToTactic = 60f;
	
	private float strafeDistance = 15f;
	
	private float movementSpeed = 4f;
	private float lateralDirection = 0;
	private float cooldown = 30;
	
	private bool targetFound;
	
	private void Awake(){
		lateralDirection = Random.Range(0,3) - 1;
		cooldown += Random.Range(0, 15);
		gun = transform.Find("Gun").gameObject;
		targetFound = false;
		
		VaryEnemy();
	}
	
	private void VaryEnemy(){
	
		movementSpeed += Random.Range(0,2);
		
		timeToTactic += Random.Range(0, 10);
		tacticCooldown = timeToTactic;
		strafeDistance += Random.Range(0, 10);
		int styleRoll = Random.Range(0,2);
		
		switch(styleRoll){
			case 0:
				style = "passive";
				break;
			case 1:
				style = "aggressive";
				break;
			case 2:
				style = "tricky";
				break;
			default:
				Debug.Log("Something went wrong rolling this enemy");
				break;
		}
		
		
	}
	
	private void FixedUpdate(){
		if(hitpoints <= 0){
			Die();
		}
	
		if(Movement.Instance() == null)
			return;
	
		cooldown -= 1;
		tacticCooldown -= 1;
		
		if(targetFound){
			Strafe();
		} else {
			Move();
		}
		
		Vector3 cameraRelativePosition = Camera.main.WorldToViewportPoint(transform.position);
		
		if(cameraRelativePosition.y < 0){
			// Instantiate(explosion, transform.position, transform.rotation);
			Destroy(gameObject);
		}
		
		if(cameraRelativePosition.x < 0){
			transform.position = new Vector3(-transform.position.x - 1f, transform.position.y, transform.position.z);
		} else if(cameraRelativePosition.x > 1){
			transform.position = new Vector3(-transform.position.x + 1f, transform.position.y, transform.position.z);
		}
		
		if(tacticCooldown <= 0){
			Tactic();
		}
		
		if(cooldown <= 0)
			Attack();
			
		
	}
	
	private void Tactic(){
		switch(style){
			case "trickster":
				strafeDistance -= Random.Range(0, 5);
				break;
			case "aggressive":
				GetComponent<Rigidbody>().velocity = 1.6f * movementSpeed * transform.forward;
				break;
			case "passive":
				GetComponent<Rigidbody>().velocity = new Vector3(-GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);
				break;
			default:
				break;
		}
	}
	
	private void Attack(){
		if(!targetFound){
			return;
		}
		
		//target = Movement.Instance().transform.position;
		
		Blaster attack = Instantiate(Resources.Load("Projectiles/EnemyBlaster", typeof (Blaster)) as Blaster) as Blaster; 
		attack.transform.position = gun.transform.position;
		attack.transform.eulerAngles = new Vector3(90f, transform.eulerAngles.y, 0f);
		attack.GetComponent<Rigidbody>().velocity = 16f * transform.forward;
		attack.GetComponent<Blaster>().owner = transform;
		
		if(gameObject != null)
			GetComponent<AudioSource>().Play();
		cooldown = 45;
	}
	
	public void Strafe(){
		
		Vector3 playerPosition = Movement.Instance().transform.position;
		
		float playerDistance = Vector3.Distance(playerPosition, transform.position);
		
		if(playerDistance > strafeDistance){
			Move();
		} else {
			
			if(transform.position.x > 19 || transform.position.x < -19){
				lateralDirection *= -1;
			}
	
			float verticalVelocity = movementSpeed / 2f;
			
			GetComponent<Rigidbody>().velocity = Vector3.Lerp(GetComponent<Rigidbody>().velocity, new Vector3(lateralDirection * movementSpeed, verticalVelocity, movementSpeed), Time.deltaTime);
		}
	}
	
	private void Die(){
		Score.Instance().playerScore += scorePoints;
	
		Instantiate(explosion, transform.position, transform.rotation);
		Destroy(gameObject);
	}
	
	private void Move(){
		if(transform.position.x > 19 || transform.position.x < -19){
			lateralDirection *= -1;
		}
		
		float verticalVelocity = movementSpeed / 2f;
		
		GetComponent<Rigidbody>().velocity = Vector3.Lerp(GetComponent<Rigidbody>().velocity, new Vector3(lateralDirection * movementSpeed, verticalVelocity, -1 * movementSpeed), Time.deltaTime);
		
	}
	
	private void OnCollisionEnter(Collision c){
		if(c.gameObject.tag == "Blaster"){
			Die();
		}else{
			lateralDirection *= -1;
			//rigidbody.AddForce(0f, 0f, .0005f * lateralDirection);
		}
	}
	
	private void OnTriggerStay(Collider c){
		if(c.gameObject.tag == "Player"){
			transform.LookAt(Movement.Instance().transform.position);
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y , transform.eulerAngles.z);
			target = c.transform.position;
			targetFound = true;
		} else if(c.gameObject.tag == "Asteroid"){
			transform.LookAt(c.transform.position);
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y , transform.eulerAngles.z);
			target = c.transform.position;
			targetFound = true;
		} 
	}
	
	public void OnTriggerExit(Collider c){
		if(c.gameObject.tag == "Player" || c.gameObject.tag == "Asteroid"){
			targetFound = false;
		}
	}
}
