using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSMeetingOrganizer.Data.Entities
{
    [Table("Meetings")]
    public class Meeting : IEntity
    {
        public int Id { get; set; }
        [Required]
        public string Topic { get; set; }
        public DateTime? Date { get; set; }
        public string EndTime { get; set; }
        public ICollection<Participant> Participants { get; set; }
    }
}
