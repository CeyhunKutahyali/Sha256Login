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
                    MessageBox.Show("L�tfen T�m Alanlar� Doldurun.");
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
                        MessageBox.Show("Kullan�c� Ad� ve �ifre Do�ru.\nGiri� Yap�ld�.");
                    }
                    else
                    {
                        MessageBox.Show("Kullan�c� Ad� veya �ifre Yanl��. \n L�tfen Giri� Bilgilerinizi Kontrol Ediniz.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kullan�c� Giri�i Esnas�nda Bir Sorun Olu�tu." + ex.Message);
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
                    MessageBox.Show(txtUsername.Text + " : " + "Kullan�c� Ad� Ba�ka Bir Kullan�c� Taraf�ndan Al�nm��t�r.\nL�tfen Ba�ka Bir Kullan�c� Ad� Deneyiniz.");
                }
                else
                {
                    Methods methods = new Methods();
                    string query1 = "INSERT INTO Login (Username, Password, Deleted) VALUES (@p1, @p2, 0)";
                    SqlCommand command1 = new SqlCommand(query1, ConnectionString.connection());
                    command1.Parameters.AddWithValue("@p1", txtUsername.Text);
                    command1.Parameters.AddWithValue("@p2", methods.CreateSHA256(txtPassword.Text));// �ifreyi SHA256 ile kodlay�p o �ekilde DB'ye yazacak.
                    command1.ExecuteNonQuery();
                    MessageBox.Show(txtUsername.Text + " : " + "Kullan�c� Kay�t ��lemi Ba�ar�l�");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kullan�c� Kay�t ��lemi S�ras�nda Bir Hata Olu�tu." + ex.Message);
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
                    MessageBox.Show(txtUsername.Text + " : " + "�ifre De�i�tirildi.");
                }
                else
                {
                    MessageBox.Show(txtUsername.Text + " : " + "Ad�nda Bir Kullan�c� Bulunamad�. \n L�tfen Kullan�c� Ad�n�z� Kontrol Ediniz yada Kay�t Olunuz.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("�ifre De�i�tirme ��lemi S�ras�nda Bir Hata Olu�tu." + ex.Message);
            }
            finally
            {
                Methods methods = new Methods();
                methods.CloseConnection();
            }
        }
    }
}
