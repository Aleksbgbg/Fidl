namespace Fidl.Helpers
{
    using System.Windows.Controls;
    using System.Windows.Interactivity;

    internal class FocusOnVisibleBehavior : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            AssociatedObject.IsVisibleChanged += (sender, e) =>
            {
                if (!(bool)e.NewValue) return;

                AssociatedObject.Focus();
                AssociatedObject.SelectAll();
            };
        }
    }
}