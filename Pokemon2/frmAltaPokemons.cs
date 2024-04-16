using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;

namespace Pokemon2
{
    public partial class frmAltaPokemons : Form
    {
        private Pokemon pokemon = null;
        public frmAltaPokemons()
        {
            InitializeComponent();
        }

        public frmAltaPokemons(Pokemon pokemon)
        {
            InitializeComponent();
            this.pokemon = pokemon;
            Text = "Modificar Pokemon";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //Pokemon poke = new Pokemon();
            PokemonNegocio negocio = new PokemonNegocio();

            try
            {
                if(pokemon == null) 
                {
                    pokemon = new Pokemon();
                }
                pokemon.Numero = int.Parse(tbxNumero.Text);
                pokemon.Nombre = tbxNombre.Text;
                pokemon.Descripcion = txbDescripcion.Text;
                pokemon.UrlImagen = tbxUrlImagen.Text;
                pokemon.Tipo = (Elemento)cboTipo.SelectedItem;
                pokemon.Debilidad = (Elemento)cboDebilidad.SelectedItem;

                if(pokemon.Id != 0)
                {
                negocio.modificar(pokemon);
                MessageBox.Show("Modificado");

                }
                else
                {
                negocio.agregar(pokemon);
                MessageBox.Show("Agregado");

                }

                Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void frmAltaPokemons_Load(object sender, EventArgs e)
        {
            ElementoNegocio elementoNegocio = new ElementoNegocio();

            try
            {
                cboTipo.DataSource = elementoNegocio.listar();
                cboTipo.ValueMember = "Id";
                cboTipo.DisplayMember = "Descripcion";
                cboDebilidad.DataSource = elementoNegocio.listar();
                cboDebilidad.ValueMember = "Id";
                cboDebilidad.DisplayMember = "Descripcion";

                if(pokemon != null)
                {
                    tbxNumero.Text = pokemon.Numero.ToString();
                    tbxNombre.Text = pokemon.Nombre;
                    txbDescripcion.Text = pokemon.Descripcion;
                    tbxUrlImagen.Text = pokemon.UrlImagen;
                    cargarImagen(pokemon.UrlImagen);
                    cboTipo.SelectedValue = pokemon.Tipo.Id;
                    cboDebilidad.SelectedValue = pokemon.Debilidad.Id;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void tbxUrlImagen_Leave(object sender, EventArgs e)
        {
            cargarImagen(tbxUrlImagen.Text);
        }
        private void cargarImagen(string imagen)
        {
            try
            {
                pbUrlImagen.Load(imagen);
            }
            catch (Exception ex)
            {

                pbUrlImagen.Load("https://media.licdn.com/dms/image/D4D03AQF0oxCcvOA1bA/profile-displayphoto-shrink_800_800/0/1669689321436?e=2147483647&v=beta&t=aCFumvWK_9BSBZ7O1Q3HHss5iZdbR7fEwEtn2Y9FQgs");
            }
        }
    }
}
