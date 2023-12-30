

namespace BlImplementation;
using BO;

using System.Collections.Generic;
using System.Text.RegularExpressions;


internal class EngineerImplementation : BlApi.IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public int Create(BO.Engineer boEngineer)
    {
        if (boEngineer.Name == null || boEngineer.Email == null || boEngineer.Cost == null)
            throw new BO.BlNullPropertyException("you can not send a null property");
        string emailPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
        Regex regex = new Regex(emailPattern);
        Match match = regex.Match(boEngineer.Email);
        if (boEngineer.Id < 0 || boEngineer.Cost < 0 || boEngineer.Name == ""|| !match.Success)
        {
            throw new BO.InvalidInputException("Invalid input");
        }
        DO.Engineer doEngineer = Tools.EngineerfromBoToDo(boEngineer);
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
        if (Tools.TaskinEngineer(id) != null)
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
        return Tools.EngineerfromDoToBo(doEngineer);
    }


    public IEnumerable<BO.Engineer> ReadAll(Func<BO.Engineer, bool>? filter=null)
    {

        var listEngineer = (from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
                            select Tools.EngineerfromDoToBo(doEngineer));
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
        Regex regex = new Regex(emailPattern);
        Match match = regex.Match(boEngineer.Email); if (boEngineer.Id < 0 || boEngineer.Cost < 0 || boEngineer.Name == "" || !match.Success)
        {
            throw new BO.InvalidInputException("Invalid input");
        }
        DO.Engineer doEngineer =Tools.EngineerfromBoToDo(boEngineer);

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









