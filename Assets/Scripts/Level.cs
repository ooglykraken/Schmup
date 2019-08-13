using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level : MonoBehaviour {

	public List<Tile> tiles = new List<Tile>();
	
	public void Awake() {
		XMLNode xml = XMLParser.Parse((Resources.Load("level", typeof(TextAsset)) as TextAsset).text);
		
		XMLNodeList tilesXML = xml.GetNodeList("doc>0>tiles>0>tile");
		for (int i = 0; i < tilesXML.Count; i++) {
			XMLNode tileXML = tilesXML[i] as XMLNode;
			Tile tile = new Tile();
			tile.type = tileXML.GetValue("@type");
			
			XMLNodeList enemiesXML = tileXML.GetNodeList("enemies>0>enemy");
			for (int j = 0; j < enemiesXML.Count; j++) {
				XMLNode enemyXML = enemiesXML[j] as XMLNode;
				Enemy enemy = new Enemy();
				enemy.type = enemyXML.GetValue("@type");
				enemy.position = new Vector3(
					float.Parse(enemyXML.GetValue("@x")),
					0,
					float.Parse(enemyXML.GetValue("@z"))
				);
				tile.enemies.Add(enemy);
			}
			
			tiles.Add(tile);
		}
	}
	
	private static Level instance = null;
	
	public static Level Instance(){
		if(instance == null){
			instance = (new GameObject("Level")).AddComponent<Level>();
		}
		return instance;
	}
}

public class Tile{
	public string type = "";
	public List<Enemy> enemies = new List<Enemy>();
}

public class Enemy{
	public Vector3 position = Vector3.zero;
	public string type = "";
}