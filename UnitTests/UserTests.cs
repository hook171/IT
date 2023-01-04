using Xunit;
using Moq;
using domain;

namespace UnitTests;

public class UserTests
{
    private readonly UserService _userService;
    private readonly Mock<IUserRepository> _db;

    public UserTests()
    {
        _db = new Mock<IUserRepository>();
        _userService = new UserService(_db.Object);
    }

    public User GetUser(string username)
    {
        return new User(username, "qwerty123", 1, "89998887766", "Alex Alexandrov", Role.Patient);
    }

    [Fact]
    public void UserAlreadyExists()
    {
        _db.Setup(rep => rep.IsExist(It.Is<string>(s => s == "Gigachad")))
            .Returns(true);

        _db.Setup(rep => rep.IsValid(It.IsAny<User>()))
            .Returns(true);

        var response = _userService.CreateUser(GetUser("Gigachad"));

        Assert.False(response.Success);
        Assert.Equal("User with that username is already exist", response.Error);
    }

    [Fact]
    public void CreateEmptyUsername()
    {
        _db.Setup(rep => rep.IsValid(It.Is<User>(user => string.IsNullOrEmpty(user.Username))))
            .Returns(false);

        var response = _userService.CreateUser(GetUser(""));

        Assert.False(response.Success);
        Assert.Equal("Username is not valid", response.Error);
    }

    [Fact]
    public void EmptyLoginPassword()
    {
        var response = _userService.CheckExist("", "");
        Assert.False(response.Success);
        Assert.Equal("Empty login/password", response.Error);

    }

    [Fact]
    public void CheckCreateLoginPasswordOk()
    {
        _db.Setup(repo => repo.IsExist(
                It.Is<string>(u => u == "Gigachad"),
                It.Is<string>(p => p == "qwerty123")
            )
        ).Returns(true);

        var response = _userService.CheckExist("Gigachad", "qwerty123");

        Assert.True(response.Success);
    }

    [Fact]
    public void SuccessCreate()
    {
        _db.Setup(repo => repo.IsExist(It.IsAny<string>()))
            .Returns(false);
        _db.Setup(repo => repo.IsValid(It.IsAny<User>()))
            .Returns(true);

        var response = _userService.CreateUser(GetUser("Gigachad"));

        Assert.True(response.Success);
    }

    [Fact]
    public void LoginEmptyOrNull()
    {
        var response = _userService.GetByLogin(string.Empty);
        Assert.False(response.Success);
        Assert.Equal("Empty login", response.Error);
    }

    [Fact]
    public void LoginNotFound()
    {
        _db.Setup(rep => rep.GetByLogin(It.IsAny<string>()))
            .Returns(() => null);

        var response = _userService.GetByLogin("Gigachad");

        Assert.False(response.Success);
        Assert.Equal("User with this login doesn't exists", response.Error);
    }
    [Fact]
    public void LoginFound()
    {
        _db.Setup(rep => rep.IsExist(It.Is<string>(s => s == "Gigachad")))
            .Returns(true);
        _db.Setup(rep => rep.GetByLogin(It.Is<string>(s => s == "Gigachad")))
            .Returns(GetUser("Gigachad"));

        var response = _userService.GetByLogin("Gigachad");

        Assert.True(response.Success);
    }

}