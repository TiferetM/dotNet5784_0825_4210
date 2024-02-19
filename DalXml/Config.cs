using System.Xml.Linq;
using Dal;
namespace DalXml;

internal static class Config
{
    static string s_data_config_xml = "data-config";
    internal static int NextDependencyId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextDependencyId"); }
    internal static int NextTaskId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextTaskId"); }
    internal static DateTime StartProjectDate { get => XMLTools.GetDates(s_data_config_xml, "startProjectDate"); }
    internal static DateTime EndProjectDate { get => XMLTools.GetDates(s_data_config_xml, "endProjectDate"); }
}//lkjhgfd