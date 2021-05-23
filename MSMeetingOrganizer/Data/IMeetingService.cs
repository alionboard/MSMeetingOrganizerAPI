using MSMeetingOrganizer.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSMeetingOrganizer.Data
{
    public interface IMeetingService
    {
        void Add(Meeting meeting);
        void Delete(Meeting meeting);
        Task<bool> SaveChangesAsync();
        Meeting Update(Meeting meeting);
        Task<Meeting[]> GetAllAsync(bool includeParticipants = true);
        Task<Meeting> GetAsync(int id);

    }
}
