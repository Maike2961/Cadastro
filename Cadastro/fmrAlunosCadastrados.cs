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
    public partial class fmrAlunosCadastrados : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Mayke Silva\source\repos\Cadastro\Cadastro\banco.mdf"";Integrated Security=True");

        SqlCommand cmd;
        SqlDataReader dr;
        public fmrAlunosCadastrados()
        {
            InitializeComponent();
        }

        private void fmrAlunosCadastrados_Load(object sender, EventArgs e)
        {
            string sql;
            try
            {
                conn.Open();
                sql = "select Id from alunos";
                cmd = new SqlCommand(sql, conn);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    lbxDados.Items.Add(dr["Id"].ToString());
                }
                cmd.Dispose();
                dr.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }

        }

        private void lbxDados_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql;
            sql = "select * from alunos where Id=" + lbxDados.SelectedItem;
            try
            {
                cmd = new SqlCommand(sql, conn);
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    txtid.Text = dr["Id"].ToString();
                    txtnome.Text = dr["nome"].ToString();
                    txtcurso.Text = dr["curso"].ToString();
                }
                cmd.Dispose();
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro " + ex.Message);
            }
        }

        private void btnfechar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
