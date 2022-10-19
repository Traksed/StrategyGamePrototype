using System;

public class Building : PlaceableObject
{
    public override void Place()
    {
        base.Place();
        Timer timer = gameObject.AddComponent<Timer>();
        timer.Initialize("Building", DateTime.Now, TimeSpan.FromMinutes(3));
        timer.StartTimer();
        timer.TimerFinishedEvent.AddListener(delegate
        {
            Destroy(timer);
        });
    }

    private void OnMouseUpAsButton()
    {
        TimerTooltip.ShowTimer_Static(gameObject);
    }
}
