
namespace Dal;

using DalApi;
using DO;
using System.Reflection.Metadata.Ecma335;
using System.Xml;
using System.Xml.Linq;

internal class DependencyImplementation : IDependency
{
    public int Create(Dependency item)
    {
        XElement xmlDependency = XMLTools.LoadListFromXMLElement("dependencies");
        Dependency newDependency = item with { Id = Config.NextDependencyId };
        xmlDependency.Add(new XElement("Dependency",
                                        new XAttribute("Id", newDependency.Id),
                                        new XAttribute("DependentTask", newDependency.DependentTask),
                                        new XAttribute("DependsTask", newDependency.DependsTask)));
        XMLTools.SaveListToXMLElement(xmlDependency, "dependencies");
        return newDependency.Id;
    }

    public void Delete(int id)
    {
        XElement xmlDependency = XMLTools.LoadListFromXMLElement("dependencies");
        XElement? XmlElement = xmlDependency.Descendants("Dependency")
         .FirstOrDefault(elmn => elmn.Attribute("Id")!.Value.Equals(id))
         ?? throw new DalDoesNotExistException(($"Dependency with ID={id} does Not exist"));
        XmlElement.Remove();
        XMLTools.SaveListToXMLElement(xmlDependency, "dependencies");
    }

    public Dependency? Read(int id)
    {
        XElement xmlDependency = XMLTools.LoadListFromXMLElement("dependencies");
        XElement? XmlElement = xmlDependency.Descendants("Dependency")
            .FirstOrDefault(elmn => elmn.Attribute("Id")!.Value.Equals(id));
        if (XmlElement == null)
            return null;
        return new Dependency (id, XmlElement.ToIntNullable("DependentTask") ?? 0, XmlElement.ToIntNullable("DependsTask") ?? 0);   //מכיון שאני בטוחה בכך שלעולם לא אקבל ערך נאל אני מכניסה 0 בגל שאם מספר הזהות לא נמצא הפונקציה לא מגיעה לשלב זה   
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {

        XElement xmlDependency = XMLTools.LoadListFromXMLElement("dependencies");
            Dependency? dependency = (from item in xmlDependency.Descendants("Dependency")
                                      where filter(new Dependency(Convert.ToInt32(item.Attribute("Id")?.Value), 
                                      Convert.ToInt32(item.Attribute("DependentTask")?.Value),
                                      Convert.ToInt32(item.Attribute("DependentTask")?.Value)))
                                      select new Dependency(Convert.ToInt32(item.Attribute("Id")?.Value),
                                      Convert.ToInt32(item.Attribute("DependentTask")?.Value),
                                      Convert.ToInt32(item.Attribute("DependentTask")?.Value))).FirstOrDefault();

            return dependency != null ? dependency : null;




    }

    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
    {
        XElement xmlDependency = XMLTools.LoadListFromXMLElement("dependencies");

        if (filter != null)
        {

            return (from item in xmlDependency.Descendants("dependency")
                    where filter(new Dependency(Convert.ToInt32(item.Attribute("Id")?.Value)
                , Convert.ToInt32(item.Attribute("DependentTask")?.Value)
                , Convert.ToInt32(item.Attribute("DependentTask")?.Value)))
                    select new Dependency(Convert.ToInt32(item.Attribute("Id")?.Value)
, Convert.ToInt32(item.Attribute("DependentTask")?.Value)
, Convert.ToInt32(item.Attribute("DependentTask")?.Value))); ;
        }
        return (from item in xmlDependency.Descendants("dependency")
                select new Dependency(Convert.ToInt32(item.Attribute("Id")?.Value)
                , Convert.ToInt32(item.Attribute("DependentTask")?.Value)
                , Convert.ToInt32(item.Attribute("DependentTask")?.Value)));
    }

    public void Update(Dependency item)
    {
        XElement xmlDependency = XMLTools.LoadListFromXMLElement("dependencies");

        XElement? XmlElement = xmlDependency.Descendants("Dependency")
         .FirstOrDefault(elmn => elmn.Attribute("Id")!.Value.Equals(item.Id))
         ?? throw new DalDoesNotExistException(($"Dependency with ID={item.Id} does Not exist"));
        XmlElement.Attribute("DependentTask")!.Value = item.DependentTask.ToString();
        XmlElement.Attribute("DependsTask")!.Value = item.DependsTask.ToString();
        XMLTools.SaveListToXMLElement(xmlDependency, "dependencies");

        
    }
}
