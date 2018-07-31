using StrongBeaver.Showroom.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SwitchCell), typeof(NoSelectionSwitchCellRenderer))]

namespace StrongBeaver.Showroom.iOS.Renderers
{
    public class NoSelectionSwitchCellRenderer : SwitchCellRenderer
    {
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            UITableViewCell cell = base.GetCell(item, reusableCell, tv);
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            return cell;
        }
    }
}
