using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSMeetingOrganizer.Models
{
    public class ParticipantDto
    {
        public int Id { get; set; }
        public string Fullname { get; set; }

    }
}
