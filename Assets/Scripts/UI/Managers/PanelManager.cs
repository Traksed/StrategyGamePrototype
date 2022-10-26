using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PanelManager : Singleton<PanelManager>
{
    public List<PanelModel> Panels;

    private Queue<PanelInstanceModel> _queue = new Queue<PanelInstanceModel>();
    
    public void ShowPanel(string panelId)
    {
        PanelModel panelModel = Panels.FirstOrDefault(panel => panel.PanelId == panelId);

        if (panelModel != null)
        {
            var newInstancePanel = Instantiate(panelModel.PanelPrefab, transform);
            
            _queue.Enqueue(new PanelInstanceModel
            {
                PanelId = panelId,
                PanelInstance = newInstancePanel
            });
        }
        else
        {
            Debug.LogWarning($"Trying to use panelId = {panelId}, but this is not found in Panels");
        }
    }
}
