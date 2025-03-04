using Microsoft.EntityFrameworkCore;
using DevManager.Data;
using DevManager.Data.Models;
using DevManager.Data.Request;
using DevManager.Data.Response;
using DevManager.Data.Context;

namespace DevManager.Data.Service;

public class ProjectServices : IProjectServices
{
    private readonly AppDbContext _database;

    public ProjectServices(AppDbContext database)
    {
        _database = database;
    }

    public async Task<Result> Crear(ProjectRequest request)
    {
        try
        {
            var item = Project.Crear(request);
            _database.Projects.Add(item);
            await _database.SaveChangesAsync();
            return new Result() { Mensaje = "Ok", Exitoso = true };
        }
        catch (DbUpdateException dbEx)
        {
            return new Result() { Mensaje = "Error al actualizar la base de datos: " + dbEx.Message, Exitoso = false };
        }
        catch (Exception ex)
        {
            return new Result() { Mensaje = ex.Message, Exitoso = false };
        }
    }

    public async Task<Result> Modificar(ProjectRequest request)
    {
        try
        {
            var item = await _database.Projects.AsTracking()
                .FirstOrDefaultAsync(c => c.ProjectID == request.ProjectID);
            if (item == null)
                return new Result() { Mensaje = "No se encontró el proyecto", Exitoso = false };

            if (item.Modificar(request))
                await _database.SaveChangesAsync();

            return new Result() { Mensaje = "Ok", Exitoso = true };
        }
        catch (DbUpdateException dbEx)
        {
            return new Result() { Mensaje = "Error al actualizar la base de datos: " + dbEx.Message, Exitoso = false };
        }
        catch (Exception ex)
        {
            return new Result() { Mensaje = ex.Message, Exitoso = false };
        }
    }

    public async Task<Result> Eliminar(ProjectRequest request)
    {
        try
        {
            var item = await _database.Projects.FindAsync(request.ProjectID);
            if (item == null)
                return new Result() { Mensaje = "No se encontró el proyecto", Exitoso = false };

            _database.Projects.Remove(item);
            await _database.SaveChangesAsync();
            return new Result() { Mensaje = "Ok", Exitoso = true };
        }
        catch (DbUpdateException dbEx)
        {
            return new Result() { Mensaje = "Error al eliminar el proyecto: " + dbEx.Message, Exitoso = false };
        }
        catch (Exception ex)
        {
            return new Result() { Mensaje = ex.Message, Exitoso = false };
        }
    }

    public async Task<Result<List<ProjectResponse>>> Consultar(string filtro)
    {
        try
        {
            var items = await _database.Projects
                .Where(u => string.IsNullOrWhiteSpace(filtro) || 
                            (u.ProjectName + " " + u.Description).ToLowerInvariant().Contains(filtro.ToLowerInvariant()))
                .Include(f => f.Creator)
                .ToListAsync();
            var responseItems = items.Select(u => u.ToResponse()).ToList();

            return new Result<List<ProjectResponse>>()
            {
                Mensaje = "Ok",
                Exitoso = true,
                Datos = responseItems
            };
        }
        catch (Exception ex)
        {
            return new Result<List<ProjectResponse>>
            {
                Mensaje = ex.Message,
                Exitoso = false
            };
        }
    }
}

public interface IProjectServices
{
    Task<Result<List<ProjectResponse>>> Consultar(string filtro);
    Task<Result> Crear(ProjectRequest request);
    Task<Result> Eliminar(ProjectRequest request);
    Task<Result> Modificar(ProjectRequest request);
}