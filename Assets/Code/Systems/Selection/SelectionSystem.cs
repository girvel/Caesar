using Code.Common;

namespace Code.Systems.Selection
{
    public class SelectionSystem : SingletonBehaviour<SelectionSystem>
    {
        public SelectableComponent Selection { get; private set; }
        
        public SelectableComponent Preselection { get; private set; }



        public void Preselect(SelectableComponent target)
        {
            if (Preselection != null && Selection != Preselection)
            {
                Preselection.HighlightRenderer.sprite = null;
            }
            
            Preselection = target;

            if (Preselection != null && Selection != Preselection)
            {
                Preselection.HighlightRenderer.sprite = Preselection.PreselectedSprite;
            }
        }
        
        
        
        public void Select(SelectableComponent target)
        {
            if (Selection != null)
            {
                Selection.HighlightRenderer.sprite = null;
            }
            
            Selection = target;

            if (Selection != null)
            {
                Selection.HighlightRenderer.sprite = Selection.SelectedSprite;
            }
        }
    }
}