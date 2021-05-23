using Microsoft.EntityFrameworkCore;
using MSMeetingOrganizer.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSMeetingOrganizer.Data
{
    public class MeetingContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=MSMeetingOrganizerDB;Trusted_Connection=True;");
        }

        public virtual DbSet<Meeting> Meetings { get; set; }
    }
}
