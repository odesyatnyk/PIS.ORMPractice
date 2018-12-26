using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS.ORMPractice.CodeFirst.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<PIS.ORMPractice.CodeFirst.DatabaseContext.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }
    }
}
