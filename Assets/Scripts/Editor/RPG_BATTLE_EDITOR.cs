using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class RPG_BATTLE_EDITOR : RPG_EDITOR{
	RPG_EDITOR parentEditor;
	public List<Hero> temp=new List<Hero>();
	public List<Hero> temp2=new List<Hero>();
	public List<Hero> HEROTURNORDER=new List<Hero>();
	public List<string> ChangeLog=new List<string>();
	public bool P1_Turn=false,P2_Turn=false,ENDTURN=false;
	public bool OpenWindow=false,OpenWindow2=false;
	bool Battle=false;
	bool TargetChosen=false;
	public Vector2 ScrollPositionBattleEditor;
	int P1_SPD,P2_SPD;
	public int target;
	public bool WIN=false;	

	static void Init () {
		// Get existing open window or if none, make a new one:
		RPG_BATTLE_EDITOR window = (RPG_BATTLE_EDITOR)EditorWindow.GetWindow (typeof (RPG_BATTLE_EDITOR));
		window.name="RPG_BATTLE_EDITOR";
	}

	public void USINGSKILL(Hero HEROOFTINKER,int ManaUsagePercentage)
	{
		HEROOFTINKER.MP-=(HEROOFTINKER.MP*ManaUsagePercentage/100);
	}

	public void Open_Battle_ChangeLog()
	{
		RPG_LOG window =(RPG_LOG)EditorWindow.GetWindow(typeof(RPG_LOG));
		window.childEditor(this);
		window.Repaint();
	}

	public void Open_Hero_Turns()
	{
		RPG_HERO_TURNS window = (RPG_HERO_TURNS)EditorWindow.GetWindow (typeof (RPG_HERO_TURNS));
		window.childEditor(this);
		window.Repaint();
	}

	public void childEditor(RPG_EDITOR parent)
	{
		parentEditor=parent;
	}
	
	public int GET_MOST_SPEED_HERO(List<Hero> TinkerPlayer)
	{
		int MAXtempSPD=0;
		for(int a=0;a<TinkerPlayer.Count;a++)
		{
			if(MAXtempSPD<=TinkerPlayer[a].Speed)
			{
				MAXtempSPD=TinkerPlayer[a].Speed;
				return a;
			}
		}
		return 0;
	}

	public bool CheckingTurn(List<Hero> KumpulanHero,Hero HeroCek)
	{
		for(int k=0;k<KumpulanHero.Count;k++)
		{
			if(KumpulanHero[k].NAMA.Equals(HeroCek.NAMA))
			{
				return true;
			}
		}
		return false;
	}

	void OnGUI () {
		if(parentEditor!=null)
		{
			ScrollPositionBattleEditor=EditorGUILayout.BeginScrollView(ScrollPositionBattleEditor);
			if(parentEditor.TinkerPlayer_1.Count>0&&parentEditor.TinkerPlayer_2.Count>0)
			{
				if(Battle)
				{
					if(GUILayout.Button("OPEN HERO TURN LIST"))
					{
						if(!OpenWindow)
						{
							Open_Hero_Turns();
							OpenWindow=!OpenWindow;
						}	
					}
					if(temp.Count==0 && temp2.Count==0 && HEROTURNORDER.Count==0)
					{
						temp=parentEditor.OrderTurnListbySpeed(parentEditor.TinkerPlayer_1);
						temp2=parentEditor.OrderTurnListbySpeed(parentEditor.TinkerPlayer_2);

						HEROTURNORDER.AddRange(temp);
						HEROTURNORDER.AddRange(temp2);
						HEROTURNORDER=parentEditor.OrderTurnListbySpeed(HEROTURNORDER);
					}

					if(HEROTURNORDER.Count>0)
					{
						P1_Turn=CheckingTurn(temp,HEROTURNORDER[0]);
						P2_Turn=CheckingTurn(temp2,HEROTURNORDER[0]);
					}
					else
					{
						HEROTURNORDER.AddRange(temp);
						HEROTURNORDER.AddRange(temp2);
						HEROTURNORDER=parentEditor.OrderTurnListbySpeed(HEROTURNORDER);
						for(int x=0;x<HEROTURNORDER.Count;x++)
						{
							if(HEROTURNORDER[x].Health<=0)
							{
								HEROTURNORDER.Remove(HEROTURNORDER[x]);
							}
						}
					}

					if(P1_Turn)
					{
						EditorGUILayout.LabelField(HEROTURNORDER[HEROTURNORDER.Count-HEROTURNORDER.Count].NAMA+"'s turn Player 1",EditorStyles.toolbarButton);
						EditorGUILayout.BeginToggleGroup("Available Skills",P1_Turn);
						EditorGUILayout.LabelField("TARGET");
						if(TargetChosen==false&&!ENDTURN)
						{
							for(int j=0;j<temp2.Count;j++)
							{
								if(temp2[j].Health>0)
								{
									if(GUILayout.Button(temp2[j].NAMA)&&TargetChosen==false)
									{
										target=j;
										TargetChosen=true;
									}
								}
							}
						}
						EditorGUILayout.BeginHorizontal();
						if(TargetChosen)
						{
							if(GUILayout.Button("SKILL 1 - ATTACK"))
							{
								int damagedeal=Random.Range(0,temp.Find(p=>p.Equals(HEROTURNORDER[0])).Attack);
								HEROTURNORDER[0].Skill1(temp.Find(p => p.Equals(HEROTURNORDER[0])),temp2[target],damagedeal);
								ChangeLog.Add(temp.Find(p => p.Equals(HEROTURNORDER[0])).NAMA+" Doing Attack to "+temp2[target].NAMA+" with Damage Power "+damagedeal);
								TargetChosen=false;
								ENDTURN=true;
							}
							EditorGUILayout.BeginVertical();
							if(EditorGUILayout.BeginToggleGroup("",temp.Find(p=>p.Equals(HEROTURNORDER[0])).Mana>(HEROTURNORDER[0].MP*20/100)))
							{
								if(GUILayout.Button("SKILL 2 - SPECIAL ATTACK (20% MP)" ))
								{
									int damagedeal=Random.Range(0,temp.Find(p=>p.Equals(HEROTURNORDER[0])).Attack);
									HEROTURNORDER[0].Skill2(temp.Find(p => p.Equals(HEROTURNORDER[0])),temp2[target],damagedeal);
									ChangeLog.Add(temp.Find(p => p.Equals(HEROTURNORDER[0])).NAMA+" Doing Special Attack to "+temp2[target].NAMA+" with Damage Power "+damagedeal);
									USINGSKILL(temp.Find(p=>p.Equals(HEROTURNORDER[0])),20);
									TargetChosen=false;
									ENDTURN=true;
								}
							}
							EditorGUILayout.EndToggleGroup();
							if(EditorGUILayout.BeginToggleGroup("",temp.Find(p=>p.Equals(HEROTURNORDER[0])).Mana>(HEROTURNORDER[0].MP*30/100)))
							{
								if(GUILayout.Button("SKILL ULTIMATE (30% MP)"))
								{
									int damagedeal=Random.Range(0,temp.Find(p=>p.Equals(HEROTURNORDER[0])).Attack);
									HEROTURNORDER[0].Skill4(temp.Find(p => p.Equals(HEROTURNORDER[0])),temp2[target],damagedeal);
									ChangeLog.Add(temp.Find(p => p.Equals(HEROTURNORDER[0])).NAMA+" Doing Ultimate Attack to "+temp2[target].NAMA+" with Damage Power "+damagedeal);
									USINGSKILL(temp.Find(p=>p.Equals(HEROTURNORDER[0])),30);
									TargetChosen=false;
									ENDTURN=true;
								}
							}
							EditorGUILayout.EndToggleGroup();
							EditorGUILayout.EndVertical();
						}
						else if(!ENDTURN)
						{
							if(GUILayout.Button("SKILL 3 - DEFEND"))
							{
								HEROTURNORDER[0].Skill3(temp.Find(p => p.Equals(HEROTURNORDER[0])));
								ChangeLog.Add(temp.Find(p=>p.Equals(HEROTURNORDER[0])).NAMA+" Doing Defend (DEF INCREASED TO)" +temp.Find(p=>p.Equals(HEROTURNORDER[0])).DEF);
								TargetChosen=false;
								ENDTURN=true;
							}
						}
						if(GUILayout.Button("END TURN"))
						{
							if(HEROTURNORDER.Count>0)
							{
								HEROTURNORDER.RemoveAt(0);
								ENDTURN=false;
							}
						}
						EditorGUILayout.EndHorizontal();
						EditorGUILayout.EndToggleGroup();
					}
					
					if(P2_Turn)
					{
						EditorGUILayout.LabelField(HEROTURNORDER[HEROTURNORDER.Count-HEROTURNORDER.Count].NAMA+"'s turn Player 2",EditorStyles.toolbarButton);
						EditorGUILayout.BeginToggleGroup("Available Skills",P2_Turn);
						EditorGUILayout.LabelField("TARGET");
						if(TargetChosen==false&&!ENDTURN)
						{
						for(int j=0;j<temp.Count;j++)
						{
							if(temp[j].Health>0)
							{
								if(GUILayout.Button(temp[j].NAMA)&&TargetChosen==false)
								{
									target=j;
									TargetChosen=true;
								}
							}
						}
						}
						EditorGUILayout.BeginHorizontal();
						if(TargetChosen)
						{
							if(GUILayout.Button("SKILL 1 - ATTACK"))
							{
								int damagedeal=Random.Range(0,temp2.Find(p=>p.Equals(HEROTURNORDER[0])).Attack);
								HEROTURNORDER[0].Skill1(temp2.Find(p => p.Equals(HEROTURNORDER[0])),temp[target],damagedeal);
								ChangeLog.Add(temp2.Find(p => p.Equals(HEROTURNORDER[0])).NAMA+" Doing Attack to "+temp[target].NAMA+" with Damage Power "+damagedeal);
								TargetChosen=false;
								ENDTURN=true;
							}
							EditorGUILayout.BeginVertical();
							if(EditorGUILayout.BeginToggleGroup("",temp2.Find(p=>p.Equals(HEROTURNORDER[0])).Mana>(HEROTURNORDER[0].MP*20/100)))
							{
								if(GUILayout.Button("SKILL 2 - SPECIAL ATTACK (20% MP)" ))
								{
									int damagedeal=Random.Range(0,temp2.Find(p=>p.Equals(HEROTURNORDER[0])).Attack);
									HEROTURNORDER[0].Skill2(temp2.Find(p => p.Equals(HEROTURNORDER[0])),temp[target],damagedeal);
									ChangeLog.Add(temp2.Find(p => p.Equals(HEROTURNORDER[0])).NAMA+" Doing Special Attack to "+temp[target].NAMA+" with Damage Power "+damagedeal);
									USINGSKILL(temp2.Find(p=>p.Equals(HEROTURNORDER[0])),20);
									TargetChosen=false;
									ENDTURN=true;
								}
							}
							EditorGUILayout.EndToggleGroup();
							if(EditorGUILayout.BeginToggleGroup("",temp2.Find(p=>p.Equals(HEROTURNORDER[0])).Mana>(HEROTURNORDER[0].MP*30/100)))
							{
								if(GUILayout.Button("SKILL ULTIMATE (30% MP)"))
								{
									int damagedeal=Random.Range(0,temp2.Find(p=>p.Equals(HEROTURNORDER[0])).Attack);
									HEROTURNORDER[0].Skill4(temp2.Find(p => p.Equals(HEROTURNORDER[0])),temp[target],damagedeal);
									ChangeLog.Add(temp2.Find(p => p.Equals(HEROTURNORDER[0])).NAMA+" Doing Ultimate Attack to "+temp[target].NAMA+" with Damage Power "+damagedeal);
									USINGSKILL(temp2.Find(p=>p.Equals(HEROTURNORDER[0])),30);
									TargetChosen=false;
									ENDTURN=true;
								}
							}
							EditorGUILayout.EndToggleGroup();
							EditorGUILayout.EndVertical();

						}
						else if(!ENDTURN)
						{
							if(GUILayout.Button("SKILL 3 - DEFEND"))
							{
								HEROTURNORDER[0].Skill3(temp2.Find(p => p.Equals(HEROTURNORDER[0])));;
								ChangeLog.Add(temp2.Find(p=>p.Equals(HEROTURNORDER[0])).NAMA+" Doing Defend (DEF INCREASED TO)" +temp2.Find(p=>p.Equals(HEROTURNORDER[0])).DEF);
								TargetChosen=false;
								ENDTURN=true;
							}
						}
						if(GUILayout.Button("END TURN"))
						{
							if(HEROTURNORDER.Count>0)
							{
								HEROTURNORDER.RemoveAt(0);
								ENDTURN=false;
							}
						}
						EditorGUILayout.EndHorizontal();
						EditorGUILayout.EndToggleGroup();
					}

					if(P1_Turn||P2_Turn)
					{
						if(GUILayout.Button("OPEN COMBAT LOG"))
						{
							Open_Battle_ChangeLog();
						}
						EditorGUILayout.LabelField(" ",EditorStyles.boldLabel);
						EditorGUILayout.LabelField("--=============================================================--",EditorStyles.boldLabel);
						EditorGUILayout.LabelField(" ",EditorStyles.boldLabel);
						for(int f=0;f<temp.Count;f++)
						{
						EditorGUILayout.BeginHorizontal();
						EditorGUILayout.LabelField("----------PLAYER 1----------",EditorStyles.boldLabel);
						EditorGUILayout.LabelField("||----------PLAYER 2----------",EditorStyles.boldLabel);
						EditorGUILayout.EndHorizontal();
						
						EditorGUILayout.BeginHorizontal();
						EditorGUILayout.LabelField("ID :"+temp[f].ID,EditorStyles.boldLabel);
						EditorGUILayout.LabelField("||ID :"+temp2[f].ID,EditorStyles.boldLabel);
						EditorGUILayout.EndHorizontal();
						
						EditorGUILayout.BeginHorizontal();
						EditorGUILayout.LabelField("NAMA :"+temp[f].NAMA,EditorStyles.boldLabel);
						EditorGUILayout.LabelField("||NAMA :"+temp2[f].NAMA,EditorStyles.boldLabel);
						EditorGUILayout.EndHorizontal();
						
						EditorGUILayout.BeginHorizontal();
						EditorGUILayout.LabelField("Tipe :"+temp[f].HeroTipe,EditorStyles.boldLabel);
						EditorGUILayout.LabelField("||Tipe :"+temp2[f].HeroTipe,EditorStyles.boldLabel);
						EditorGUILayout.EndHorizontal();
						
						EditorGUILayout.BeginHorizontal();
						EditorGUILayout.LabelField("HP :",temp[f].Health.ToString());
						EditorGUILayout.LabelField("||HP :",temp2[f].Health.ToString());
						EditorGUILayout.EndHorizontal();
						
						EditorGUILayout.BeginHorizontal();
						EditorGUILayout.LabelField("MP :",temp[f].Mana.ToString());
						EditorGUILayout.LabelField("||MP :",temp2[f].Mana.ToString());
						EditorGUILayout.EndHorizontal();
						
						EditorGUILayout.BeginHorizontal();
						EditorGUILayout.LabelField("ATK :",temp[f].Attack.ToString());
						EditorGUILayout.LabelField("||ATK :",temp2[f].Attack.ToString());
						EditorGUILayout.EndHorizontal();
						
						EditorGUILayout.BeginHorizontal();
						EditorGUILayout.LabelField("DEF :",temp[f].Defense.ToString());
						EditorGUILayout.LabelField("||DEF :",temp2[f].Defense.ToString());
						EditorGUILayout.EndHorizontal();
						
						EditorGUILayout.BeginHorizontal();
						EditorGUILayout.LabelField("REC :",temp[f].Recovery.ToString());
						EditorGUILayout.LabelField("||REC :",temp2[f].Recovery.ToString());
						EditorGUILayout.EndHorizontal();
						
						EditorGUILayout.BeginHorizontal();
						EditorGUILayout.LabelField("SPD :",temp[f].Speed.ToString());
						EditorGUILayout.LabelField("||SPD :",temp2[f].Speed.ToString());
						EditorGUILayout.EndHorizontal();
						}
					}



				}

				if(!Battle)
				{
					if(GUILayout.Button("BATTLE START"))
					{
						Battle=true;
					}

					/// show player status and parameters
					for(int f=0;f<parentEditor.TinkerPlayer_1.Count;f++)
					{
						EditorGUILayout.BeginHorizontal();
						EditorGUILayout.LabelField("----------PLAYER 1----------",EditorStyles.boldLabel);
						EditorGUILayout.LabelField("||----------PLAYER 2----------",EditorStyles.boldLabel);
						EditorGUILayout.EndHorizontal();

						EditorGUILayout.BeginHorizontal();
						EditorGUILayout.LabelField("ID :"+parentEditor.TinkerPlayer_1[f].ID,EditorStyles.boldLabel);
						EditorGUILayout.LabelField("||ID :"+parentEditor.TinkerPlayer_2[f].ID,EditorStyles.boldLabel);
						EditorGUILayout.EndHorizontal();

						EditorGUILayout.BeginHorizontal();
						EditorGUILayout.LabelField("NAMA :"+parentEditor.TinkerPlayer_1[f].NAMA,EditorStyles.boldLabel);
						EditorGUILayout.LabelField("||NAMA :"+parentEditor.TinkerPlayer_2[f].NAMA,EditorStyles.boldLabel);
						EditorGUILayout.EndHorizontal();

						EditorGUILayout.BeginHorizontal();
						EditorGUILayout.LabelField("Tipe :"+parentEditor.TinkerPlayer_1[f].HeroTipe,EditorStyles.boldLabel);
						EditorGUILayout.LabelField("||Tipe :"+parentEditor.TinkerPlayer_2[f].HeroTipe,EditorStyles.boldLabel);
						EditorGUILayout.EndHorizontal();

						EditorGUILayout.BeginHorizontal();
						EditorGUILayout.LabelField("HP :",parentEditor.TinkerPlayer_1[f].Health.ToString());
						EditorGUILayout.LabelField("||HP :",parentEditor.TinkerPlayer_2[f].Health.ToString());
						EditorGUILayout.EndHorizontal();

						EditorGUILayout.BeginHorizontal();
						EditorGUILayout.LabelField("MP :",parentEditor.TinkerPlayer_1[f].Mana.ToString());
						EditorGUILayout.LabelField("||MP :",parentEditor.TinkerPlayer_2[f].Mana.ToString());
						EditorGUILayout.EndHorizontal();

						EditorGUILayout.BeginHorizontal();
						EditorGUILayout.LabelField("ATK :",parentEditor.TinkerPlayer_1[f].Attack.ToString());
						EditorGUILayout.LabelField("||ATK :",parentEditor.TinkerPlayer_2[f].Attack.ToString());
						EditorGUILayout.EndHorizontal();

						EditorGUILayout.BeginHorizontal();
						EditorGUILayout.LabelField("DEF :",parentEditor.TinkerPlayer_1[f].Defense.ToString());
						EditorGUILayout.LabelField("||DEF :",parentEditor.TinkerPlayer_2[f].Defense.ToString());
						EditorGUILayout.EndHorizontal();

						EditorGUILayout.BeginHorizontal();
						EditorGUILayout.LabelField("REC :",parentEditor.TinkerPlayer_1[f].Recovery.ToString());
						EditorGUILayout.LabelField("||REC :",parentEditor.TinkerPlayer_2[f].Recovery.ToString());
						EditorGUILayout.EndHorizontal();

						EditorGUILayout.BeginHorizontal();
						EditorGUILayout.LabelField("SPD :",parentEditor.TinkerPlayer_1[f].Speed.ToString());
						EditorGUILayout.LabelField("||SPD :",parentEditor.TinkerPlayer_2[f].Speed.ToString());
						EditorGUILayout.EndHorizontal();
					}
				}
			}
			EditorGUILayout.EndScrollView();
		}
		else
		{
			Close();
		}
	}
}
