

namespace BlImplementation;
using BO;
using DalApi;
using System.Collections.Generic;
using System.Text.RegularExpressions;


internal class EngineerImplementation : BlApi.IEngineer
{
    private IDal _dal = Factory.Get;

    public int Create(BO.Engineer boEngineer)
    {
        if (boEngineer.Name == null || boEngineer.Email == null || boEngineer.Cost == null)
            throw new BO.BlNullPropertyException("you can not send a null property");
        string emailPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
        if (boEngineer.Id < 0 || boEngineer.Cost < 0 || boEngineer.Name == ""|| boEngineer.Email==null || Regex.IsMatch(boEngineer.Email, emailPattern))
        {
            throw new BO.InvalidInputException("Invalid input");
        }
        DO.Engineer doEngineer = fromBoToDoEngineer(boEngineer);
        try
        {
            int ideng = _dal.Engineer.Create(doEngineer);
            return ideng;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Student with ID={boEngineer.Id} already exists", ex);
        }
    }

    public void Delete(int id)
    {
        if (TaskinEngineer(id) != null)
        {
            throw new BO.BlDeletionImpossible($" Can't delete engineer with ID={id} ");
        }
        else
        {
            try
            { 
            _dal.Engineer.Delete(id);
        }
            catch (DO.DalDoesNotExistException ex)
            {
                throw new BO.BlDoesNotExistException($"Student with ID={id} already exists", ex);
            }
        }
    }

    public BO.Engineer? Read(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        if (doEngineer == null)
            throw new BO.BlDoesNotExistException($"Engneer with ID={id} does Not exist");
        return fromDoToBoEngineer(doEngineer);
    }


    public IEnumerable<BO.Engineer> ReadAll(Func<BO.Engineer, bool>? filter=null)
    {
       
          var listEngineer= (from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
                    select new BO.Engineer
                    {
                        Id = doEngineer.Id,
                        Name = doEngineer.Name,
                        Email = doEngineer.Email,
                        Level = (BO.EngineerExperience)doEngineer.Level,
                        Cost = doEngineer.Cost,
                        Task = TaskinEngineer(doEngineer.Id)
                    });
        if (filter != null)
        {
            return listEngineer.Where(filter);
        }
        else
        {
            return listEngineer;
        }
     
    }

    public void Update(BO.Engineer boEngineer)
    {
        if(boEngineer.Name == null || boEngineer.Email == null|| boEngineer.Cost==null)
            throw new BO.BlNullPropertyException("you can not send a null property");
        string emailPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
        if (boEngineer.Id < 0 || boEngineer.Cost < 0 || boEngineer.Name == "" || Regex.IsMatch(boEngineer.Email, emailPattern))
        {
            throw new BO.InvalidInputException("Invalid input");
        }
        DO.Engineer doEngineer = fromBoToDoEngineer(boEngineer);

        try
        {
            _dal.Engineer.Update(doEngineer);
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Student with ID={boEngineer.Id} already exists", ex);
        }
    }
    public TaskinEngineer? TaskinEngineer(int Id)
    {
        return (from DO.Task doTask in _dal.Task.ReadAll()
                where doTask.EngineerId == Id
                select new TaskinEngineer
                {
                    Id = doTask.Id,
                    Alias = doTask.Alias
                }).FirstOrDefault();// FirstOrDefaultכתוב בתיאור הכללי שמהנדס יכול לעבוד על משימה אחת בו אבל גם יתכן שךא מוגדרת לו אף משימה ולכן ניתן לןהשתמש בפונקציה 
    }
    public DO.Engineer fromBoToDoEngineer(BO.Engineer boEngineer)
    {
        return new DO.Engineer(
        boEngineer.Id, boEngineer.Name, boEngineer.Email, (DO.EngineerExperience)boEngineer.Level, boEngineer.Cost);
    }

    public BO.Engineer fromDoToBoEngineer(DO.Engineer doEngineer)
    {
        return new BO.Engineer
        {
            Id = doEngineer.Id,
            Name = doEngineer.Name,
            Email = doEngineer.Email,
            Level = (BO.EngineerExperience)doEngineer.Level,
            Cost = doEngineer.Cost,
            Task = TaskinEngineer(doEngineer.Id)
        };
        }
            
}









