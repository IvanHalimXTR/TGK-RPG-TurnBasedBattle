using UnityEngine;
using System.Collections;

public enum HeroType{
	DURABLE = 0,
	ATTACK = 1,
	MAGIC = 2,
	HEALER = 3,
	SPLASH = 4
}

[System.Serializable]
public class Hero{
	[SerializeField]
	public int ID;
	[SerializeField]
	public int HP,MP,ATK,DEF,REC,SPD;
	[SerializeField]
	public string NAMA;
	[SerializeField]
	public string TipeHero;
	
	public Hero(HeroType tipehero)
	{
		switch(tipehero)
		{
			case HeroType.DURABLE:{
				HP=1500;
				MP=200;
				ATK=100;
				DEF=50;
				REC=15;
				SPD=10;
				TipeHero=tipehero.ToString();
				break;
			}
			case HeroType.ATTACK:{
				HP=800;
				MP=300;
				ATK=250;
				DEF=40;
				REC=5;
				SPD=40;
				TipeHero=tipehero.ToString();
				break;
			}
			case HeroType.MAGIC:{
				HP=750;
				MP=600;
				ATK=75;
				DEF=35;
				REC=10;
				SPD=30;
				TipeHero=tipehero.ToString();
				break;
			}
			case HeroType.HEALER:{
				HP=1000;
				MP=500;
				ATK=80;
				DEF=25;
				REC=50;
				SPD=35;
				TipeHero=tipehero.ToString();
				break;
			}
			case HeroType.SPLASH:{
				HP=950;
				MP=200;
				ATK=65;
				DEF=20;
				REC=15;
				SPD=20;
				TipeHero=tipehero.ToString();
				break;
			}
		}
	}
	public int Health{
		get{return HP;}
	}
	public int Mana{
		get{return MP;}
	}
	public int Attack{
		get{return ATK;}
	}
	public int Defense{
		get{return DEF;}
	}
	public int Recovery{
		get{return REC;}
	}
	public string HeroTipe{
		get{return TipeHero;}
	}
	public int Speed{
		get{return SPD;}
	}
	
	
	//Overriding Skills
	public virtual void Skill1(Hero Caster,Hero Target,int DamageDealRandom)
	{
		switch(Caster.HeroTipe)
		{
		case "DURABLE":{
			///ATTACK FORMULA EXAMPLE: Target.Speed = 100 , Caster.Speed = 110 , MAX= (100/210) * 100 = 47.61 , MIN= 0 ,  
			if(Random.Range(0,((Target.Speed /(Caster.Speed+Target.Speed))*100))>((Target.Speed /(Caster.Speed+Target.Speed))*100)/4)
			{
				Target.HP=Target.Health-(Caster.ATK+DamageDealRandom-Target.DEF);
			}
			else
			{
				Target.HP=Target.Health-DamageDealRandom+Target.DEF;
			}
			break;
		}
		case "ATTACK":{
			if(Random.Range(0,((Target.Speed /(Caster.Speed+Target.Speed))*100))>((Target.Speed /(Caster.Speed+Target.Speed))*100)/4)
			{
				Target.HP=Target.Health-(Caster.ATK+DamageDealRandom-Target.DEF);
			}
			else
			{
				Target.HP=Target.Health-DamageDealRandom+Target.DEF;
			}
			break;
		}
		case "MAGIC":{
			if(Random.Range(0,((Target.Speed /(Caster.Speed+Target.Speed))*100))>((Target.Speed /(Caster.Speed+Target.Speed))*100)/4)
			{
				Target.HP=Target.Health-(Caster.ATK+DamageDealRandom-Target.DEF);
			}
			else
			{
				Target.HP=Target.Health-DamageDealRandom+Target.DEF;
			}
			break;
		}
		case "HEALER":{
			if(Random.Range(0,((Target.Speed /(Caster.Speed+Target.Speed))*100))>((Target.Speed /(Caster.Speed+Target.Speed))*100)/4)
			{
				Target.HP=Target.Health-(Caster.ATK+DamageDealRandom-Target.DEF);
			}
			else
			{
				Caster.HP += Caster.Recovery;
				Target.HP = Target.Health-DamageDealRandom+Target.DEF;
			}
			break;
		}
		case "SPLASH":{
			if(Random.Range(0,((Target.Speed /(Caster.Speed+Target.Speed))*100))>((Target.Speed /(Caster.Speed+Target.Speed))*100)/4)
			{
				Target.HP=Target.Health-(Caster.ATK+DamageDealRandom-Target.DEF);
			}
			else
			{
				Target.HP=Target.Health-DamageDealRandom+Target.DEF;
			}
			break;
		}
		}
	}
	
