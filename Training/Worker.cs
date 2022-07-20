
using Training;
using System.Timers;
using Training.Controllers;
using Training.Models;
using System.Data;
using System.Data.SqlClient;

namespace Training
{
    public class Worker : BackgroundService
    {
        
        private readonly ILogger<Worker> _logger;
       

        public Worker(ILogger<Worker> logger) => (_logger) = (logger);
       
     
        public void GetData()
        {
            string connectionString = "Server=DESKTOP-R5FEB6F;Database=Training;uid=sa;pwd=sa;";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "insert_NhanVien";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = con;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        
                    }
                }
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

           
                while (!stoppingToken.IsCancellationRequested)
                {

                         GetData();

                        _logger.LogWarning("Dang add user: {time}", DateTimeOffset.Now );
                    

                    await Task.Delay(TimeSpan.FromMinutes(60), stoppingToken);
                }
            
        }

       

        

    }
}
