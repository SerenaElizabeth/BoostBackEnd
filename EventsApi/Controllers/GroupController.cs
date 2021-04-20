﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;


[ApiController]
[Route("[controller]s")]
public class GroupController : ControllerBase
{
  private readonly IRepository<Group> _groupRepository;

  public GroupController(IRepository<Group> groupRepository)
  {
    _groupRepository = groupRepository;
  }

  [HttpGet]
  public async Task<IActionResult> GetAll(string name)
  {
    try
    { 
      if (name != null)
      {
        var searchByNameResults = await _groupRepository.Search(name);
        return Ok(searchByNameResults);
      }
    var allGroups = await _groupRepository.GetAll();
    return Ok(allGroups);
    }
    catch (Exception)
    {
      return NotFound("Sorry, out of order.");
    }
  }

  [HttpGet("{id}")]

  public async Task<IActionResult> GetById(long id)
  {
    try
    {
      var returnedGroup = await _groupRepository.Get(id);
      return Ok(returnedGroup);
    }
    catch (Exception)
    {
      return NotFound("Group not found. Are you sure you have the right id?");
    }
  }

  [HttpPost]

  public async Task<IActionResult> Post([FromBody] Group groupToPost)
  {
    try
    {
      var postedGroup = await _groupRepository.Insert(groupToPost);
      return Created($"/groups/{postedGroup.Id}", postedGroup);
    }
    catch (Exception)
    {
      return BadRequest("Sorry can not insert your group, is it valid?");
    }
  }

  [HttpPut("{id}")]

  public async Task<IActionResult> Put(long id, [FromBody] Group groupToPut)
  {
    try
    {
      groupToPut.Id = id;
      var updatedgroup = await _groupRepository.Update(groupToPut);
      return Ok(updatedgroup);
    }
    catch (Exception)
    {
      return BadRequest("Sorry can not update your Group. Is your id and your Group valid?");
    }
  }

  [HttpDelete("{id}")]
  public IActionResult Delete(long id)
  {
    try
    {
      _groupRepository.Delete(id);
      return Ok($"Group at id {id} is successfully deleted.");
    }
    catch (Exception)
    {
      return BadRequest($"Sorry, group of id {id} cannot be deleted, since it does not exit.\nAre you sure the id is correct?");
    }
  }

}