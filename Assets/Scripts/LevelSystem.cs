using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{
    private int _xpNow;
    private int _level;
    private int _xpToNext;

    [SerializeField] private GameObject levelPanel;
    [SerializeField] private GameObject lvlWindowPrefab;

    private Slider _slider;
    private TextMeshProUGUI _xpText;
    private TextMeshProUGUI _lvlText;
    private Image _starImage;

    private static bool _initialized;
    private static Dictionary<int, int> _xpToNextLevel = new Dictionary<int, int>();
    private static Dictionary<int, int[]> _lvlReward = new Dictionary<int, int[]>();

    private void Awake()
    {
        _slider = levelPanel.GetComponent<Slider>();
        _xpText = levelPanel.transform.Find("XP text").GetComponent<TextMeshProUGUI>();
        _starImage = levelPanel.transform.Find("Star").GetComponent<Image>();
        _lvlText = _starImage.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        if (!_initialized)
        {
            Initialize();
        }

        _xpToNextLevel.TryGetValue(_level, out _xpToNext);
    }

    private static void Initialize()
    {
        try
        {
            string path = "levelsXP";

            TextAsset textAsset = Resources.Load<TextAsset>(path);
            string[] lines = textAsset.text.Split('\n');

            _xpToNextLevel = new Dictionary<int, int>(lines.Length - 1);

            for (int i = 1; i < lines[i].Length - 1; i++)
            {
                string[] colums = lines[i].Split(',');

                int lvl = -1;
                int xp = -1;
                int curr1 = -1;
                int curr2 = -1;

                int.TryParse(colums[0], out lvl);
                int.TryParse(colums[1], out xp);
                int.TryParse(colums[2], out curr1);
                int.TryParse(colums[3], out curr2);

                if (lvl >= 0 && xp > 0)
                {
                    if (!_xpToNextLevel.ContainsKey(lvl))
                    {
                        _xpToNextLevel.Add(lvl, xp);
                        _lvlReward.Add(lvl, new[] { curr1, curr2 });
                    }
                }
            }
        }

        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }

        _initialized = true;
    }

    private void Start()
    {
        EventManager.Instance.AddListener<GameEvent.XPAddedGameEvent>(OnXPAdded);
        EventManager.Instance.AddListener<GameEvent.LevelChangedGameEvent>(OnLevelChanged);
        
        UpdateUI();
    }

    private void UpdateUI()
    {
        float fill = (float)_xpNow / _xpToNext;
        _slider.value = fill;
        _xpText.text = _xpNow + "/" + _xpToNext;
    }

    private void OnXPAdded(GameEvent.XPAddedGameEvent info)
    {
        _xpNow += info.Amount;
        
        UpdateUI();

        if (_xpNow >= _xpToNext)
        {
            _level++;
            GameEvent.LevelChangedGameEvent levelChange = new GameEvent.LevelChangedGameEvent(_level);
            EventManager.Instance.QueueEvent(levelChange);
        }
    }

    private void OnLevelChanged(GameEvent.LevelChangedGameEvent info)
    {
        _xpNow -= _xpToNext;
        _xpToNext = _xpToNextLevel[info.NewLvl];
        _lvlText.text = (info.NewLvl + 1).ToString();
        UpdateUI();

        GameObject window = Instantiate(lvlWindowPrefab, GameManager.Current.canvas.transform);
        
        window.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { Destroy(window); });

        GameEvent.CurrencyChangeGameEvent currencyInfo =
            new GameEvent.CurrencyChangeGameEvent(_lvlReward[info.NewLvl[0], CurrencyType.Coins]);
        EventManager.Instance.QueueEvent(currencyInfo);
        
        currencyInfo =
            new GameEvent.CurrencyChangeGameEvent(_lvlReward[info.NewLvl[1], CurrencyType.Coins]);
        EventManager.Instance.QueueEvent(currencyInfo);
    }
}
