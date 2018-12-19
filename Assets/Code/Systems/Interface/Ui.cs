using System.Linq;
using Imperium.CommonData;

namespace Code.Systems.Interface
{
    public static class Ui
    {
        private static readonly string[] ResourceNames =
        {
            "Дерево",
            "Еда",
            "Сырая еда",
            "Инструменты",
            "Пшеница",
            "Уголь",
        };
        
        public static void ShowResources(float[] resources)
        {
            var resourcesString
                = resources
                    .Select((r, i) => 
                        string.Format(
                            "\n{0}: {1}", ResourceNames[i], (int) r))
                    .Where(s => !s.EndsWith(" 0"))
                    .Aggregate("", (sum, r) => sum + r);

            UiController.Current.ResourcesText.text 
                = resourcesString == ""
                    ? "" 
                    : resourcesString.Substring(1);
        }
    }
}