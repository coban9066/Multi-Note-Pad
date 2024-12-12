using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MultiNot
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string noteTitle = textBox1.Text.Trim();
            string noteContent = richTextBox1.Text.Trim();

            if (string.IsNullOrWhiteSpace(noteTitle) || string.IsNullOrWhiteSpace(noteContent))
            {
                MessageBox.Show("Başlık ve içerik alanları boş bırakılamaz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = @"Data Source=DESKTOP-N5LCA8G;Initial Catalog=MultiNote;Integrated Security=True;Encrypt=False";
            string insertQuery = "INSERT INTO Notlar (notbaslik, noticerik) VALUES (@notbaslik, @noticerik)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@notbaslik", noteTitle);
                        command.Parameters.AddWithValue("@noticerik", noteContent);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Not başarıyla kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close(); 
                        }
                        else
                        {
                            MessageBox.Show("Not kaydedilirken bir sorun oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
