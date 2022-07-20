using Microsoft.AspNetCore.SignalR;

namespace Training.Hubs
{
    public class SignalrServer : Hub
    {
        //public async Task LoadNhanVien(int NvId, string Name, string Position, int IdDepartment)
        //{
        //    await Clients.All.SendAsync("LoadNhanVien", NvId, Name, Position,IdDepartment);
        //}
    }
}
