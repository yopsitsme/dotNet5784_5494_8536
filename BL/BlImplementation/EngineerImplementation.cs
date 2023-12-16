

namespace BlImplementation;
using BlApi;
using DalApi;
using System.Collections.Generic;
using System.Text.RegularExpressions;


internal class EngineerImplementation : BlApi.IEngineer
{
    private IDal _dal = Factory.Get;

    public int Create(BO.Engineer boEngineer)
    {
        string emailPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
        if (boEngineer.Id<0|| boEngineer.Cost<0|| boEngineer.Name=="" || Regex.IsMatch(boEngineer.Email, emailPattern))
        {
            throw new BO.InvalidInputException("Invalid input");
        }
        DO.Engineer doEngineer = new DO.Engineer(
              boEngineer.Id, boEngineer.Name, boEngineer.Email, (DO.EngineerExperience?)boEngineer.Level, boEngineer.Cost);
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
        _dal.Engineer.Delete(id);
    }

    public BO.Engineer? Read(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        if (doEngineer == null)
            throw new BO.BlDoesNotExistException($"Engneer with ID={id} does Not exist");
        return new BO.Engineer()
        {
            Id = id,
            Name = doEngineer.Name,
            Email = doEngineer.Email,
            Level = (BO.EngineerExperience?)doEngineer.Level,
            Cost = doEngineer.Cost,
            Task =//משימה נוכחית אם קיימת דרך taskinengenner
        };
    }


    public IEnumerable<BO.Engineer> ReadAll()


    {
        return (from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
                select new BO.Engineer
                {
                    Id = doEngineer.Id,
                    Name = doEngineer.Name,
                    Email = doEngineer.Email,
                    Level = (BO.EngineerExperience?)doEngineer.Level,
                    Cost = doEngineer.Cost,
                    Task =//משימה נוכחית אם קיימת דרך taskinengenner
                });
    }

    public void Update(BO.Engineer boEngineer)
    {
        string emailPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
        if (boEngineer.Id < 0 || boEngineer.Cost < 0 || boEngineer.Name == "" || Regex.IsMatch(boEngineer.Email, emailPattern))
        {
            throw new BO.InvalidInputException("Invalid input");
        }
        DO.Engineer doEngineer = new DO.Engineer(
             boEngineer.Id, boEngineer.Name, boEngineer.Email, (DO.EngineerExperience?)boEngineer.Level, boEngineer.Cost);

        try
        {
          _dal.Engineer.Update(doEngineer);
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Student with ID={boEngineer.Id} already exists", ex);
        }
    }
}








