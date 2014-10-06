using UnityEngine;
using System.Collections;
using UnityEditor;
public class RPG_HERO_P2 : RPG_EDITOR {
	RPG_EDITOR parentEditor;
	Vector2 ScrollPositionEditor2;

	public void childEditor(RPG_EDITOR parent)
	{
		parentEditor=parent;
	}
	
	void OnGUI()
	{
		EditorGUILayout.LabelField("===============================",EditorStyles.boldLabel);
		EditorGUILayout.LabelField("Player 2 Chosen Characters Field",EditorStyles.boldLabel);
		EditorGUILayout.LabelField("===============================",EditorStyles.boldLabel);
		ScrollPositionEditor2=EditorGUILayout.BeginScrollView(ScrollPositionEditor2);
		if(parentEditor.TinkerPlayer_2.Count>0)
		{
			for(int a=0;a<parentEditor.TinkerPlayer_2.Count;a++)
			{
				EditorGUILayout.LabelField("===============================",EditorStyles.boldLabel);
				EditorGUILayout.LabelField(a+1+". Chosen Character Parameter Field",EditorStyles.boldLabel);
				EditorGUILayout.LabelField("===============================",EditorStyles.boldLabel);
				EditorGUILayout.LabelField("ID :",parentEditor.TinkerPlayer_2[a].ID.ToString());
				EditorGUILayout.LabelField("NAMA :",parentEditor.TinkerPlayer_2[a].NAMA);
				EditorGUILayout.LabelField("Tipe_Hero :",parentEditor.TinkerPlayer_2[a].HeroTipe);
				EditorGUILayout.LabelField("HP :",parentEditor.TinkerPlayer_2[a].Health.ToString());
				EditorGUILayout.LabelField("MP :",parentEditor.TinkerPlayer_2[a].Mana.ToString());
				EditorGUILayout.LabelField("ATK :",parentEditor.TinkerPlayer_2[a].Attack.ToString());
				EditorGUILayout.LabelField("DEF :",parentEditor.TinkerPlayer_2[a].Defense.ToString());
				EditorGUILayout.LabelField("REC :",parentEditor.TinkerPlayer_2[a].Recovery.ToString());
				EditorGUILayout.LabelField("SPD :",parentEditor.TinkerPlayer_2[a].Speed.ToString());
				
				if(GUILayout.Button("UNCHOSEN HERO"))
				{
					parentEditor.TinkerPlayer_2.RemoveAt(a);
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