using Microsoft.EntityFrameworkCore;
using MSMeetingOrganizer.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSMeetingOrganizer.Data
{
    public class MeetingService : IMeetingService
    {
        private readonly MeetingContext meetingContext;

        public MeetingService(MeetingContext meetingContext)
        {
            this.meetingContext = meetingContext;
        }

        public void Add(Meeting meeting)
        {
            meetingContext.Add(meeting);
        }

        public void Delete(Meeting meeting)
        {
            meetingContext.Remove(meeting);
        }

        public async Task<Meeting[]> GetAllAsync(bool includeParticipants = true)
        {
            IQueryable<Meeting> query = meetingContext.Meetings;
            if (includeParticipants)
            {
                query = query.Include(m => m.Participants);
            }
            return await query.OrderBy(d => d.Date).ToArrayAsync();
        }

        public async Task<Meeting> GetAsync(int id)
        {
            return await meetingContext.Meetings.Include(m => m.Participants).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await meetingContext.SaveChangesAsync()) > 0;
        }

        public Meeting Update(Meeting meeting)
        {
            meetingContext.Update(meeting);
            return meeting;
        }
    }
}
