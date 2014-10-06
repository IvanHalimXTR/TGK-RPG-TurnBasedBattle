using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using Newtonsoft.Json;
using JsonDotNet;
using System.Linq;
/// <summary>
/// RPG_EDITOR License and Agreement From Creators
/// <description>
/// RPG TURN BASED EDITOR RPG by Ivan Halim
/// Feature:
/// 1.Implement Simulation Battle Turn Based
/// 2.Auto Generated GameObject + Scripts to Scene
/// 3.Save and Load Database JSON
/// 4.Customization Player Attribute
/// </description>
/// </summary>
/// 
public class RPG_EDITOR : EditorWindow {
	HeroType TinkerType;
	public Texture2D sprite;
	public object[] KoleksiObject=new object[]{};
	public List<GameObject> MyRPGObject=new List<GameObject>();
	public List<MonoScript> MyRPGScript=new List<MonoScript>();
	public List<Texture2D> KoleksiTexture=new List<Texture2D>();
	public List<Hero> TinkerPlayer_1=new List<Hero>();
	public List<Hero> TinkerPlayer_2=new List<Hero>();
	public List<Hero> TinkerHeroList=new List<Hero>();
	Vector2 ScrollPositionEditor,ScrollPositionEditor2,ScrollPositionEditor3,ScrollPositionEditorMenu;
	string NamaHero="";
	// Add menu named "RPG_EDITOR" to the TINKER_GAME_RPG menu
	[MenuItem ("TINKER_GAME_RPG/RPG_EDITOR")]
	static void Init () {
		// Get existing open window or if none, make a new one:
		RPG_EDITOR window = (RPG_EDITOR)EditorWindow.GetWindow (typeof (RPG_EDITOR));
		window.name="RPG_EDITOR";
	}

	public List<Hero> OrderTurnListbySpeed(List<Hero> HeroToOrder)
	{
		return HeroToOrder.OrderByDescending(o=>o.Speed).ToList(); 
	}

	public void ClearAllData()
	{
		TinkerHeroList.Clear();
		TinkerPlayer_1.Clear();
		TinkerPlayer_2.Clear();
	}

	public void SaveData()
	{
		var TINKER_HERO_LIST=JsonConvert.SerializeObject(TinkerHeroList,new Newtonsoft.Json.Converters.StringEnumConverter());
		var TINKER_PLAYER_1=JsonConvert.SerializeObject(TinkerPlayer_1,new Newtonsoft.Json.Converters.StringEnumConverter());
		var TINKER_PLAYER_2=JsonConvert.SerializeObject(TinkerPlayer_2,new Newtonsoft.Json.Converters.StringEnumConverter());

		SaveResources("TINKER_HERO_LIST",TINKER_HERO_LIST);
		SaveResources("TINKER_PLAYER_1",TINKER_PLAYER_1);
		SaveResources("TINKER_PLAYER_2",TINKER_PLAYER_2);
	}

	public void LoadData()
	{
		TinkerHeroList=JsonConvert.DeserializeObject<List<Hero>>(LoadResources("TINKER_HERO_LIST"));
		TinkerPlayer_1=JsonConvert.DeserializeObject<List<Hero>>(LoadResources("TINKER_PLAYER_1"));
		TinkerPlayer_2=JsonConvert.DeserializeObject<List<Hero>>(LoadResources("TINKER_PLAYER_2"));
	}

	void SaveResources(string FileNameNoExt,string savecontent)
	{
		string path=Application.dataPath+"/Resources/"+FileNameNoExt+".txt";
		System.IO.File.WriteAllText(path,"");
		System.IO.File.AppendAllText(path,savecontent);
	}

	string LoadResources(string FileNameNoExt)
	{
		return System.IO.File.ReadAllText(Application.dataPath+"/Resources/"+FileNameNoExt+".txt");
	}

	public void CreateHero(HeroType TinkerType,string NamaHero)
	{
		TinkerHeroList.Add(new Hero(TinkerType));
		TinkerHeroList[TinkerHeroList.Count-1].ID=TinkerHeroList.Count-1;
		TinkerHeroList[TinkerHeroList.Count-1].NAMA=NamaHero;
	}

