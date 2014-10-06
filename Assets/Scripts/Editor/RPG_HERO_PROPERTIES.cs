using UnityEngine;
using System.Collections;
using UnityEditor;

public class RPG_HERO_PROPERTIES : RPG_EDITOR {
	RPG_EDITOR parentEditor;
	Vector2 ScrollPositionEditor;

	public void childEditor(RPG_EDITOR parent)
	{
		parentEditor=parent;
	}

	void OnGUI(){
		ScrollPositionEditor=EditorGUILayout.BeginScrollView(ScrollPositionEditor);
		if(parentEditor.TinkerHeroList.Count>0)
		{
			for(int b=0;b<parentEditor.TinkerHeroList.Count;b++)
			{
				parentEditor.MyRPGObject.Add((GameObject)Resources.Load("Prefab/ExamplePrefab"));
				parentEditor.MyRPGScript.Add((MonoScript)Resources.Load("Scripts/PrefabPropertiesExample"));
			}

			for(int a=0;a<parentEditor.TinkerHeroList.Count;a++)
			{
				if(parentEditor.MyRPGObject[a]!=null&&parentEditor.MyRPGScript[a]!=null)
				{
				EditorGUILayout.LabelField("===============================",EditorStyles.boldLabel);
				EditorGUILayout.LabelField("Character "+a+" Parameter Field",EditorStyles.boldLabel);
				EditorGUILayout.LabelField("===============================",EditorStyles.boldLabel);
				parentEditor.MyRPGObject[a]=(GameObject)EditorGUILayout.ObjectField("MY PREFAB:",(GameObject)parentEditor.MyRPGObject[a],typeof(GameObject),true);
				parentEditor.MyRPGScript[a]=(MonoScript)EditorGUILayout.ObjectField("MY SCRIPT:",(MonoScript)parentEditor.MyRPGScript[a],typeof(MonoScript),true);

				if(GUILayout.Button("Add General Script to Prefab"))
				{
					parentEditor.AddingScriptComponentToGameObject(parentEditor.MyRPGObject[a],parentEditor.MyRPGScript[a]);
				}
				
				if(GUILayout.Button("Insert Parameter to Prefab"))
				{
					if(parentEditor.TinkerHeroList.Count>0)
					{
						parentEditor.InsertDataToGameObject(parentEditor.MyRPGObject[a],parentEditor.TinkerHeroList[a],parentEditor.MyRPGScript[a]);
					}
				}

				if(GUILayout.Button("Create Hero to Scene"))
				{
						GameObject NewHero=(GameObject)Instantiate(parentEditor.MyRPGObject[a],new Vector3(0,0,0),Quaternion.identity);
						NewHero.name=parentEditor.TinkerHeroList[a].NAMA;
						NewHero.tag="HERO";
						NewHero.layer=8;
				}

				EditorGUILayout.LabelField("ID :",parentEditor.TinkerHeroList[a].ID.ToString());
				EditorGUILayout.LabelField("NAMA :",parentEditor.TinkerHeroList[a].NAMA);
				EditorGUILayout.LabelField("Tipe_Hero :",parentEditor.TinkerHeroList[a].HeroTipe);
				EditorGUILayout.LabelField("HP :",parentEditor.TinkerHeroList[a].Health.ToString());
				EditorGUILayout.LabelField("MP :",parentEditor.TinkerHeroList[a].Mana.ToString());
				EditorGUILayout.LabelField("ATK :",parentEditor.TinkerHeroList[a].Attack.ToString());
				EditorGUILayout.LabelField("DEF :",parentEditor.TinkerHeroList[a].Defense.ToString());
				EditorGUILayout.LabelField("REC :",parentEditor.TinkerHeroList[a].Recovery.ToString());
				EditorGUILayout.LabelField("SPD :",parentEditor.TinkerHeroList[a].Speed.ToString());
				if(GUILayout.Button("CHOOSE HERO FOR PLAYER 1"))
				{
					parentEditor.P1_ChooseCharacter(a);
				}
				if(GUILayout.Button("CHOOSE HERO FOR PLAYER 2"))
				{
					parentEditor.P2_ChooseCharacter(a);
				}
				if(GUILayout.Button("DELETE HERO"))
				{
					parentEditor.TinkerHeroList.RemoveAt(a);
				}
				EditorGUILayout.LabelField("-------------------------------",EditorStyles.boldLabel);
				}
			}
		}
		else
		{
			Close();
		}
		EditorGUILayout.EndScrollView();
	}
}
