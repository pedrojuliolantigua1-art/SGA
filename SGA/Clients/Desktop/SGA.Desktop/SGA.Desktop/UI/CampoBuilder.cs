using System.Drawing;

namespace SGA.Desktop.UI
{

    public static class CampoBuilder
    {
        public const int AnchoEstandar = 280;

        public static Label Etiqueta(Panel contenedor, string texto, int x, int y)
        {
            var lbl = new Label
            {
                Text = texto,
                AutoSize = true,
                Location = new Point(x, y),
                Font = AppTheme.FuenteBaseNegrita,
                ForeColor = AppTheme.GrisTexto
            };
            contenedor.Controls.Add(lbl);
            return lbl;
        }

        public static TextBox CajaTexto(Panel contenedor, int x, int y, int ancho = AnchoEstandar)
        {
            var txt = new TextBox
            {
                Location = new Point(x, y),
                Size = new Size(ancho, 27),
                Font = AppTheme.FuenteBase
            };
            contenedor.Controls.Add(txt);
            return txt;
        }

        public static ComboBox Combo(Panel contenedor, int x, int y, int ancho = AnchoEstandar)
        {
            var cmb = new ComboBox
            {
                Location = new Point(x, y),
                Size = new Size(ancho, 27),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = AppTheme.FuenteBase
            };
            contenedor.Controls.Add(cmb);
            return cmb;
        }

        public static DateTimePicker Fecha(Panel contenedor, int x, int y, int ancho = AnchoEstandar)
        {
            var dtp = new DateTimePicker
            {
                Location = new Point(x, y),
                Size = new Size(ancho, 27),
                Format = DateTimePickerFormat.Short,
                Font = AppTheme.FuenteBase
            };
            contenedor.Controls.Add(dtp);
            return dtp;
        }

        public static NumericUpDown Numero(Panel contenedor, int x, int y, decimal min, decimal max, int ancho = AnchoEstandar)
        {
            var num = new NumericUpDown
            {
                Location = new Point(x, y),
                Size = new Size(ancho, 27),
                Minimum = min,
                Maximum = max,
                Font = AppTheme.FuenteBase
            };
            contenedor.Controls.Add(num);
            return num;
        }

        public static Label Titulo(Panel contenedor, string texto, int x, int y)
        {
            var lbl = new Label
            {
                Text = texto,
                AutoSize = true,
                Location = new Point(x, y),
                Font = AppTheme.FuenteSubtitulo,
                ForeColor = AppTheme.AzulOscuro
            };
            contenedor.Controls.Add(lbl);
            return lbl;
        }

        public static Label Mensaje(Panel contenedor, int x, int y, int ancho = AnchoEstandar)
        {
            var lbl = new Label
            {
                Location = new Point(x, y),
                Size = new Size(ancho, 50),
                Font = AppTheme.FuenteBaseNegrita,
                Text = string.Empty
            };
            contenedor.Controls.Add(lbl);
            return lbl;
        }

        public static void MostrarMensaje(Label lbl, string texto, bool esError)
        {
            lbl.ForeColor = esError ? AppTheme.Rojo : AppTheme.Exito;
            lbl.Text = texto;
        }

        public static Button Boton(Panel contenedor, string texto, int x, int y, int ancho = AnchoEstandar)
        {
            var btn = new Button
            {
                Text = texto,
                Location = new Point(x, y),
                Size = new Size(ancho, 36)
            };
            contenedor.Controls.Add(btn);
            return btn;
        }

        /// <summary>Crea, dentro de una celda de TableLayoutPanel, un mini-panel con etiqueta arriba
        /// y campo de texto abajo (Dock=Top), para que el campo se estire con el ancho de la columna.
        /// IMPORTANTE: AutoSize=false en el label es obligatorio, si no WinForms ignora el Height/Padding
        /// que le asignamos y el texto queda pegado al control de abajo.</summary>
        public static TextBox CampoEnTabla(TableLayoutPanel tabla, int fila, int columna, string etiqueta, int colSpan = 1)
        {
            var contenedor = new Panel { Dock = DockStyle.Fill, Margin = new Padding(6, 8, 6, 14) };
            var lbl = new Label
            {
                Text = etiqueta,
                Dock = DockStyle.Top,
                AutoSize = false,
                Height = 26,
                Font = AppTheme.FuenteBase,
                ForeColor = AppTheme.GrisTexto,
                TextAlign = ContentAlignment.BottomLeft
            };
            var txt = new TextBox { Dock = DockStyle.Top, Font = AppTheme.FuenteBase, Margin = new Padding(0, 4, 0, 0) };
            contenedor.Controls.Add(txt);
            contenedor.Controls.Add(lbl);
            tabla.Controls.Add(contenedor, columna, fila);
            if (colSpan > 1) tabla.SetColumnSpan(contenedor, colSpan);
            return txt;
        }

        /// <summary>Igual que <see cref="CampoEnTabla"/> pero para un DateTimePicker.</summary>
        public static DateTimePicker CampoFechaEnTabla(TableLayoutPanel tabla, int fila, int columna, string etiqueta, int colSpan = 1)
        {
            var contenedor = new Panel { Dock = DockStyle.Fill, Margin = new Padding(6, 8, 6, 14) };
            var lbl = new Label
            {
                Text = etiqueta,
                Dock = DockStyle.Top,
                AutoSize = false,
                Height = 26,
                Font = AppTheme.FuenteBase,
                ForeColor = AppTheme.GrisTexto,
                TextAlign = ContentAlignment.BottomLeft
            };
            var dtp = new DateTimePicker { Dock = DockStyle.Top, Font = AppTheme.FuenteBase };
            contenedor.Controls.Add(dtp);
            contenedor.Controls.Add(lbl);
            tabla.Controls.Add(contenedor, columna, fila);
            if (colSpan > 1) tabla.SetColumnSpan(contenedor, colSpan);
            return dtp;
        }
    }
}