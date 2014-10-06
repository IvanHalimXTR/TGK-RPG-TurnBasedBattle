using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public Texture2D BackgroundTexture;
	public Rect UISize;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),BackgroundTexture,ScaleMode.StretchToFill);
		GUI.Label(new Rect(Screen.width/2-50,50,500,75),"BATTLE MANAGER");

	}
}
