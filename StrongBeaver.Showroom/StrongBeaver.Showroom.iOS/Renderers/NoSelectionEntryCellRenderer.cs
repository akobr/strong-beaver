using StrongBeaver.Showroom.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(EntryCell), typeof(NoSelectionEntryCellRenderer))]

namespace StrongBeaver.Showroom.iOS.Renderers
{
    public class NoSelectionEntryCellRenderer : EntryCellRenderer
    {
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            UITableViewCell cell = base.GetCell(item, reusableCell, tv);
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            return cell;
        }
    }
}
