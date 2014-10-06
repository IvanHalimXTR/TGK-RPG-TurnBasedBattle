using UnityEngine;
using System.Collections;
using UnityEditor;

public class RPG_LOG : RPG_BATTLE_EDITOR {
	RPG_BATTLE_EDITOR parentEditor; 

	public void childEditor(RPG_BATTLE_EDITOR parent)
	{
		parentEditor=parent;
	}

	void OnGUI()
	{
		for(int a=0;a<parentEditor.ChangeLog.Count;a++)
		{
			EditorGUILayout.LabelField(parentEditor.ChangeLog[a],EditorStyles.toolbarButton);
		}
	}
}
