using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using AngryAnimals.Classes;
using Newtonsoft.Json;

public partial class ScoreManager : Node
{
	public static ScoreManager Instance { get; private set; }
	private const string ScoreFile = "user://animals.save";
	
	private const int DefaultScore = 1000;
	private int _levelSelected;
	
	List<LevelScore> _levelScores = new List<LevelScore>();
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;
		LoadScores();
	}

	public override void _ExitTree()
	{
		SaveScores();
	}

	public static LevelScore GetLevelScore(int level)
	{
		return Instance._levelScores.FirstOrDefault(ls => ls.LevelNumber == level);
	}
	
	public static int GetLevelBestScore(int level)
	{
		LevelScore levelScore = GetLevelScore(level);
		if (levelScore != null)
		{
			return levelScore.BestScore;
		}
		return DefaultScore;
	}

	public static int GetLevelSelected()
	{
		return Instance._levelSelected;
	}

	public static int SetLevelSelected(int level)
	{
		return Instance._levelSelected = level;
	}

	public static void SetScoreForLevel(int level, int score)
	{
		LevelScore levelScore = GetLevelScore(level);
		if (levelScore != null)
		{
			if (score < levelScore.BestScore)
			{
				levelScore.BestScore = score;
				levelScore.DateSet = DateTime.Now;
			}
		}
		else
		{
			Instance._levelScores.Add(new LevelScore(level, score));
		}
	}

	private void SaveScores()
	{
		using var file = FileAccess.Open(ScoreFile, FileAccess.ModeFlags.Write);
		if (file != null)
		{
			string jsonStr = JsonConvert.SerializeObject(_levelScores);
			file.StoreString(jsonStr);
		}
	}
	
	private void LoadScores()
	{
		using var file = FileAccess.Open(ScoreFile, FileAccess.ModeFlags.Read);
		if (file != null)
		{
			string jsonString = file.GetAsText();
			if (!string.IsNullOrEmpty(jsonString))
			{
				_levelScores = JsonConvert.DeserializeObject<List<LevelScore>>(jsonString);
			}
		}
	}
}
