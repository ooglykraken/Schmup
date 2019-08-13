using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidMovement : MonoBehaviour {
	
	public GameObject explosion;
	
	// private SphereCollider explosionRadius;
	
	private List<GameObject> inRadius = new List<GameObject>();
	
	public int scorePoints = 10;
	private int explosionDamage = 2;
	private int hitpoints = 1;
	
	private float movementSpeed = 4f;
	private float lateralDirection = 0;
	private float cooldown = 30;

	private void Awake(){
		lateralDirection = Random.Range(0,3) - 1;
		// explosionRadius = gameObject.GetComponent<SphereCollider>();
	}
	
	private void FixedUpdate(){
		if(hitpoints <= 0){
			Die();
		}
	
		if(Movement.Instance() == null)
			return;
		
		cooldown -= 1;
		
		if(Camera.main.WorldToViewportPoint(transform.position).y < 0){
			// if(Random.Range(0, 100) > 75){
			// Explode();
			// }
			
			// Instantiate(explosion, transform.position, transform.rotation);
			Destroy(gameObject);
			}

		Move();
	}
	
	private void Die(){
		Score.Instance().playerScore += scorePoints;
		
		if(Random.Range(0, 100) > 75){
			Explode();
		}
		
		Instantiate(explosion, transform.position, transform.rotation);
		Destroy(gameObject);

	}
	
	private void Explode(){
		foreach(GameObject g in inRadius){
			if(g == null){
				return;
			}
			
			if(g.tag == "Player"){
				if(g.transform.Find("Shield").GetComponent<Shield>().isActive){
				} else {
					Movement.Instance().hitpoints -= explosionDamage;
				}
			} else {
				if(g.name == "Asteroid"){
					g.GetComponent<AsteroidMovement>().hitpoints -= explosionDamage;
				}else{
					g.GetComponent<EnemyMovement>().hitpoints -= explosionDamage;
				}
			}
		}
	}
	
	private void Move(){
		if(transform.position.x > 19.5 || transform.position.x < -19.5){
			lateralDirection *= -1;
		}
		GetComponent<Rigidbody>().velocity = new Vector3(lateralDirection * movementSpeed, 0f, -1 * movementSpeed);
	}
	
	private void OnCollisionEnter(Collision c){
		if(c.gameObject.tag == "Blaster" || c.gameObject.tag == "Shield"){
			Die();
		}else{
			lateralDirection *= -1;
			//rigidbody.AddForce(0f, 0f, .0005f * lateralDirection);
		}
	}
	
	private void OnTriggerStay(Collider c){
		if(c.tag == "Enemy" || c.tag == "Player"){
			inRadius.Add(c.gameObject);
		}
	}
	
	private void OnTriggerExit(Collider c){
		if(inRadius.Contains(c.gameObject)){
			inRadius.Remove(c.gameObject);
		}
	}
}

