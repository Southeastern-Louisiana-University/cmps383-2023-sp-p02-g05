namespace SP23.P02.Web.Features.TrainStations;

public class TrainStationDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Address { get; set; }

    public int ManagerId  {get; set; }
}

public class LoginDto
{
    public string UserName { get; set; }

    public string Password { get; set; }

}
public class UserDto
{
    public int Id { get; set; }

    public string UserName { get; set; }

    public string Roles { get; set; }
}

public class CreateUserDto
{
    public string UserName { get; set; }

    public string Roles { get; set; }

    public string Password { get; set; }
}