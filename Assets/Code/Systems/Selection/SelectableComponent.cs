using System;
using UnityEngine;

namespace Code.Systems.Selection
{
    public class SelectableComponent : MonoBehaviour
    {
        public SpriteRenderer HighlightRenderer;

        public Sprite SelectedSprite, PreselectedSprite;

        private void OnMouseEnter()
        {
            SelectionSystem.Current.Preselect(this);
        }

        private void OnMouseExit()
        {
            SelectionSystem.Current.Preselect(null);
        }

        private void OnMouseDown()
        {
            SelectionSystem.Current.Select(this);
        }
    }
}