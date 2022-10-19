using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerTooltip : MonoBehaviour
{
    private static TimerTooltip _instance;

    private Timer _timer;

    [SerializeField] private UnityEngine.Camera uiCamera;
    [SerializeField] private TextMeshProUGUI callerNameText;
    [SerializeField] private TextMeshProUGUI timeLeftText;
    [SerializeField] private Button skipButton;
    [SerializeField] private TextMeshProUGUI skipAmountText;
    [SerializeField] private Slider progressSlider;

    private bool _countdown;

    private void Awake()
    {
        _instance = this;
        transform.parent.gameObject.SetActive(false);
    }

    private void ShowTimer(GameObject caller)
    {
        _timer = caller.GetComponent<Timer>();

        if (_timer == null)
        {
            return;
        }

        callerNameText.text = _timer.Name;
        skipAmountText.text = _timer.SkipAmount.ToString();
        skipButton.gameObject.SetActive(true);

        Vector3 position = caller.transform.position - uiCamera.transform.position;
        position = uiCamera.WorldToScreenPoint(uiCamera.transform.TransformPoint(position));
        transform.position = position;

        _countdown = true;
        FixedUpdate();
        
        transform.parent.gameObject.SetActive(true);
    }

    private void FixedUpdate()
    {
        if (_countdown)
        {
            progressSlider.value = (float)(1.0 - _timer.SecodsLeft / _timer.TimeToFinish.TotalSeconds);
            timeLeftText.text = _timer.DisplayTime();
        }
    }

    public void SkipButton()
    {
        EventManager.Instance.AddListenerOnce<GameEvent.EnoughCurrencyGameEvent>(OnEnoughCurrency);
        EventManager.Instance.AddListenerOnce<GameEvent.NotEnoughCurrencyGameEvent>(OnNotEnoughCurrency);

        GameEvent.CurrencyChangeGameEvent info =
            new GameEvent.CurrencyChangeGameEvent(-_timer.SkipAmount, CurrencyType.Crystals);
        EventManager.Instance.QueueEvent(info);
    }

    private void OnEnoughCurrency(GameEvent.EnoughCurrencyGameEvent info)
    {
        _timer.SkipTimer();
        skipButton.gameObject.SetActive(false);
        EventManager.Instance.RemoveListener<GameEvent.NotEnoughCurrencyGameEvent>(OnNotEnoughCurrency);
    }

    private void OnNotEnoughCurrency(GameEvent.NotEnoughCurrencyGameEvent info)
    {
        EventManager.Instance.RemoveListener<GameEvent.EnoughCurrencyGameEvent>(OnEnoughCurrency);
    }

    public void HideTimer()
    {
        transform.parent.gameObject.SetActive(false);
        _timer = null;
        _countdown = false;
    }

    public static void ShowTimer_Static(GameObject caller)
    {
        _instance.ShowTimer(caller);
    }

    public static void HideTimer_Static()
    {
        _instance.HideTimer();
    }
}
