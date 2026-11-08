using System.Drawing.Drawing2D;

namespace AI_Assistant
{
    /// <summary>
    /// Flat, rounded buttons for the status bar: an accent outline, a light fill on hover, grey when disabled.
    /// A label with its own back color is shown as a rounded highlight.
    /// </summary>
    internal class SleekRenderer : ToolStripProfessionalRenderer
    {
        private static readonly Color Accent = Color.FromArgb(66, 133, 244);

        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            if (e.Item is not ToolStripButton button)
            {
                base.OnRenderButtonBackground(e);
                return;
            }

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            var bounds = new Rectangle(2, 2, button.Width - 5, button.Height - 5);
            Color fill = !button.Enabled ? Color.Transparent
                       : button.Pressed ? Color.FromArgb(200, 220, 250)
                       : button.Selected ? Color.FromArgb(225, 238, 252)
                       : Color.White;
            Color border = button.Enabled ? Accent : Color.FromArgb(205, 205, 205);

            using GraphicsPath path = RoundedRectangle(bounds, 8);
            using var fillBrush = new SolidBrush(fill);
            using var borderPen = new Pen(border);
            e.Graphics.FillPath(fillBrush, path);
            e.Graphics.DrawPath(borderPen, path);
        }

        // A status label with its own back color (the thinking level, when the model takes one) gets a rounded highlight.
        protected override void OnRenderToolStripStatusLabelBackground(ToolStripItemRenderEventArgs e)
        {
            if (e.Item.BackColor != e.ToolStrip.BackColor)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                var bounds = new Rectangle(4, 2, e.Item.Width - 7, e.Item.Height - 5);
                using GraphicsPath path = RoundedRectangle(bounds, 6);
                using var fillBrush = new SolidBrush(e.Item.BackColor);
                e.Graphics.FillPath(fillBrush, path);
            }
            base.OnRenderToolStripStatusLabelBackground(e); // the border
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            if (e.Item is ToolStripButton button)
            {
                e.TextColor = button.Enabled ? Accent : Color.Gray;
            }
            base.OnRenderItemText(e);
        }

        private static GraphicsPath RoundedRectangle(Rectangle bounds, int radius)
        {
            int d = radius * 2;
            var path = new GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
            path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
            path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
