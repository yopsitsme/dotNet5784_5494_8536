using System;
namespace Dal;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;



internal static class Config
{
    static string s_data_config_xml = "data-config";
    internal static int nextTaskId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextTaskId"); }
    internal static int nextDependencyId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextDependencyId"); }

}
