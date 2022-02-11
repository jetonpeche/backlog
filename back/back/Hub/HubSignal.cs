using Microsoft.AspNetCore.SignalR;

namespace back.signal
{
    public class HubSignal: Hub
    {
        public async Task AskServer(string _texteVenantAngular)
        {
            Console.WriteLine(Context.ConnectionId);
            string msgRetour;

            if (_texteVenantAngular == "salut")
                msgRetour = "coucou";
            else
                msgRetour = "hello";

            await Clients.All.SendAsync("askServerReponse", msgRetour);
        }
    }
}
