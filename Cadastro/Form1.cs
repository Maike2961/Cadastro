using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Cadastro
{
    public partial class Form1 : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Mayke Silva\source\repos\Cadastro\Cadastro\banco.mdf"";Integrated Security=True");

        SqlCommand cmd;
        SqlDataReader dr;
        public Form1()
        {
            InitializeComponent();
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro na conexao" + ex.Message);
            }
        }

        private void btninserir_Click(object sender, EventArgs e)
        {
            string sql;
            int retorno;
            try
            {
                sql = "insert into alunos (id, nome, curso) (" + txtid.Text +
                    ", '" + txtnome.Text + "', '" + txtcurso.Text + "')";
                cmd = new SqlCommand(sql, conn);
                retorno = cmd.ExecuteNonQuery();
                if (retorno == 0)
                {
                    MessageBox.Show("Cadastro efetuado");
                }
                else
                {
                    MessageBox.Show("Cadastro não realizado");
                }
                txtcurso.Text = "";
                txtid.Text = "";
                txtnome.Text = "";
                cmd.Dispose();
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Erro no comando sql" +ex.Message);
            }
        }

        private void btnconsultar_Click(object sender, EventArgs e)
        {
            string sql;
            try
            {
                sql = "select * from alunos where id = " + txtid.Text;
                cmd = new SqlCommand(sql, conn);
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    txtnome.Text = dr["nome"].ToString();
                    txtcurso.Text = dr["curso"].ToString();
                }
                else
                {
                    MessageBox.Show("registro não encontrado");
                }
                dr.Close();
                cmd.Dispose();
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Erro no comando sql" + ex.Message);
            }
        }

        private void btnexcluir_Click(object sender, EventArgs e)
        {
            string sql;
            int retorno;
            try
            {
                sql = "delete alunos where id = " + txtid.Text;
                cmd = new SqlCommand(sql, conn);
                retorno = cmd.ExecuteNonQuery();
                if(retorno > 0)
                {
                    MessageBox.Show("Registro Excluido");
                }
                else
                {
                    MessageBox.Show("Registro não excluido");
                }
                txtcurso.Text = "";
                txtid.Text = "";
                txtnome.Text = "";
                cmd.Dispose();
            }
            catch(SqlException ex)
            {
                MessageBox.Show("ERRO: " + ex.Message);
            }
        }

        private void btnexibir_Click(object sender, EventArgs e)
        {
            fmrAlunosCadastrados f2 = new fmrAlunosCadastrados();
            f2.Show();
        }
    }
}
