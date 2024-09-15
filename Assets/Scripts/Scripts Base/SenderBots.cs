using UnityEngine;
using System.Linq;

public class SenderBots : MonoBehaviour
{
    [SerializeField] private Base _base;
    [SerializeField] private ResourceWarehouse _resourceWarehouse;
    [SerializeField] private Beacon _beacon;

    private int _priceBase = 5;
    private int _minQuantityBots = 2;

    private void Update()
    {
        if (_beacon.IsActivated == true && _beacon.CanBeMoved == true)
            SendBotBuild();
    }

    private void SendBotBuild()
    {
        if (_resourceWarehouse.ResourceCount >= _priceBase)
        {
            var freeBot = _base.Bots
                .FirstOrDefault(bot => bot.DesignatedResource == null && bot.DesignatedBeacon == null && _base.Bots.Count >= _minQuantityBots);

            if (freeBot != null)
            {
                freeBot.RemoveDesignatedBase();
                freeBot.AssignBeacon(_beacon);
                freeBot.GoToBeacon();

                _base.RemoveBot(freeBot);
                _beacon.ProhibitMoving();
                _resourceWarehouse.RemoveResource(_priceBase);
            }
        }
    }
}
