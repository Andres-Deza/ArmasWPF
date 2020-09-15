using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RegistroArmas
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            grdPanel.Visibility = Visibility.Collapsed;

        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            grdPanel.Visibility = Visibility.Visible;
            Limpiar();
            btnGuardar.IsEnabled = true;
            btnModificar1.IsEnabled = false;
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            grdPanel.Visibility = Visibility.Visible;
            btnGuardar.IsEnabled = false;
            btnModificar1.IsEnabled = true;
            ArmasDBEntities entidades = new ArmasDBEntities();
            ARMA a = entidades.ARMA.ToList<ARMA>().Where(c => c.ID == int.Parse(txtIDModificar.Text)).FirstOrDefault<ARMA>();
            if (a == null)
            {
                MessageBox.Show("No se encontro el arma");
            }
            else
            {
                lblID.Content = a.ID;
                txtNombre.Text = a.NOMBRE;
                txtCliente.Text = a.CLIENTE;
                txtCategoria.Text = a.CATEGORIA;
                txtAnio.Text = a.ANIO_CREACION.ToString();
                dpFechaVenta.SelectedDate = Convert.ToDateTime(a.FECHA_VENTA);
                txtPrecio.Text = a.PRECIO.ToString();

            }

        }

        private bool CamposVacios()
        {
            if (txtAnio.Text == null || txtCategoria.Text == null || txtCliente.Text == null || txtNombre.Text == null || txtPrecio.Text == null || dpFechaVenta.SelectedDate == null)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        private bool TipoletraCliente()
        {
            string p = string.Empty;

            foreach (char a in txtCliente.Text)
            {
                if (Char.IsLetter(a))
                {
                    p += a;
                }
            }
            //MessageBox.Show(p + " - " + txtNombre.Text);
            //MessageBox.Show(p.Length + " - " + txtNombre.Text.Length);
            if (p.Length == txtNombre.Text.Length)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool TipoletraCategoria()
        {
            string p = string.Empty;

            foreach (char a in txtCategoria.Text)
            {
                if (Char.IsLetter(a))
                {
                    p += a;
                }
            }
            MessageBox.Show(p + " - " + txtNombre.Text);
            MessageBox.Show(p.Length + " - " + txtNombre.Text.Length);
            if (p.Length == txtNombre.Text.Length)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool TipoNumeroPrecio()
        {
            int p = 0;

            foreach (char a in txtCategoria.Text)
            {
                if (Char.IsNumber(a))
                {
                    p += a;
                }
            }
            MessageBox.Show(p + " - " + txtNombre.Text);
            MessageBox.Show(p + " - " + txtNombre.Text.Length);
            if (p == int.Parse(txtNombre.Text))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private void Limpiar()
        {
            lblID.Content = "";
            txtAnio.Text = "";
            txtCategoria.Text = "";
            txtCliente.Text = "";
            txtID.Text = "";
            txtNombre.Text = "";
            txtPrecio.Text = "";
            dpFechaVenta.SelectedDate = null;
            lblTipoAnio.Content = "";
            lblTipoCategoria.Content = "";
            lblTipoCliente.Content = "";
            lblTipoPrecio.Content = "";

        }

        private void MostrarDatos()
        {
            ArmasDBEntities entidades = new ArmasDBEntities();
            List<ARMA> arma = entidades.ARMA.ToList<ARMA>();

            dtgrdArmas.ItemsSource = arma;
            dtgrdArmas.Items.Refresh();
        }



        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            ArmasDBEntities entidades = new ArmasDBEntities();
            ARMA a = entidades.ARMA.ToList<ARMA>().Where(c => c.ID == int.Parse(txtIDEliminar.Text)).FirstOrDefault<ARMA>();
            if (a == null)
            {
                MessageBox.Show("No se encontro el arma");
            }
            else
            {
                entidades.ARMA.Remove(a);
                entidades.SaveChanges();
                MessageBox.Show("Pedido Eliminado");
                MostrarDatos();
            }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            ArmasDBEntities entidades = new ArmasDBEntities();
            ARMA a = entidades.ARMA.ToList<ARMA>().Where(c => c.ID == int.Parse(txtID.Text)).FirstOrDefault<ARMA>();

            MessageBox.Show("Nombre de arma: " + a.NOMBRE + " \nPrecio: " + a.PRECIO + "\nCategoria: " + a.CATEGORIA + "\nCliente: " + a.CLIENTE + "\nAño de creación:" + a.ANIO_CREACION + "\nFecha: " + a.FECHA_VENTA);
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            int t = 0;
            if (CamposVacios() == true)
            {
                MessageBox.Show("Ingrese Todos los datos");

            }

            else
            {
                if (int.TryParse(txtCategoria.Text, out t) == true)
                {
                    lblTipoCategoria.Content = "Campo Categoría es de tipo Letra";

                }
                if (int.TryParse(txtCliente.Text, out t) == true)
                {
                    lblTipoCliente.Content = "Campo Cliente es de tipo Letra";
                }
                if (int.TryParse(txtAnio.Text, out t) == false)
                {
                    lblTipoAnio.Content = "Campo Año es de tipo númerico";

                }
                if (int.TryParse(txtPrecio.Text, out t) == false)
                {
                    lblTipoPrecio.Content = "Campo Precio es de tipo númerico";
                }

                else
                {
                    ArmasDBEntities entidades = new ArmasDBEntities();
                    ARMA a = new ARMA();
                    a.NOMBRE = txtNombre.Text;
                    a.CLIENTE = txtCliente.Text;
                    a.CATEGORIA = txtCategoria.Text;
                    a.ANIO_CREACION = int.Parse(txtAnio.Text);
                    a.FECHA_VENTA = dpFechaVenta.SelectedDate.ToString();
                    a.PRECIO = int.Parse(txtPrecio.Text);
                    entidades.ARMA.Add(a);
                    try
                    {
                        entidades.SaveChanges();

                    }
                    catch (DbEntityValidationException el)
                    {
                        foreach (var eve in el.EntityValidationErrors)
                        {
                            Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                eve.Entry.Entity.GetType().Name, eve.Entry.State);
                            foreach (var ve in eve.ValidationErrors)
                            {
                                Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                    ve.PropertyName, ve.ErrorMessage);
                            }
                        }
                        throw;
                    }
                    MessageBox.Show("Agregado exitosamente");
                    MostrarDatos();
                    Limpiar();
                }
            }
        }

        private void dtgrdArmas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ArmasDBEntities entidades = new ArmasDBEntities();
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                txtNombre.Text = row_selected["NOMBRE"].ToString();
                txtNombre.Text = row_selected["CATEGORIA"].ToString();
            }
        }



        private void btnModificar1_Click_1(object sender, RoutedEventArgs e)
        {
            if (CamposVacios() == true)
            {
                MessageBox.Show("Ingrese Todos los datos");
            }
            else
            {
                ArmasDBEntities entidades = new ArmasDBEntities();
                ARMA a = entidades.ARMA.ToList<ARMA>().Where(c => c.ID == int.Parse(txtIDModificar.Text)).FirstOrDefault<ARMA>();
                if (a == null)
                {
                    MessageBox.Show("No se encontro el arma");
                }
                else
                {
                    a.NOMBRE = txtNombre.Text;
                    a.CLIENTE = txtCliente.Text;
                    a.CATEGORIA = txtCategoria.Text;
                    a.ANIO_CREACION = int.Parse(txtAnio.Text);
                    a.FECHA_VENTA = dpFechaVenta.SelectedDate.ToString();
                    a.PRECIO = int.Parse(txtPrecio.Text);
                    entidades.SaveChanges();
                    MessageBox.Show("Pedido modificado");
                    MostrarDatos();
                }
            }

        }

        private void txtAnio_Error(object sender, ValidationErrorEventArgs e)
        {

        }


    }
}
