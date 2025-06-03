using System.Drawing;
using System.Windows.Forms;

public class CustomMenuRenderer : ToolStripProfessionalRenderer
{
    private SolidBrush baseBrush = new SolidBrush(ColorTranslator.FromHtml("#005187"));        // Azul inactivo
    private SolidBrush hoverBrush = new SolidBrush(ColorTranslator.FromHtml("#007ab8"));       // Azul claro al pasar el mouse

    protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
    {
        Rectangle rect = new Rectangle(Point.Empty, e.Item.Size);

        if (e.Item.Selected || e.Item.Pressed)
        {
            e.Graphics.FillRectangle(hoverBrush, rect);
        }
        else
        {
            e.Graphics.FillRectangle(baseBrush, rect);
        }

        e.Item.ForeColor = Color.White;
    }
}