	public void P1_ChooseCharacter(int a)
	{
		if(!TinkerPlayer_1.Contains(TinkerHeroList[a]))
		{
			TinkerPlayer_1.Add(TinkerHeroList[a]);
		}
	}

	public void P2_ChooseCharacter(int a)
	{
		if(!TinkerPlayer_2.Contains(TinkerHeroList[a]))
		{
			TinkerPlayer_2.Add(TinkerHeroList[a]);
		}
	}

	public void OpenBattleEditor()
	{
		RPG_BATTLE_EDITOR window=EditorWindow.GetWindow<RPG_BATTLE_EDITOR> ("RPG_BATTLE_EDITOR");
		window.childEditor(this);
	}

	public void OpenHeroProperties()
	{
		RPG_HERO_PROPERTIES window=EditorWindow.GetWindow<RPG_HERO_PROPERTIES>("HERO_LIST",true);
		window.childEditor(this);
	}

	public void OpenPlayer_1()
	{
		RPG_HERO_P1 window = EditorWindow.GetWindow<RPG_HERO_P1>("PLAYER 1 - CHOSEN CHARS",true);
		window.title="PLAYER 1 - CHOSEN CHARS";
		window.childEditor(this);
	}

	public void OpenPlayer_2()
	{
		RPG_HERO_P2 window = EditorWindow.GetWindow<RPG_HERO_P2>("PLAYER 2 - CHOSEN CHARS",true);
		window.title="PLAYER 2 - CHOSEN CHARS";
		window.childEditor(this);
	}

	void OnInspectorUpdate()
	{
		Repaint();
	}

	public void AddingScriptComponentToGameObject(GameObject GamePrefab,MonoScript ScriptToAdd)
	{
		GamePrefab.AddComponent(ScriptToAdd.name);
	}

	public void InsertDataToGameObject(GameObject GamePrefab,Hero TinkerP,MonoScript ScriptToAdd)
	{
		GamePrefab.GetComponent(ScriptToAdd.name).SendMessage("InsertHeroProperties",TinkerP,SendMessageOptions.DontRequireReceiver);
	}

	public void RemovingScriptComponentFromGameObject(GameObject GamePrefab,MonoScript ScriptToAdd)
	{
		if(GamePrefab.GetComponent(ScriptToAdd.name)!=null)
		{
			DestroyImmediate(GamePrefab.GetComponent(ScriptToAdd.name),true);
		}
	}
	
	void OnGUI () {
		if(GUILayout.Button("RESET",GUILayout.Width(50f)))
		{
			ClearAllData();
		}

		EditorGUILayout.LabelField("Character Creator Field",EditorStyles.boldLabel);

		TinkerType=(HeroType)EditorGUILayout.EnumPopup("TIPE HERO : ",TinkerType);
		NamaHero=EditorGUILayout.TextField("Hero Name :",NamaHero);

		EditorGUILayout.BeginHorizontal();

		if(GUILayout.Button("SAVE DATA"))
		{
			SaveData();
		}
		if(GUILayout.Button("LOAD DATA"))
		{
			LoadData();
		}

		if(GUILayout.Button("CREATE HERO"))
		{
			CreateHero(TinkerType,NamaHero);
		}

		EditorGUILayout.EndHorizontal();
		if(GUILayout.Button("OPEN BATTLE EDITOR"))
		{
			OpenBattleEditor();
		}

		if(GUILayout.Button("OPEN HERO LIST"))
		{
			OpenHeroProperties();
		}

		if(TinkerPlayer_1.Count>0)
		{
			if(GUILayout.Button("SHOW PLAYER 1 CHOSEN CHARACTERS"))
			{
				OpenPlayer_1();
			}
		}

		if(TinkerPlayer_2.Count>0)
		{
			if(GUILayout.Button("SHOW PLAYER 2 CHOSEN CHARACTERS"))
			{
				OpenPlayer_2();
			}
		}
	}	
}
