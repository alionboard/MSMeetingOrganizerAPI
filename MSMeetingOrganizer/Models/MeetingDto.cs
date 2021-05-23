using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSMeetingOrganizer.Models
{
    public class MeetingDto
    {
        public int Id { get; set; }
        public string Topic { get; set; }
        public string Date { get; set; }
        public DateTime? DateTime { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public ICollection<ParticipantDto> Participants { get; set; }
    }
}
