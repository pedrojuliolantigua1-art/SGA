using System.Drawing;

namespace SGA.Desktop.UI
{
    /// <summary>
    /// Paleta y estilos comunes de la aplicación de escritorio (SOLID/DRY: un único lugar
    /// para colores y tipografías; todos los formularios y controles lo reutilizan en vez
    /// de repetir valores mágicos).
    /// </summary>
    public static class AppTheme
    {
        // === Azules (color principal institucional) ===
        public static readonly Color AzulOscuro = Color.FromArgb(13, 41, 82);     // sidebar / headers
        public static readonly Color Azul = Color.FromArgb(21, 101, 192);         // botones primarios
        public static readonly Color AzulClaro = Color.FromArgb(227, 242, 253);   // fondos suaves
        public static readonly Color AzulHover = Color.FromArgb(30, 136, 229);    // hover

        // === Rojos (detalles / acciones destructivas o de alerta) ===
        public static readonly Color Rojo = Color.FromArgb(198, 40, 40);
        public static readonly Color RojoHover = Color.FromArgb(229, 57, 53);
        public static readonly Color RojoClaro = Color.FromArgb(253, 236, 234);

        // === Neutros ===
        public static readonly Color Blanco = Color.White;
        public static readonly Color GrisTexto = Color.FromArgb(66, 66, 66);
        public static readonly Color GrisClaro = Color.FromArgb(245, 247, 250);
        public static readonly Color Borde = Color.FromArgb(224, 224, 224);
        public static readonly Color Exito = Color.FromArgb(46, 125, 50);

        public static readonly Font FuenteTitulo = new("Segoe UI", 13F, FontStyle.Bold);
        public static readonly Font FuenteSubtitulo = new("Segoe UI", 10.5F, FontStyle.Bold);
        public static readonly Font FuenteBase = new("Segoe UI", 9.5F);
        public static readonly Font FuenteBaseNegrita = new("Segoe UI", 9.5F, FontStyle.Bold);

        /// <summary>Aplica el estilo de botón primario (azul).</summary>
        public static void ComoBotonPrimario(Button boton)
        {
            boton.FlatStyle = FlatStyle.Flat;
            boton.FlatAppearance.BorderSize = 0;
            boton.BackColor = Azul;
            boton.ForeColor = Blanco;
            boton.Font = FuenteBaseNegrita;
            boton.Cursor = Cursors.Hand;
            boton.Height = Math.Max(boton.Height, 36);
            boton.FlatAppearance.MouseOverBackColor = AzulHover;
        }

        /// <summary>Aplica el estilo de botón de peligro (rojo) — eliminar, cerrar sesión, cancelar, rechazar.</summary>
        public static void ComoBotonPeligro(Button boton)
        {
            boton.FlatStyle = FlatStyle.Flat;
            boton.FlatAppearance.BorderSize = 0;
            boton.BackColor = Rojo;
            boton.ForeColor = Blanco;
            boton.Font = FuenteBaseNegrita;
            boton.Cursor = Cursors.Hand;
            boton.Height = Math.Max(boton.Height, 36);
            boton.FlatAppearance.MouseOverBackColor = RojoHover;
        }

        /// <summary>Aplica el estilo de botón secundario (contorno, para "Cancelar"/"Refrescar").</summary>
        public static void ComoBotonSecundario(Button boton)
        {
            boton.FlatStyle = FlatStyle.Flat;
            boton.FlatAppearance.BorderSize = 1;
            boton.FlatAppearance.BorderColor = Azul;
            boton.BackColor = Blanco;
            boton.ForeColor = Azul;
            boton.Font = FuenteBaseNegrita;
            boton.Cursor = Cursors.Hand;
            boton.Height = Math.Max(boton.Height, 34);
            boton.FlatAppearance.MouseOverBackColor = AzulClaro;
        }

        /// <summary>Da estilo consistente a un DataGridView (encabezados azules, filas alternadas).</summary>
        public static void ComoGrillaEstandar(DataGridView grid)
        {
            grid.BackgroundColor = Blanco;
            grid.BorderStyle = BorderStyle.None;
            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            grid.EnableHeadersVisualStyles = false;
            grid.RowHeadersVisible = false;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.ReadOnly = true;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.Font = FuenteBase;
            grid.RowTemplate.Height = 30;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            grid.ColumnHeadersDefaultCellStyle.BackColor = AzulOscuro;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Blanco;
            grid.ColumnHeadersDefaultCellStyle.Font = FuenteBaseNegrita;
            grid.ColumnHeadersDefaultCellStyle.Padding = new Padding(6);
            grid.ColumnHeadersHeight = 36;

            grid.DefaultCellStyle.SelectionBackColor = AzulClaro;
            grid.DefaultCellStyle.SelectionForeColor = GrisTexto;
            grid.DefaultCellStyle.Padding = new Padding(4);
            grid.AlternatingRowsDefaultCellStyle.BackColor = GrisClaro;
        }

        /// <summary>Encabezado estándar (panel azul oscuro + título en blanco) para un formulario/módulo.</summary>
        public static Panel CrearEncabezado(string titulo, out Label lblTitulo)
        {
            var panel = new Panel { Dock = DockStyle.Top, Height = 55, BackColor = AzulOscuro };
            lblTitulo = new Label
            {
                Text = titulo,
                ForeColor = Blanco,
                Font = FuenteTitulo,
                AutoSize = true,
                Location = new Point(20, 14)
            };
            panel.Controls.Add(lblTitulo);
            return panel;
        }

        /// <summary>Etiqueta de sección del menú lateral (ej. "CATÁLOGO").</summary>
        public static Label EtiquetaSeccionLateral(string texto)
        {
            return new Label
            {
                Text = texto,
                ForeColor = Color.FromArgb(117, 144, 188),
                Font = new Font("Segoe UI", 7.5F, FontStyle.Bold),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Height = 22
            };
        }

        /// <summary>Aplica el estilo de botón del menú lateral (inactivo u opción activa resaltada).</summary>
        public static void ComoBotonMenuLateral(Button boton, bool activo = false)
        {
            boton.FlatStyle = FlatStyle.Flat;
            boton.FlatAppearance.BorderSize = 0;
            boton.TextAlign = ContentAlignment.MiddleLeft;
            boton.Padding = new Padding(20, 0, 0, 0);
            boton.Height = 34;
            boton.Font = FuenteBase;
            boton.Cursor = Cursors.Hand;
            boton.BackColor = activo ? Azul : AzulOscuro;
            boton.ForeColor = activo ? Blanco : Color.FromArgb(220, 230, 245);
            boton.FlatAppearance.MouseOverBackColor = activo ? Azul : Color.FromArgb(20, 55, 105);
        }

        /// <summary>Botón tipo "toggle" horizontal (selector de tipo de usuario, filtros, etc.).</summary>
        public static void ComoBotonToggle(Button boton, bool activo)
        {
            boton.FlatStyle = FlatStyle.Flat;
            boton.FlatAppearance.BorderSize = 1;
            boton.FlatAppearance.BorderColor = Azul;
            boton.Font = FuenteBaseNegrita;
            boton.Cursor = Cursors.Hand;
            boton.BackColor = activo ? Azul : Blanco;
            boton.ForeColor = activo ? Blanco : Azul;
            boton.FlatAppearance.MouseOverBackColor = activo ? AzulHover : AzulClaro;
        }
    }
}
