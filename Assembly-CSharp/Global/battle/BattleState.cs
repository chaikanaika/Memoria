﻿using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BattleState : MonoBehaviour
{
	private void Awake()
	{
		this.Init();
		this.InitMapName();
		this.IsPlayFieldBGMInCurrentBattle = false;
	}

	private void InitMapName()
	{
		this.mapName = new Dictionary<Int32, String>();
		TextAsset textAsset = AssetManager.Load<TextAsset>("EmbeddedAsset/Manifest/BattleMap/BattleMapList.txt", false);
		StringReader stringReader = new StringReader(textAsset.text);
		String text;
		while ((text = stringReader.ReadLine()) != null)
		{
			String[] array = text.Split(new Char[]
			{
				','
			});
			Int32 num = -1;
			String empty = String.Empty;
			Int32.TryParse(array[0], out num);
			if (num != -1)
			{
				this.mapName.Add(num, array[1]);
			}
		}
	}

	public void Init()
	{
		this.battleMapIndex = 0;
		this.isFrontRow = false;
		this.debugStartType = 2;
		this.isLevitate = false;
		this.isTrance = new Boolean[4];
		for (Int32 i = 0; i < (Int32)this.isTrance.Length; i++)
		{
			this.isTrance[i] = false;
		}
		this.isFade = false;
		this.selectCharPosID = 0;
		this.selectPlayerCount = 4;
		this.isDebug = false;
		this.isRandomEncounter = false;
		this.isTutorial = false;
		battle.isAlreadyShowTutorial = false;
		this.FF9Battle = new FF9StateBattleSystem();
		this.FF9Battle.status_data = FF9BattleDB.status_data;
		this.FF9Battle.aa_data = FF9BattleDB.CharacterActions;
		this.FF9Battle.add_status = FF9BattleDB.StatusSets;
		this.fadeShader = Shader.Find("PSX/BattleMap_Abr_1");
		this.battleShader = Shader.Find("PSX/BattleMap_StatusEffect");
		this.shadowShader = Shader.Find("PSX/BattleMap_Abr_2");
		this.detailTexture = Resources.Load<Texture2D>("EmbeddedAsset/BattleMap/detailTexture");
	}

	public Boolean isNoBoosterMap()
	{
		return this.battleMapIndex == 302 || this.battleMapIndex == 155 || this.battleMapIndex == 160 || this.battleMapIndex == 163 || this.battleMapIndex == 4 || this.battleMapIndex == 73 || this.battleMapIndex == 299;
	}

	public Dictionary<Int32, String> mapName;

	public Int32 battleMapIndex;

	public Byte patternIndex;

	public Boolean isFrontRow;

	public Byte debugStartType;

	public Boolean isLevitate;

	public Boolean[] isTrance;

	public Boolean isFade;

	public Boolean isDebug;

	public Boolean mappingBattleIDWithMapList;

	public Boolean isTutorial;

	public Boolean isRandomEncounter;

	public Int32 selectCharPosID;

	public Int32 selectPlayerCount;

	public Texture2D detailTexture;

	public Shader fadeShader;

	public Shader battleShader;

	public Shader shadowShader;

	public FF9StateBattleSystem FF9Battle;

	public Boolean IsPlayFieldBGMInCurrentBattle;
}
