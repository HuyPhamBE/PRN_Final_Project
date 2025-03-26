using Microsoft.AspNetCore.SignalR;

namespace PRN_Final_Project.Hubs
{
    public class BookingHub : Hub
    {
        public async Task SendBookingUpdate(string bookingId, string status)
        {
            await Clients.All.SendAsync("ReceiveBookingUpdate", bookingId, status);
        }
    }
}
