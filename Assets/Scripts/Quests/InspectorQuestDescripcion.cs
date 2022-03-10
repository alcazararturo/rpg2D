using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InspectorQuestDescripcion : QuestDescripcion
{
    [SerializeField] private TextMeshProUGUI questRecompensa;
    public override void ConfgurarQuestUI(Quest quest)
    {
        base.ConfgurarQuestUI(quest);
        questRecompensa.text = $"-{quest.RecompensaOro} oro" +
                               $"\n-{quest.RecompensaExp} exp" +
                               $"\n-{quest.Nombre} x{quest.RecompensaItem.Cantidad}";
    } 

    public void AceptarQuest()
    {
        if (QuestPorCompletar == null) { return; }
        QuestManager.Instance.AnadirQuest(QuestPorCompletar);
        gameObject.SetActive(false);
    }

}
