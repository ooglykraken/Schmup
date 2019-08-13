using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

		private int verticalDirection = 0;
		private int horizontalDirection = 0;
		private int attackCooldown = 0;
		public int hitpoints = 3;
		private int hitpointCap = 3;
		private int invulnerableDuration;
		private int invulnerableTime = 30;

		private bool invulnerable;
		
		public GameObject explosion;
		
		public void Awake(){
			hitpoints = hitpointCap;
			invulnerable = false;
			StartInvulnerability();
			
			transform.parent = GameObject.Find("Scrollables").transform;
		}
		
		public void Update(){
			if(invulnerable)
				invulnerableDuration--;
			
			if(invulnerableDuration <= 0){
				invulnerable = false;
			}
		}
		
		public void FixedUpdate(){
			if(hitpoints <= 0){
				Die();
			}
			
			verticalDirection = (int)Input.GetAxisRaw("Vertical");
			horizontalDirection = (int)Input.GetAxisRaw("Horizontal");
			
			attackCooldown -= 1;
			
			if(Input.GetKey("space")){
				if(attackCooldown <= 0){
					Attack();
				}
			}
			
			Move();
		}
		
		private void Die(){
			Instantiate(explosion, transform.position, transform.rotation);
			Destroy(gameObject);
		}
		
		private void Move(){
			float verticalSpeed = 7f;
			float horizontalSpeed = 10f;
			float normalSpeed = 9f;
			
			GetComponent<Rigidbody>().velocity = new Vector3(horizontalDirection * horizontalSpeed, 0f, normalSpeed + (verticalDirection * verticalSpeed));
		}
		
		private void Damaged(int damage){
			hitpoints -= damage;
			StartInvulnerability();
		}
		
		private void StartInvulnerability(){
			invulnerableDuration = invulnerableTime;
			invulnerable = true;
		}
		
		private void Attack(){
			Blaster attack = Instantiate(Resources.Load("Projectiles/Blaster", typeof (Blaster)) as Blaster) as Blaster; 

			attack.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1f);
			attack.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 2f * attack.movementSpeed);
			attack.GetComponent<Blaster>().owner = transform;
			GetComponent<AudioSource>().Play();
			attackCooldown = 15;
		}
		
		public void OnCollisionEnter(Collision c){
			if(!invulnerable)
				switch(c.gameObject.tag){
					case("Blaster"):
						Damaged(1);
						break;
					case("Enemy"):
						Damaged(1);
						break;
					default:
						break;
				}
		}
		
	private static Movement instance = null;
	
	public static Movement Instance(){
		if (instance == null){
			instance = GameObject.FindObjectOfType<Movement>();
		}
		
		return instance;
	}
}
