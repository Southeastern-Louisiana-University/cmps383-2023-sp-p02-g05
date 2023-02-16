using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SP23.P02.Web.Data;
using SP23.P02.Web.Extensions;
using SP23.P02.Web.Features.DTOs;
using SP23.P02.Web.Features.Roles;
using SP23.P02.Web.Features.TrainStations;
using SP23.P02.Web.Features.Users;

namespace SP23.P02.Web.Controllers;

[Route("api/stations")]
[ApiController]
public class StationsController : ControllerBase
{
    private readonly DbSet<TrainStation> stations;
    private readonly DataContext dataContext;

    private readonly SignInManager<User> signInManager;
    private readonly UserManager<User> userManager;


    public StationsController(DataContext dataContext, SignInManager<User> signInManager,
        UserManager<User> userManager)
    {
        this.dataContext = dataContext;
        stations = dataContext.Set<TrainStation>();

        this.signInManager = signInManager;
        this.userManager = userManager;
    }

    [HttpGet]
    public IQueryable<TrainStationDto> GetAllStations()
    {
        return GetTrainStationDtos(stations);
    }

    [HttpGet]
    [Route("{id}")]
    public ActionResult<TrainStationDto> GetStationById(int id)
    {
        var result = GetTrainStationDtos(stations.Where(x => x.Id == id)).FirstOrDefault();
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = RoleNames.Admin)] //Roles = Admin
    public ActionResult<TrainStationDto> CreateStation(TrainStationDto dto)
    {
        if (IsInvalid(dto))
        {
            return BadRequest();
        }
        var manager = dataContext.User.FirstOrDefault(x => x.Id == dto.ManagerId);
        if (!string.IsNullOrEmpty(dto.ManagerId.ToString())) //manager is optional, so if ManagerId was not left blank intentionally?
        {
            if (manager == null)
            {
                return BadRequest("The user you were going to put as a manager doesn't exist.");
            }
        }
        var station = new TrainStation
        {
            Name = dto.Name,
            Address = dto.Address,
            Manager = manager
        };
        stations.Add(station);

        dataContext.SaveChanges();

        dto.Id = station.Id;

        return CreatedAtAction(nameof(GetStationById), new { id = dto.Id }, dto);
    }

    [HttpPut]
    [Authorize] //Roles = Admin
    [Route("{id}")]
    public ActionResult<TrainStationDto> UpdateStation(int id, TrainStationDto dto) //wasn't async Task before
    {
        if (IsInvalid(dto))
        {
            return BadRequest();
        }
        var manager = dataContext.User.FirstOrDefault(x => x.Id == dto.ManagerId);
        if (!string.IsNullOrEmpty(dto.ManagerId.ToString()))
        {
            if (manager == null)
            {
                return BadRequest("The user you were going to put as a manager doesn't exist.");
            }
        }

        var station = stations.FirstOrDefault(x => x.Id == id);
        if (station == null)
        {
            return NotFound();
        }

        var username = User.GetCurrentUserName(); //possibly dumb code begins here
        var user = dataContext.User.FirstOrDefault(x => x.UserName == username);

        station.Name = dto.Name;
        station.Address = dto.Address;
        if (!User.IsInRole(RoleNames.Admin) && station.Manager.Id != User.GetCurrentUserId()) //User.IsInRole("Admin"), await userManager.IsInRoleAsync(user, "Admin")
        {
            return Forbid();
        }
        else
        {
            station.Manager = manager;
        }
       
        

        dataContext.SaveChanges();

        dto.Id = station.Id;

        return Ok(dto);
    }

    [HttpDelete]
    [Authorize(Roles = RoleNames.Admin)]
    [Route("{id}")]
    public ActionResult DeleteStation(int id)
    {
        var station = stations.FirstOrDefault(x => x.Id == id);
        if (station == null)
        {
            return NotFound();
        }

        stations.Remove(station);

        dataContext.SaveChanges();

        return Ok();
    }

    private static bool IsInvalid(TrainStationDto dto)
    {
        return string.IsNullOrWhiteSpace(dto.Name) ||
               dto.Name.Length > 120 ||
               string.IsNullOrWhiteSpace(dto.Address);
    }

    private static IQueryable<TrainStationDto> GetTrainStationDtos(IQueryable<TrainStation> stations)
    {
        return stations
            .Select(x => new TrainStationDto
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address,
                ManagerId = x.Manager.Id
            });
    }
}