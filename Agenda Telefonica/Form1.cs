using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Mysqlx;

namespace Agenda_Telefonica
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lstPesquisa_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection
                ("server=127.0.0.1; userid=root; password=root; database=agenda");
                conn.Open();
                MySqlCommand comandooo = new MySqlCommand("INSERT INTO `contatos` (`nome`, `telefone`) VALUES (@nome, @telefone);", conn);
                comandooo.Parameters.AddWithValue("@nome", txbNome.Text);
                comandooo.Parameters.AddWithValue("@telefone", txbTelefone.Text);
                comandooo.ExecuteNonQuery();
                conn.Close();

                txbNome.Text = "";
                txbTelefone.Text = "";
            }
            catch (Exception erro)
            {
                MessageBox.Show("ALgo de errado não está certo.\n" + erro.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();

            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection
                    ("server=127.0.0.1; userid=root; password=root; database=agenda");
                conn.Open();

                if (txbPesquisa.Text == "")
                {

                    MySqlCommand comandooo = new MySqlCommand("SELECT `nome`, `telefone` FROM `contatos`;", conn);
                    var reader = comandooo.ExecuteReader();

                    while (reader.Read())
                    {

                        rtbPesquisa.Text = ("nome: " + reader.GetString(1) + "telefone: " + reader.GetString(2));

                    }

                }
                else
                {
                    MySqlCommand comandooo = new MySqlCommand("SELECT * FROM `contatos` WHERE `nome` LIKE '%"+ txbPesquisa.Text+"%';", conn);
                    //*comandooo.Parameters.AddWithValue("@nome", txbNome.Text);

                    var reader = comandooo.ExecuteReader();

                    while (reader.Read())
                    {

                        rtbPesquisa.Text = rtbPesquisa.Text + "nome: " + reader.GetString(1) + "telefone: " + reader.GetString(2) + "\n";
                    }
                }
                if (conn.State.ToString() != "Close")
                {
                    conn.Close();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("ALgo de errado não está certo.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }finally
            {
                txbNome.Text = "";
                txbTelefone.Text = "";

            }
        }
        }
    }


