﻿using Core;
using Core.Abstractions.Services;
using Domain.Commands;
using Domain.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateTaskCommandResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(CreateTaskCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _taskService.CreateTaskCommandHandler(command);

            return Created($"/api/tasks/{result.Payload.Id}", result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UpdateTaskCommandResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(Guid id, UpdateTaskCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _taskService.UpdateTaskCommandHandler(command);

                return Ok(result);
            }
            catch (NotFoundException<Guid>)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetAllTaskQueryResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _taskService.GetAllTasksQueryHandler();

            return Ok(result);
        }

        [HttpGet]
        [Route("Get-All-Tasks-With-MemberAvatar")]
        [ProducesResponseType(typeof(GetAllTaskQueryResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllTasksWithMemberAvatar()
        {
            var result = await _taskService.GetAllTasksWithMemberAvatarQueryHandler();
            return Ok(result);
        }
    }
}
