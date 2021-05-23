using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSMeetingOrganizer.Data;
using MSMeetingOrganizer.Data.Entities;
using MSMeetingOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSMeetingOrganizer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingsController : ControllerBase
    {

        private readonly IMeetingService meetingService;

        public MeetingsController(IMeetingService meetingService)
        {
            this.meetingService = meetingService;
        }

        [HttpGet]
        public async Task<ActionResult<MeetingDto[]>> Get()
        {
            try
            {
                var results = await meetingService.GetAllAsync();

                return results.Select(x => new MeetingDto
                {
                    Id = x.Id,
                    Topic = x.Topic,
                    Date = x.Date.Value.ToString("d"),
                    StartTime = x.Date.Value.ToString("HH:mm"),
                    EndTime = x.EndTime,
                    Participants = x.Participants.Select(y => new ParticipantDto { Id = y.Id, Fullname = y.Fullname }).ToArray()
                }).ToArray();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MeetingDto>> Get(int id)
        {
            try
            {
                var result = await meetingService.GetAsync(id);
                if (result == null) return NotFound();

                MeetingDto meeting = new MeetingDto
                {
                    Id = result.Id,
                    Topic = result.Topic,
                    Date = result.Date.Value.ToString("d"),
                    DateTime = result.Date,
                    StartTime = result.Date.Value.ToString("HH:mm"),
                    EndTime = result.EndTime,
                    Participants = result.Participants.Select(y => new ParticipantDto { Id = y.Id, Fullname = y.Fullname }).ToArray()
                };

                return meeting;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Meeting>> Put(int id, [FromForm] Meeting meeting)
        {
            try
            {
                var oldMeeting = await meetingService.GetAsync(id);
                if (oldMeeting == null)
                {
                    return NotFound();
                }

                meetingService.Update(meeting, oldMeeting);
                
                if (await meetingService.SaveChangesAsync())
                {
                    return meeting;
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to update the meeting");
        }

        [HttpPost]
        public async Task<ActionResult<Meeting>> Post([FromForm] Meeting meeting)
        {
            try
            {
                var existing = await meetingService.GetAsync(meeting.Id);
                if (existing != null)
                {
                    return BadRequest("Meeting is in use!");
                }
                meetingService.Add(meeting);
                if (await meetingService.SaveChangesAsync())
                {
                    return Ok(meeting);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to create the meeting");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Meeting>> Delete(int id)
        {
            try
            {
                var existing = await meetingService.GetAsync(id);
                if (existing == null)
                {
                    return NotFound();
                }

                meetingService.Delete(existing);

                if (await meetingService.SaveChangesAsync())
                {
                    return Ok();
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the meeting");
        }


    }
}