	public virtual void Skill2(Hero Caster,Hero Target,int DamageDealRandom)
	{
		switch(Caster.HeroTipe)
		{
		case "DURABLE":{
			if(Random.Range(0,((Target.Speed /(Caster.Speed+Target.Speed))*100))>((Target.Speed /(Caster.Speed+Target.Speed))*100)/4)
			{
				Target.HP=Target.Health-((Caster.ATK*Caster.SPD)+DamageDealRandom-Target.DEF);
			}
			else
			{
				Target.HP=Target.Health-DamageDealRandom-Target.DEF;
			}
			break;
		}
		case "ATTACK":{
			if(Random.Range(0,((Target.Speed /(Caster.Speed+Target.Speed))*100))>((Target.Speed /(Caster.Speed+Target.Speed))*100)/4)
			{
				Target.HP=Target.Health-((Caster.ATK*Caster.SPD)+DamageDealRandom-Target.DEF);
			}
			else
			{
				Target.HP=Target.Health-DamageDealRandom-Target.DEF;
			}
			break;
		}
		case "MAGIC":{
			if(Random.Range(0,((Target.Speed /(Caster.Speed+Target.Speed))*100))>((Target.Speed /(Caster.Speed+Target.Speed))*100)/4)
			{
				Target.HP=Target.Health-((Caster.ATK*Caster.SPD)+DamageDealRandom-Target.DEF);
			}
			else
			{
				Target.HP=Target.Health-DamageDealRandom-Target.DEF;
			}
			break;
		}
		case "HEALER":{
			if(Random.Range(0,((Target.Speed /(Caster.Speed+Target.Speed))*100))>((Target.Speed /(Caster.Speed+Target.Speed))*100)/4)
			{
				Target.HP=Target.Health-((Caster.ATK*Caster.SPD)+DamageDealRandom-Target.DEF);
			}
			else
			{
				Caster.HP += Caster.Recovery;
				Target.HP = Target.Health-DamageDealRandom-Target.DEF;
			}
			break;
		}
		case "SPLASH":{
			if(Random.Range(0,((Target.Speed /(Caster.Speed+Target.Speed))*100))>((Target.Speed /(Caster.Speed+Target.Speed))*100)/4)
			{
				Target.HP=Target.Health-((Caster.ATK*Caster.SPD)+DamageDealRandom-Target.DEF);
			}
			else
			{
				Target.HP=Target.Health-DamageDealRandom-Target.DEF;
			}
			break;
		}
		}
	}
	public virtual void Skill3(Hero Caster)
	{
		switch(Caster.HeroTipe)
		{
			case "DURABLE":{
				Caster.DEF=Caster.DEF+(Caster.DEF+Random.Range(0,Caster.DEF));
				break;
			}
			case "ATTACK":{
				Caster.DEF=Caster.DEF+(Caster.DEF+Random.Range(0,Caster.DEF));
				break;
			}
			case "MAGIC":{
				Caster.DEF=Caster.DEF+(Caster.DEF+Random.Range(0,Caster.DEF));
				break;
			}
			case "HEALER":{
				Caster.DEF=Caster.DEF+(Caster.DEF+Random.Range(0,Caster.DEF));
				break;
			}
			case "SPLASH":{
				Caster.DEF=Caster.DEF+(Caster.DEF+Random.Range(0,Caster.DEF));
				break;
			}
		}
	}
	
	public virtual void Skill4(Hero Caster,Hero Target,int DamageDealRandom)
	{
		switch(Caster.HeroTipe)
		{
		case "DURABLE":{
			if(Random.Range(0,((Target.Speed /(Caster.Speed+Target.Speed))*100))>((Target.Speed /(Caster.Speed+Target.Speed))*100)/4)
			{
				Target.HP=Target.Health-(Caster.ATK*DamageDealRandom-Target.DEF);
			}
			else
			{
				Target.HP=Target.Health-((Caster.ATK*2)+DamageDealRandom)+Target.DEF;
			}
			break;
		}
		case "ATTACK":{
			if(Random.Range(0,((Target.Speed /(Caster.Speed+Target.Speed))*100))>((Target.Speed /(Caster.Speed+Target.Speed))*100)/4)
			{
				Target.HP=Target.Health-(Caster.ATK*DamageDealRandom-Target.DEF);
			}
			else
			{
				Target.HP=Target.Health-((Caster.ATK*2)+DamageDealRandom)+Target.DEF;
			}
			break;
		}
		case "MAGIC":{
			if(Random.Range(0,((Target.Speed /(Caster.Speed+Target.Speed))*100))>((Target.Speed /(Caster.Speed+Target.Speed))*100)/4)
			{
				Target.HP=Target.Health-(Caster.ATK*DamageDealRandom-Target.DEF);
			}
			else
			{
				Target.HP=Target.Health-((Caster.ATK*2)+DamageDealRandom)+Target.DEF;
			}
			break;
		}
		case "HEALER":{
			if(Random.Range(0,((Target.Speed /(Caster.Speed+Target.Speed))*100))>((Target.Speed /(Caster.Speed+Target.Speed))*100)/4)
			{
				Target.HP=Target.Health-(Caster.ATK*DamageDealRandom-Target.DEF);
			}
			else
			{
				if(Caster.HP<new Hero(HeroType.HEALER).Health)
				{
					Caster.HP += Caster.Recovery;
				}
				Target.HP = Target.Health-((Caster.ATK*2)+DamageDealRandom)+Target.DEF;
			}
			break;
		}
		case "SPLASH":{
			if(Random.Range(0,((Target.Speed /(Caster.Speed+Target.Speed))*100))>((Target.Speed /(Caster.Speed+Target.Speed))*100)/4)
			{
				Target.HP=Target.Health-(Caster.ATK*DamageDealRandom-Target.DEF);
			}
			else
			{
				Target.HP=Target.Health-((Caster.ATK*2)+DamageDealRandom)+Target.DEF;
			}
			break;
		}
		}
	}
}
