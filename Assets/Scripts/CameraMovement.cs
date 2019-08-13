using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	private float movementSpeed = 7.5f;
	
	private float spaceFromSide = 1/8;
	
	private int rightBound;
	private int leftBound;
	private int topBound;
	private int botBound;
	
	private Rigidbody rigidbody;
	
	public void Awake(){
		rightBound = 1;
		leftBound = 0;
		topBound = 1;
		botBound = 0;
		
		rigidbody = gameObject.GetComponent<Rigidbody>();
	}
	
	public void LateUpdate(){
		Move();
	}
	
	private void Move(){
		if(Movement.Instance() == null)
			return; 
			
		transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + (movementSpeed * Time.deltaTime));
			
		Transform playerTransform = Movement.Instance().transform;
			
		Vector2 playerScreenPosition = Camera.main.WorldToViewportPoint(playerTransform.position);
		// Vector2 screenPointOffsetLeft = Camera.main.ViewportToWorldPoint(new Vector2(leftBound,playerScreenPosition.y));
		// Vector2 screenPointOffsetRight = Camera.main.ViewportToWorldPoint(new Vector2(rightBound, playerScreenPosition.y));
		// Debug.Log(playerScreenPosition + " there he goes to here " + screenPointOffsetLeft);
		
		
		// Vector3 screenPointOffsetBot = Camera.main.ViewportToWorldPoint(new Vector2(.5f, .5f));
		
		if(playerScreenPosition.x > rightBound){
			playerTransform.position = new Vector3(-playerTransform.position.x + 1f, playerTransform.position.y, playerTransform.position.z);
		}else if(playerScreenPosition.x < leftBound){
			playerTransform.position = new Vector3(-playerTransform.position.x - 1f, playerTransform.position.y, playerTransform.position.z);
		}
		if(playerScreenPosition.y > topBound - .05f){
			playerTransform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z - .1f);
		}else if(playerScreenPosition.y < botBound + .05f){
			playerTransform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z + .1f);
		}
			
		// if(playerScreenPosition.y > topBound - .05f){
			// Vector3 newScreenPoint = Camera.main.ViewportToWorldPoint(new Vector2(0f, playerScreenPosition.y - .45f));
			// transform.position = new Vector3(transform.position.x, transform.position.y, newScreenPoint.y);
			// rigidbody.velocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y, Movement.Instance().gameObject.GetComponent<Rigidbody>().velocity.z);
		// }else if(){
			// Vector3 newScreenPoint = Camera.main.ViewportToWorldPoint(new Vector2(0f, playerTransform.position.y + .45f));
			// transform.position = new Vector3(transform.position.x, transform.position.y, newScreenPoint.y);
			// rigidbody.velocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y, Movement.Instance().gameObject.GetComponent<Rigidbody>().velocity.z);
		// }		
		
		
		// playerScreenPosition.y > topBound - .05f
		// if(transform.position.y  >  main.transform.position.y + verticalOffset){
			// main.transform.position = new Vector3(main.transform.position.x, transform.position.y - verticalOffset, main.transform.position.z);
				
		// Vector3 bot = Camera.main.ViewportToWorldPoint(new Vector2( .5f, .55f - playerScreenPosition.y));
		// Vector3 top = Camera.main.ViewportToWorldPoint(new Vector2( .5f, playerScreenPosition.y - .45f));
	
		// float t = playerTransform.position.z - s.y;
		// float u = s.y - playerTransform.position.z;
	
		// if(playerScreenPosition.y > topBound - .05f){
			// transform.position = new Vector3(transform.position.x, transform.position.y, top.y);	
		// } else if(playerScreenPosition.y < botBound + .05f){
			// transform.position = new Vector3(transform.position.x, transform.position.y, bot.y);
		// }
		
		
		if(transform.position.x > 4){
			transform.position = new Vector3(4, transform.position.y, transform.position.z);
		}else if(transform.position.x < -4){
			transform.position = new Vector3(-4, transform.position.y, transform.position.z);
		}
		
		
	}
	
	// private void OnCollisionEnter(Collision c){
		// if(c.gameObject.tag == "Player"){
			// Movement.Instance().transform.position = new Vector3(c.contacts[0].point.x, Movement.Instance().transform.position.y, Movement.Instance().transform.position.z);
		// }
	// }
	
	private static CameraMovement instance = null;
	
	public static CameraMovement Instance(){
		if(instance == null){
			instance = (new GameObject("CameraMovement")).AddComponent<CameraMovement>();
		}
		return instance;
	}
}
