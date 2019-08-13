using UnityEngine;
using System.Collections;

public class Gameplay : MonoBehaviour {
	
	private GameObject[] tiles = new GameObject[2];
	
	// public Movement player;
	
	public int gametime;
	
	private float spacing = 40f;
	
	private bool initialLoad = true;
	
	public bool playerLiving = true;
	
	private bool popupActive;
	
	public Vector3 returnPoint = Vector3.zero;
	
	private Transform scrollables;
	
	private TextMesh txtLives;
	
	public int lives;
	
	public void Awake(){
		scrollables = transform.Find("scrollables");
		txtLives = GameObject.Find("TxtLives").GetComponent<TextMesh>();
		
		popupActive = false;
		
		Resurrect();
		lives = 3;
		
		Load();
		Load();
	}
	
	public void Update(){
		gametime++;
		
		if(!Movement.Instance()){
			playerLiving = false;
		}
		
		if(lives <= 0 && !playerLiving && !Popup.Instance() && !popupActive){
			Popup p = Instantiate(Resources.Load("UI/Popup_GameOver", typeof(Popup)) as Popup) as Popup;
			
			p.transform.parent = GameObject.Find("UI Camera").transform;
			p.transform.localPosition = new Vector3(0f, 0f, 10f);
			
			popupActive = true; 
						
			GameObject.Find("TxtLives").GetComponent<MeshRenderer>().enabled = false;
			GameObject.Find("Score").transform.position = new Vector3(-4.5f, -.5f, 10f);
			return;
		}

		if(!playerLiving && Input.GetKeyDown("r"))
			Resurrect();
		
		// if(!Movement.Instance()){
			// playerLiving = false;
			// return;
		// }
			
		if(playerLiving){
			returnPoint = Movement.Instance().transform.position;
		}
	}
	
	public void Resurrect(){
			Debug.Log("Resurrect");
	
			Movement instance = Instantiate(Resources.Load("Character", typeof (Movement)) as Movement) as Movement;
			// player = instance;
			instance.transform.position = new Vector3(0f, 0f, Camera.main.transform.position.z);
			instance.name = instance.name.Split("("[0])[0];
			playerLiving = true;
			
			lives--;
	}
	
	public void LateUpdate(){
		txtLives.text = "Reassemblies: " + lives.ToString();
	
		if(Camera.main.transform.position.z > tiles[0].transform.position.z + (spacing * .9f)){
			Load();
		}
		
		float offset = 10000;
		if(Camera.main.transform.position.z > offset){
			foreach (Object o in scrollables) {
				Transform t = o as Transform;
				t.position -= new Vector3(0, 0, offset);
			}
		}
	}
	
	private void Load(){
		if (tiles[1] != null) {
			Destroy(tiles[0]);
			tiles[0] = tiles[1];
		}
		
		int randomTile = Random.Range(1, Level.Instance().tiles.Count);
		
		if(initialLoad){
			randomTile = 0;
			initialLoad = false;
		}
		
		Tile t = Level.Instance().tiles[randomTile];
		
		GameObject tile = Instantiate(Resources.Load("Tiles/" + t.type, typeof(GameObject)) as GameObject) as GameObject;
		tile.transform.parent = transform.Find("Scrollables/Tiles");
		tile.name = tile.name.Split("("[0])[0];
		Vector3 position = Vector3.zero;
		if (tiles[0] != null)
			position.z = tiles[0].transform.position.z + spacing;
		tile.transform.position = position;
		
		for(int i = 0; i < t.enemies.Count; i++){
			Enemy e = t.enemies[i];
			GameObject enemy = Instantiate(Resources.Load("Enemies/" + e.type, typeof(GameObject)) as GameObject) as GameObject;
			enemy.transform.parent = transform.Find("Scrollables/Enemies");
			enemy.name = enemy.name.Split("("[0])[0];
			enemy.transform.position = tile.transform.position + e.position;
		}
		
		if (tiles[0] == null){
			tiles[0] = tile;
		}
		else{
			tiles[1] = tile;
		}
	}
	
	private static Gameplay instance = null;
	
	public static Gameplay Instance(){
		if(instance == null){
			instance = GameObject.FindObjectOfType<Gameplay>();
		}
		
		return instance;
	}
}
