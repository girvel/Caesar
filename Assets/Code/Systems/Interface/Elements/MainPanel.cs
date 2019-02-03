using System.Collections.Generic;
using System.Linq;
using Code.Systems.Selection;
using UnityEngine;

namespace Code.Systems.Interface.Elements
{
    public class MainPanel : Panel<MainPanel>
    {
        private Dictionary<string, string> _buildingsDescriptions;
        
        protected override void Awake()
        {
            base.Awake();

            _buildingsDescriptions
                = Resources
                    .Load<TextAsset>("Texts/Buildings descriptions RU").text
                    .Replace("\r", "")
                    .Split('#')
                    .Skip(1)
                    .Select(d => d.Split('\n'))
                    .ToDictionary(
                        keySelector: d => d.First().Substring(1),
                        elementSelector: d =>
                        {
                            var element = d.Skip(1).Aggregate("", (sum, l) => sum + "\n" + l).Substring(1);
                            return element.EndsWith("\n\n")
                                ? element.Substring(0, element.Length - 2)
                                : element;
                        });
        }

        public void RefreshState()
        {
            string text;
            Text = SelectionSystem.Current.Preselection != null
                   && _buildingsDescriptions.TryGetValue(SelectionSystem.Current.Preselection.gameObject.name, out text)
                ? text
                : string.Empty;
        }
    }
}