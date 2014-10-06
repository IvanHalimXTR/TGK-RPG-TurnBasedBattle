using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class RPG_HERO_TURNS : RPG_BATTLE_EDITOR {
	RPG_BATTLE_EDITOR parentEditor;

	static void Init () {
		// Get existing open window or if none, make a new one:
		RPG_HERO_TURNS window = (RPG_HERO_TURNS)EditorWindow.GetWindow (typeof (RPG_HERO_TURNS));
		window.name="RPG_HERO_TURNS";

	}
	
	public void childEditor(RPG_BATTLE_EDITOR parent)
	{
		parentEditor=parent;
		this.position=new Rect(parentEditor.position.x+parentEditor.position.width,parent.position.y,500f,400f);
	}

	void OnGUI()
	{
		if(parentEditor.HEROTURNORDER.Count>0)
		{
			for(int datas=0;datas<parentEditor.HEROTURNORDER.Count;datas++)
			{
				GUILayout.Label(" HERO ORDER: "+datas+". Tipe Hero: "+parentEditor.HEROTURNORDER[datas].TipeHero +" |Nama Hero: "+parentEditor.HEROTURNORDER[datas].NAMA+" |SPEED: "+ parentEditor.HEROTURNORDER[datas].Speed.ToString() );
			}
		}

		if(parentEditor.temp.Count>0&&parentEditor.P1_Turn)
		{
			for(int datas=0;datas<parentEditor.temp.Count;datas++)
			{
				GUILayout.Label(" P1 HERO ORDER: "+datas+". Tipe Hero: "+parentEditor.temp[datas].TipeHero +" |Nama Hero: "+parentEditor.temp[datas].NAMA+" |SPEED: "+ parentEditor.temp[datas].Speed.ToString() );
			}
		}

		if(parentEditor.temp2.Count>0&&parentEditor.P2_Turn)
		{
			for(int dats=0;dats<parentEditor.temp2.Count;dats++)
			{
				GUILayout.Label(" P2 HERO ORDER: "+dats+". Tipe Hero: "+parentEditor.temp2[dats].TipeHero +" |Nama Hero: "+parentEditor.temp2[dats].NAMA+" |SPEED: "+ parentEditor.temp2[dats].Speed.ToString() );
			}
		}
	}
}
