using DTO;
using Microsoft.AspNetCore.SignalR;

namespace Server.Hubs.PlanningAppoimentHub
{
    public class PlanningAppoimentHub :Hub
    {
          public void OnUpdateStatusAppoitment(UpdateStatusAppoimentDto UpdateApoitment)
        {
            Clients.All.SendAsync("ReceiveUpdateStatusAppoitment", UpdateApoitment);
        }
    }
}
