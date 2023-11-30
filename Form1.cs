using SHA256Login;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace Sha256Login
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUsername.Text.Length == 0 || txtPassword.Text.Length == 0)
                {
                    MessageBox.Show("Lütfen Tüm Alanlarý Doldurun.");
                }
                else
                {
                    Methods methods = new Methods();
                    string query = "SELECT * FROM Login WHERE Username = @p1 AND Password = @p2";
                    SqlCommand command = new SqlCommand(query, ConnectionString.connection());
                    command.Parameters.AddWithValue("@p1", txtUsername.Text);
                    command.Parameters.AddWithValue("@p2", methods.CreateSHA256(txtPassword.Text));
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        MessageBox.Show("Kullanýcý Adý ve Þifre Doðru.\nGiriþ Yapýldý.");
                    }
                    else
                    {
                        MessageBox.Show("Kullanýcý Adý veya Þifre Yanlýþ. \n Lütfen Giriþ Bilgilerinizi Kontrol Ediniz.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kullanýcý Giriþi Esnasýnda Bir Sorun Oluþtu." + ex.Message);
            }
            finally
            {
                Methods methods = new Methods ();
                methods.CloseConnection();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                string query = "SELECT Username FROM Login WHERE Username = @p1";
                SqlCommand command = new SqlCommand(query, ConnectionString.connection());
                command.Parameters.AddWithValue("@p1", txtUsername.Text);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    MessageBox.Show(txtUsername.Text + " : " + "Kullanýcý Adý Baþka Bir Kullanýcý Tarafýndan Alýnmýþtýr.\nLütfen Baþka Bir Kullanýcý Adý Deneyiniz.");
                }
                else
                {
                    Methods methods = new Methods();
                    string query1 = "INSERT INTO Login (Username, Password, Deleted) VALUES (@p1, @p2, 0)";
                    SqlCommand command1 = new SqlCommand(query1, ConnectionString.connection());
                    command1.Parameters.AddWithValue("@p1", txtUsername.Text);
                    command1.Parameters.AddWithValue("@p2", methods.CreateSHA256(txtPassword.Text));// Þifreyi SHA256 ile kodlayýp o þekilde DB'ye yazacak.
                    command1.ExecuteNonQuery();
                    MessageBox.Show(txtUsername.Text + " : " + "Kullanýcý Kayýt Ýþlemi Baþarýlý");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kullanýcý Kayýt Ýþlemi Sýrasýnda Bir Hata Oluþtu." + ex.Message);
            }
            finally
            {
                Methods methods = new Methods();
                methods.CloseConnection();
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                string query = "SELECT Username FROM Login WHERE Username = @p1";
                SqlCommand command = new SqlCommand(query, ConnectionString.connection());
                command.Parameters.AddWithValue("@p1", txtUsername.Text);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    Methods methods = new Methods();
                    string query1 = "UPDATE Login SET Password = @p2 WHERE Username = @p1";
                    SqlCommand command1 = new SqlCommand(query1, ConnectionString.connection());
                    command1.Parameters.AddWithValue("@p1", txtUsername.Text);
                    command1.Parameters.AddWithValue("@p2", methods.CreateSHA256(txtPassword.Text));
                    command1.ExecuteNonQuery();
                    MessageBox.Show(txtUsername.Text + " : " + "Þifre Deðiþtirildi.");
                }
                else
                {
                    MessageBox.Show(txtUsername.Text + " : " + "Adýnda Bir Kullanýcý Bulunamadý. \n Lütfen Kullanýcý Adýnýzý Kontrol Ediniz yada Kayýt Olunuz.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Þifre Deðiþtirme Ýþlemi Sýrasýnda Bir Hata Oluþtu." + ex.Message);
            }
            finally
            {
                Methods methods = new Methods();
                methods.CloseConnection();
            }
        }
    }
}
