using Microsoft.EntityFrameworkCore;
using DevManager.Data;
using DevManager.Data.Models;
using DevManager.Data.Request;
using DevManager.Data.Response;
using DevManager.Data.Context;

namespace DevManager.Data.Service;

public class TareaServices : ITareaServices
{
    private readonly AppDbContext _database;

    public TareaServices(AppDbContext database)
    {
        _database = database;
    }

    public async Task<Result> Crear(TareaRequest request)
    {
        try
        {
            var item = Tarea.Crear(request);
            _database.Tareas.Add(item);
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

    public async Task<Result> Modificar(TareaRequest request)
    {
        try
        {
            var item = await _database.Tareas.AsTracking()
                .FirstOrDefaultAsync(c => c.TaskID == request.TaskID);
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

    public async Task<Result> Eliminar(TareaRequest request)
    {
        try
        {
            var item = await _database.Tareas.FindAsync(request.TaskID);
            if (item == null)
                return new Result() { Mensaje = "No se encontró el proyecto", Exitoso = false };

            _database.Tareas.Remove(item);
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

    public async Task<Result<List<TareaResponse>>> Consultar(string filtro)
    {
        try
        {
            var items = await _database.Tareas
                .Where(u => string.IsNullOrWhiteSpace(filtro) || 
                            (u.TaskName + " " + u.Description).ToLowerInvariant().Contains(filtro.ToLowerInvariant()))
                .Include(f => f.Project)
                .OrderBy(u => u.Project.ProjectName)
                .ToListAsync();
            var responseItems = items.Select(u => u.ToResponse()).ToList();

            return new Result<List<TareaResponse>>()
            {
                Mensaje = "Ok",
                Exitoso = true,
                Datos = responseItems
            };
        }
        catch (Exception ex)
        {
            return new Result<List<TareaResponse>>
            {
                Mensaje = ex.Message,
                Exitoso = false
            };
        }
    }
    
    public async Task<List<TareaResponse>> GetTasksByUserId(int userId)
    {
        return await _database.Tareas
            .Include(t => t.Project)
            .Where(t => t.AssignedTo == userId)
            .Select(t => new TareaResponse 
            {
                TaskID = t.TaskID,
                TaskName = t.TaskName,
                Description = t.Description,
                Priority = t.Priority,
                Status = t.Status,
                AssignedTo = t.AssignedTo
            })
            .ToListAsync();
    }

}

public interface ITareaServices
{
    Task<Result<List<TareaResponse>>> Consultar(string filtro);
    Task<Result> Crear(TareaRequest request);
    Task<Result> Eliminar(TareaRequest request);
    Task<Result> Modificar(TareaRequest request);
    Task<List<TareaResponse>> GetTasksByUserId(int userId);
}