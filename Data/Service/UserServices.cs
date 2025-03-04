using Microsoft.EntityFrameworkCore;
using DevManager.Data.Context;
using DevManager.Data.Models;
using DevManager.Data.Request;
using DevManager.Data.Response;

namespace DevManager.Data.Service;
public class UserServices : IUserServices
{
    #region Constructor y mienbro privado
    private AppDbContext _database;

    public UserServices(AppDbContext database)
    {
        _database = database;
    }
    #endregion
    //Tarea para registrar un Vehiculo nuevo en la base de datos...
    #region Funciones
    public async Task<Result> Crear(UserRequest request)
    {
        try
        {
            var item = User.Crear(request);
            _database.Users.Add(item);
            await _database.SaveChangesAsync();
            return new Result() { Mensaje = "Ok", Exitoso = true };
        }
        catch (Exception E)
        {

            return new Result() { Mensaje = E.Message, Exitoso = false };
        }
    }
    public async Task<Result> Modificar(UserRequest request)
    {
        try
        {
            var user = await _database.Users
                .FirstOrDefaultAsync(c => c.UserID == request.UserID);
            if (user == null)
                return new Result() { Mensaje = "No se encontró el usuario", Exitoso = false };


            if (user.Modificar(request))
                await _database.SaveChangesAsync();

            return new Result() { Mensaje = "Ok", Exitoso = true };
        }
        catch (Exception E)
        {
            return new Result() { Mensaje = E.Message, Exitoso = false };
        }
    }
    public async Task<Result> Eliminar(UserRequest request)
    {
        try
        {
            var contacto = await _database.Users
                .FirstOrDefaultAsync(c => c.UserID == request.UserID);
            if (contacto == null)
                return new Result() { Mensaje = "No se encontro el usuario", Exitoso = false };

            _database.Users.Remove(contacto);
            await _database.SaveChangesAsync();
            return new Result() { Mensaje = "Ok", Exitoso = true };
        }
        catch (Exception E)
        {

            return new Result() { Mensaje = E.Message, Exitoso = false };
        }
    }
    public async Task<Result<List<UserResponse>>> Consultar(string filtro)
    {
        try
        {
            var usuarios = await _database.Users
                .Where(u =>
                    (u.FirstName + " "+ u.LastName+" "+ u.Email + " "+u.UserRole)
                    .ToLower()
                    .Contains(filtro.ToLower()
                    )
                )
                .Select(u => u.ToResponse())
                .ToListAsync();
            return new Result<List<UserResponse>>()
            {
                Mensaje = "Ok",
                Exitoso = true,
                Datos = usuarios
            };
        }
        catch (Exception E)
        {
            return new Result<List<UserResponse>>
            {
                Mensaje = E.Message,
                Exitoso = false
            };
        }
    }
    public async Task CrearUsuarioAdmin()
    {
        var adminUser = await _database.Users.FirstOrDefaultAsync(u => u.Email == "admin");

        if (adminUser == null)
        {
            adminUser = new User
            {
                FirstName = "Admin",
                LastName = "DevManager",
                Email = "admin",
                PasswordHash = "1234", // Recuerda realizar un hash de la contraseña en un entorno de producción
                UserRole = "Admin"
            };

            _database.Users.Add(adminUser);
            await _database.SaveChangesAsync();
        }
    }
    #endregion

}


public interface IUserServices
{
    Task<Result<List<UserResponse>>> Consultar(string filtro);
    Task<Result> Crear(UserRequest request);
    Task<Result> Eliminar(UserRequest request);
    Task<Result> Modificar(UserRequest request);
    Task CrearUsuarioAdmin();
}