using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MultiNot
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadNotes(); 

            listBox1.SelectedIndexChanged += ListBox1_SelectedIndexChanged;
        }

        private void LoadNotes()
        {
            string connectionString = @"Data Source=DESKTOP-N5LCA8G;Initial Catalog=MultiNote;Integrated Security=True;Encrypt=False";
            string query = "SELECT notbaslik FROM Notlar";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            listBox1.Items.Clear(); 

                            bool hasNotes = false; 

                            while (reader.Read())
                            {
                                hasNotes = true;
                                listBox1.Items.Add(reader["notbaslik"].ToString());
                            }

                            if (!hasNotes)
                            {
                                listBox1.Items.Add("Henüz bir not oluşturmadınız.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message);
                }
            }
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null) return;

            string selectedNote = listBox1.SelectedItem.ToString();
            string connectionString = @"Data Source=DESKTOP-N5LCA8G;Initial Catalog=MultiNote;Integrated Security=True;Encrypt=False";
            string query = "SELECT noticerik FROM Notlar WHERE notbaslik = @notbaslik";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@notbaslik", selectedNote);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                richTextBox1.Text = reader["noticerik"].ToString();
                            }
                            else
                            {
                                richTextBox1.Text = "İlgili içerik bulunamadı.";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
            {
                MessageBox.Show("Lütfen silmek istediğiniz bir not seçin.");
                return;
            }

            string selectedNote = listBox1.SelectedItem.ToString();
            string connectionString = @"Data Source=DESKTOP-N5LCA8G;Initial Catalog=MultiNote;Integrated Security=True;Encrypt=False";
            string deleteQuery = "DELETE FROM Notlar WHERE notbaslik = @notbaslik";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@notbaslik", selectedNote);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Not başarıyla silindi.");
                            richTextBox1.Clear(); 
                        }
                        else
                        {
                            MessageBox.Show("Not silinirken bir sorun oluştu.");
                        }
                    }

                    LoadNotes();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
            {
                MessageBox.Show("Lütfen bir not seçin.");
                return;
            }

            string selectedNote = listBox1.SelectedItem.ToString();
            string updatedContent = richTextBox1.Text;

            string connectionString = @"Data Source=DESKTOP-N5LCA8G;Initial Catalog=MultiNote;Integrated Security=True;Encrypt=False";
            string updateQuery = "UPDATE Notlar SET noticerik = @noticerik WHERE notbaslik = @notbaslik";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@noticerik", updatedContent);
                        command.Parameters.AddWithValue("@notbaslik", selectedNote);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Not başarıyla güncellendi.");
                        }
                        else
                        {
                            MessageBox.Show("Not güncellenirken bir sorun oluştu.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();

            form2.Show();

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
