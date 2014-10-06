using UnityEngine;
using System.Collections;

public class PrefabPropertiesExample : MonoBehaviour {

	Hero HeroPropertiesAttribute;
	public void InsertHeroProperties(Hero PropertiesHero)
	{
		HeroPropertiesAttribute=PropertiesHero;
	}

	void main(){

	}

	void Awake(){

	}

	public bool showProperties=false;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		if(showProperties&&HeroPropertiesAttribute!=null)
		{
			GUILayout.Label("ID:"+HeroPropertiesAttribute.ID.ToString());
			GUILayout.Label("HEROTYPE:"+HeroPropertiesAttribute.HeroTipe);
			GUILayout.Label("HERONAME:"+HeroPropertiesAttribute.NAMA);
			GUILayout.Label("HP:"+HeroPropertiesAttribute.Health.ToString());
			GUILayout.Label("ATK:"+HeroPropertiesAttribute.Attack.ToString());
		}
		if(GUILayout.Button("SHOW ATTRIBUTE"))
		{
			showProperties=!showProperties;
		}
	}
}
