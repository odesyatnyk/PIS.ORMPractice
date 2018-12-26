using PIS.ORMPractice.CodeFirst.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS.ORMPractice.CodeFirst.DatabaseContext
{
    public class Context : DbContext
    {
        public Context()
            : base("KraftHeinz_cf")
        {

        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Position> Positions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<Context>(new ContextInitializer());
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Position>()
                        .HasRequired<Department>(s => s.Department)
                        .WithMany(g => g.Positions)
                        .HasForeignKey<int>(s => s.IdDepartment);
        }
    }
}
