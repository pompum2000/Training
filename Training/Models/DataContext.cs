using System.Data;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Training.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                  .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json")
                  .Build();
                var connectionString = "Server=DESKTOP-R5FEB6F;Database=Training;uid=sa;pwd=sa;"; /*"Server=DESKTOP-R5FEB6F;Initial Catalog=Training;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False";*/
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            this.DepartmentMapping(modelBuilder);


            this.NhanVienMapping(modelBuilder);


            RelationshipsMapping(modelBuilder);

        }
        private void DepartmentMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().ToTable(@"Department", @"dbo");
            modelBuilder.Entity<Department>().Property(x => x.IdDepartment).HasColumnName(@"IdDepartment").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0);
            modelBuilder.Entity<Department>().Property(x => x.NameDepartment).HasColumnName(@"NameDepartment").HasColumnType(@"nvarchar(20)").IsRequired().ValueGeneratedNever().HasMaxLength(20);
            modelBuilder.Entity<Department>().HasKey(@"IdDepartment");
        }
        private void NhanVienMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NhanVien>().ToTable(@"NhanVien", @"dbo");
            modelBuilder.Entity<NhanVien>().Property(x => x.IdNv).HasColumnName(@"IdNv").HasColumnType(@"int").IsRequired().ValueGeneratedOnAdd().HasPrecision(10, 0);
            modelBuilder.Entity<NhanVien>().Property(x => x.Name).HasColumnName(@"Name").HasColumnType(@"nvarchar(20)").IsRequired().ValueGeneratedNever().HasMaxLength(20);
            modelBuilder.Entity<NhanVien>().Property(x => x.Position).HasColumnName(@"Position").HasColumnType(@"nvarchar(20)").IsRequired().ValueGeneratedNever().HasMaxLength(20);
            modelBuilder.Entity<NhanVien>().Property(x => x.IdDepartment).HasColumnName(@"IdDepartment").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0);
            modelBuilder.Entity<NhanVien>().HasKey(@"IdNv");
        }
        private void RelationshipsMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().HasMany(x => x.NhanVien).WithOne(op => op.Department).HasForeignKey(@"IdDepartment").IsRequired(true);

            modelBuilder.Entity<NhanVien>().HasOne(x => x.Department).WithMany(op => op.NhanVien).HasForeignKey(@"IdDepartment").IsRequired(true);
        }
        public bool HasChanges()
        {
            return ChangeTracker.Entries().Any(e => e.State == Microsoft.EntityFrameworkCore.EntityState.Added || e.State == Microsoft.EntityFrameworkCore.EntityState.Modified || e.State == Microsoft.EntityFrameworkCore.EntityState.Deleted);
        }
        public  DbSet<Department> Department
        {
            get;
            set;
        }
        public  DbSet<NhanVien> NhanVien
        {
            get;
            set;
        }
        public List<SelectAll_Result> SelectAll()
        {

            List<SelectAll_Result> result = new List<SelectAll_Result>();
            DbConnection connection = this.Database.GetDbConnection();
            bool needClose = false;
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
                needClose = true;
            }

            try
            {
                using (DbCommand cmd = connection.CreateCommand())
                {
                    if (this.Database.GetCommandTimeout().HasValue)
                        cmd.CommandTimeout = this.Database.GetCommandTimeout().Value;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = @"dbo.SelectAll";
                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        var fieldNames = Enumerable.Range(0, reader.FieldCount).Select(i => reader.GetName(i)).ToArray();
                        while (reader.Read())
                        {
                            SelectAll_Result row = new SelectAll_Result();
                            if (fieldNames.Contains("IdNv") && !reader.IsDBNull(reader.GetOrdinal("IdNv")))
                                row.IdNv = (int)Convert.ChangeType(reader.GetValue(reader.GetOrdinal(@"IdNv")), typeof(int));

                            if (fieldNames.Contains("NameDepartment") && !reader.IsDBNull(reader.GetOrdinal("NameDepartment")))
                                row.NameDepartment = (string)Convert.ChangeType(reader.GetValue(reader.GetOrdinal(@"NameDepartment")), typeof(string));

                            if (fieldNames.Contains("Name") && !reader.IsDBNull(reader.GetOrdinal("Name")))
                                row.Name = (string)Convert.ChangeType(reader.GetValue(reader.GetOrdinal(@"Name")), typeof(string));

                            if (fieldNames.Contains("Position") && !reader.IsDBNull(reader.GetOrdinal("Position")))
                                row.Position = (string)Convert.ChangeType(reader.GetValue(reader.GetOrdinal(@"Position")), typeof(string));

                            result.Add(row);
                        }
                    }
                }
            }
            finally
            {
                if (needClose)
                    connection.Close();
            }
            return result;
        }
        public void insert_NhanVien(string Name, string Position, int? IdDepartment)
        {

            DbConnection connection = this.Database.GetDbConnection();
            bool needClose = false;
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
                needClose = true;
            }

            try
            {
                using (DbCommand cmd = connection.CreateCommand())
                {
                    if (this.Database.GetCommandTimeout().HasValue)
                        cmd.CommandTimeout = this.Database.GetCommandTimeout().Value;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = @"dbo.insert_NhanVien";

                    DbParameter NameParameter = cmd.CreateParameter();
                    NameParameter.ParameterName = "Name";
                    NameParameter.Direction = ParameterDirection.Input;
                    NameParameter.DbType = DbType.String;
                    NameParameter.Size = 20;
                    if (Name != null)
                    {
                        NameParameter.Value = Name;
                    }
                    else
                    {
                        NameParameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(NameParameter);

                    DbParameter PositionParameter = cmd.CreateParameter();
                    PositionParameter.ParameterName = "Position";
                    PositionParameter.Direction = ParameterDirection.Input;
                    PositionParameter.DbType = DbType.String;
                    PositionParameter.Size = 20;
                    if (Position != null)
                    {
                        PositionParameter.Value = Position;
                    }
                    else
                    {
                        PositionParameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(PositionParameter);

                    DbParameter IdDepartmentParameter = cmd.CreateParameter();
                    IdDepartmentParameter.ParameterName = "IdDepartment";
                    IdDepartmentParameter.Direction = ParameterDirection.Input;
                    IdDepartmentParameter.DbType = DbType.Int32;
                    IdDepartmentParameter.Precision = 10;
                    IdDepartmentParameter.Scale = 0;
                    if (IdDepartment.HasValue)
                    {
                        IdDepartmentParameter.Value = IdDepartment.Value;
                    }
                    else
                    {
                        IdDepartmentParameter.Size = -1;
                        IdDepartmentParameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(IdDepartmentParameter);
                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                if (needClose)
                    connection.Close();
            }
        }
        public List<SelectAllid> SelectAllid(int? IdNv, int? IdNv2)
        {

            List<SelectAllid> result = new List<SelectAllid>();
            DbConnection connection = this.Database.GetDbConnection();
            bool needClose = false;
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
                needClose = true;
            }

            try
            {
                using (DbCommand cmd = connection.CreateCommand())
                {
                    if (this.Database.GetCommandTimeout().HasValue)
                        cmd.CommandTimeout = this.Database.GetCommandTimeout().Value;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = @"dbo.SelectAllid";

                    DbParameter IdNvParameter = cmd.CreateParameter();
                    IdNvParameter.ParameterName = "IdNv";
                    IdNvParameter.Direction = ParameterDirection.Input;
                    IdNvParameter.DbType = DbType.Int32;
                    IdNvParameter.Precision = 10;
                    IdNvParameter.Scale = 0;
                    if (IdNv.HasValue)
                    {
                        IdNvParameter.Value = IdNv.Value;
                    }
                    else
                    {
                        IdNvParameter.Size = -1;
                        IdNvParameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(IdNvParameter);

                    DbParameter IdNv2Parameter = cmd.CreateParameter();
                    IdNv2Parameter.ParameterName = "IdNv2";
                    IdNv2Parameter.Direction = ParameterDirection.Input;
                    IdNv2Parameter.DbType = DbType.Int32;
                    IdNv2Parameter.Precision = 10;
                    IdNv2Parameter.Scale = 0;
                    if (IdNv2.HasValue)
                    {
                        IdNv2Parameter.Value = IdNv2.Value;
                    }
                    else
                    {
                        IdNv2Parameter.Size = -1;
                        IdNv2Parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(IdNv2Parameter);
                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        var fieldNames = Enumerable.Range(0, reader.FieldCount).Select(i => reader.GetName(i)).ToArray();
                        while (reader.Read())
                        {
                            SelectAllid row = new SelectAllid();
                            if (fieldNames.Contains("IdNv") && !reader.IsDBNull(reader.GetOrdinal("IdNv")))
                                row.IdNv = (int)Convert.ChangeType(reader.GetValue(reader.GetOrdinal(@"IdNv")), typeof(int));

                            if (fieldNames.Contains("Name") && !reader.IsDBNull(reader.GetOrdinal("Name")))
                                row.Name = (string)Convert.ChangeType(reader.GetValue(reader.GetOrdinal(@"Name")), typeof(string));

                            if (fieldNames.Contains("Position") && !reader.IsDBNull(reader.GetOrdinal("Position")))
                                row.Position = (string)Convert.ChangeType(reader.GetValue(reader.GetOrdinal(@"Position")), typeof(string));

                            if (fieldNames.Contains("NameDepartment") && !reader.IsDBNull(reader.GetOrdinal("NameDepartment")))
                                row.NameDepartment = (string)Convert.ChangeType(reader.GetValue(reader.GetOrdinal(@"NameDepartment")), typeof(string));

                            result.Add(row);
                        }
                    }
                }
            }
            finally
            {
                if (needClose)
                    connection.Close();
            }
            return result;
        }
    }
}
