using UnityEngine;
using System.Collections;
using UnityEditor;

public class RPG_HERO_P1 : RPG_EDITOR {
	RPG_EDITOR parentEditor;
	Vector2 ScrollPositionEditor2;
	public void childEditor(RPG_EDITOR parent)
	{
		parentEditor=parent;
	}

	static void Init () {
		// Get existing open window or if none, make a new one:
		RPG_HERO_P1 window = (RPG_HERO_P1)EditorWindow.GetWindow (typeof (RPG_HERO_P1));
		window.name="RPG_BATTLE_EDITOR";

		if(window==null)
		{
			window.Close();
		}
	}
	
	void OnGUI()
	{
		EditorGUILayout.LabelField("===============================",EditorStyles.boldLabel);
		EditorGUILayout.LabelField("Player 1 Chosen Characters Field",EditorStyles.boldLabel);
		EditorGUILayout.LabelField("===============================",EditorStyles.boldLabel);
		ScrollPositionEditor2=EditorGUILayout.BeginScrollView(ScrollPositionEditor2);
		if(parentEditor.TinkerPlayer_1.Count>0)
		{
			for(int a=0;a<parentEditor.TinkerPlayer_1.Count;a++)
			{
				EditorGUILayout.LabelField("===============================",EditorStyles.boldLabel);
				EditorGUILayout.LabelField(a+1+". Chosen Character Parameter Field",EditorStyles.boldLabel);
				EditorGUILayout.LabelField("===============================",EditorStyles.boldLabel);
				EditorGUILayout.LabelField("ID :",parentEditor.TinkerPlayer_1[a].ID.ToString());
				EditorGUILayout.LabelField("NAMA :",parentEditor.TinkerPlayer_1[a].NAMA);
				EditorGUILayout.LabelField("Tipe_Hero :",parentEditor.TinkerPlayer_1[a].HeroTipe);
				EditorGUILayout.LabelField("HP :",parentEditor.TinkerPlayer_1[a].Health.ToString());
				EditorGUILayout.LabelField("MP :",parentEditor.TinkerPlayer_1[a].Mana.ToString());
				EditorGUILayout.LabelField("ATK :",parentEditor.TinkerPlayer_1[a].Attack.ToString());
				EditorGUILayout.LabelField("DEF :",parentEditor.TinkerPlayer_1[a].Defense.ToString());
				EditorGUILayout.LabelField("REC :",parentEditor.TinkerPlayer_1[a].Recovery.ToString());
				EditorGUILayout.LabelField("SPD :",parentEditor.TinkerPlayer_1[a].Speed.ToString());
				
				if(GUILayout.Button("UNCHOSEN HERO"))
				{
					parentEditor.TinkerPlayer_1.RemoveAt(a);
				}
				EditorGUILayout.LabelField("-------------------------------",EditorStyles.boldLabel);
			}
		}
		if(parentEditor==null)
		{
			Close();
		}
		EditorGUILayout.EndScrollView();

	}
}
