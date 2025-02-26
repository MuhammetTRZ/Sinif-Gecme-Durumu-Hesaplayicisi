using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sınıfGecmeKontrol
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static string databaseLink = "Data Source=Muhammet;Initial Catalog = topluDersGecmeİslemleri; Integrated Security = True;";
        SqlConnection connect= new SqlConnection(databaseLink);

        void guncelle()
        {
            using (SqlConnection connect = new SqlConnection(databaseLink))
            {
                connect.Open();
                string tabloÇağır = "select * from notlar";
                using (SqlCommand komut = new SqlCommand(tabloÇağır, connect))
                {
                    SqlDataAdapter da = new SqlDataAdapter(tabloÇağır, connect);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    dataGridView2.DataSource = dt;

                }
            }
        }
        void sadeceHarfGirisi(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }
        void sadeceSayıGirisi(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                //metinkutuları boyut sınırlandırılması
                textBox3.MaxLength = 9;//ogr numarası max 9 olur
                textBox5.MaxLength = 3;
                textBox6.MaxLength = 3;
                textBox7.MaxLength = 3;
                textBox9.MaxLength = 11;

                //ilgili metinkutularının sadece harf almasını sağlar
                TextBox[] metinKutuları = {textBox1,textBox2,textBox4};
                foreach (TextBox kutu in metinKutuları)
                {
                    kutu.KeyPress += sadeceHarfGirisi;
                }
                //ilgili metinkutularının sadece sayı alabilmesini sağlar
                TextBox[] sayıKutuları = { textBox3, textBox5, textBox6,textBox7,textBox9 };
                foreach (TextBox kutu2 in sayıKutuları)
                {
                    kutu2.KeyPress += sadeceSayıGirisi;
                }



                using (SqlConnection connect = new SqlConnection(databaseLink))
                {
                    connect.Open();
                    string tabloÇağır = "select * from notlar";
                    using (SqlCommand komut = new SqlCommand(tabloÇağır, connect))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(tabloÇağır, connect);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;
                        dataGridView2.DataSource = dt;
                       
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("bir hata meydana geldi" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
           
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

         
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                bool bosVarmı = false;
                TextBox[] metinKutuları = { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7, textBox8, textBox9 };
                if (metinKutuları.Any(metinler =>string.IsNullOrWhiteSpace(metinler.Text)) || dateTimePicker1.Value.Date>=DateTime.Now.Date)
                {
                    MessageBox.Show("Boş kutu bırakmayınız");
                    return;
                }
                MessageBox.Show("basıldı");
                using (SqlConnection connect = new SqlConnection(databaseLink))
                {
                    connect.Open();
                    MessageBox.Show("bağlantı açıldı");
                    string öğrenciEklemeKodu = "insert into notlar (isim,soyisim,öğrenciNumara,bölüm,vize,final,büt,eposta,telefon,dogumTarihi) values (@isim,@soyisim,@ogrNo,@bölüm,@vize,@final,@büt,@eposta,@telefon,@dt)";
                    using (SqlCommand komut = new SqlCommand(öğrenciEklemeKodu, connect))
                    {
                        komut.Parameters.AddWithValue("@isim", textBox1.Text);
                        komut.Parameters.AddWithValue("@soyisim", textBox2.Text);
                        komut.Parameters.AddWithValue("@ogrNo", textBox3.Text);
                        komut.Parameters.AddWithValue("@bölüm", textBox4.Text);
                        komut.Parameters.AddWithValue("@vize", textBox5.Text);
                        komut.Parameters.AddWithValue("@final", textBox6.Text);
                        komut.Parameters.AddWithValue("@büt", textBox7.Text);
                        komut.Parameters.AddWithValue("@eposta", textBox8.Text);
                        komut.Parameters.AddWithValue("@telefon", textBox9.Text);
                        komut.Parameters.AddWithValue("@dt", dateTimePicker1.Value.Date);
                        int etkilenenSatırSayısı=komut.ExecuteNonQuery();
                        if(etkilenenSatırSayısı > 0)
                        {
                            MessageBox.Show("Öğrenci eklendi");
                        }
                        else
                        {
                            MessageBox.Show("öğrenci bulunamadı");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("bir hata meydana geldi" + ex.Message);
            }
            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string pattern = @"^(\d{9})";
            if (!Regex.IsMatch(textBox3.Text,pattern))
            {
                pictureBox1.Visible = true;
                pictureBox2.Visible = false;
            }
            else
            {
                pictureBox1.Visible = false;
                pictureBox2.Visible = true;
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBox5.Text, out int sayı))
            {
                if (sayı >=101)
                {
                    pictureBox12.Visible = true;
                    pictureBox13.Visible = false;
                }
                else
                {
                    pictureBox12.Visible = false;
                    pictureBox13.Visible = true;
                }
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBox6.Text, out int sayı))
            {
                if (sayı >= 101)
                {
                    pictureBox6.Visible = true;
                    pictureBox8.Visible = false;
                }
                else
                {
                    pictureBox6.Visible = false;
                    pictureBox8.Visible = true;
                }
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBox7.Text, out int sayı))
            {
                if (sayı >= 101)
                {
                    pictureBox3.Visible = true;
                    pictureBox4.Visible = false;
                }
                else
                {
                    pictureBox3.Visible = false;
                    pictureBox4.Visible = true;
                }
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            string pattern = @"^.*@[^@]+\.com$";
            if (!Regex.IsMatch(textBox8.Text, pattern))
            {//kutuya her bir değer girildiğinde,metin regex yapısına(pattern)'a uyuşuyor mu diye bajar
                pictureBox7.Visible = true;
                pictureBox5.Visible = false;
            }
            else
            {
                pictureBox7.Visible = false;
                pictureBox5.Visible = true;
            }
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            string pattern = @"^(\d{11})";
            if (Regex.IsMatch(textBox9.Text, pattern) && textBox9.Text.StartsWith("0"))
            {//kutuya her bir değer girildiğinde,metin regex yapısına(pattern)'a uyuşuyor mu ve 0 ile mi başlıyor ona bakar
                pictureBox14.Visible = false;
                pictureBox15.Visible = true;
            }
            else
            {
                pictureBox14.Visible = true;
                pictureBox15.Visible = false;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(databaseLink))
                {
                    connect.Open();
                    string hesaplamaSqlkodu = "UPDATE notlar SET durum = CASE WHEN (vize * 0.4 + final * 0.6) > 50 THEN 'Geçti'       ELSE 'Büte Kaldı' END;";
                    using (SqlCommand komut = new SqlCommand(hesaplamaSqlkodu, connect))
                    {
                        int etkilenenSatırSayisi = komut.ExecuteNonQuery();
                        if (etkilenenSatırSayisi > 0)
                        {
                            guncelle();
                            MessageBox.Show("Sonuçlar hesaplandı");
                            
                        }
                        else
                        {
                            guncelle();
                            MessageBox.Show("bir sorun oluştu");
                            
                        }

                    }
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("bir hata meydana geldi"+hata.Message,"Hata",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(databaseLink))
                {
                    connect.Open();
                    string hesaplamaSqlkodu = "UPDATE notlar SET durum = CASE WHEN (vize * 0.4 + büt * 0.6) > 50 THEN ' büt ile Geçti'       ELSE 'sınıf tekrarı' END;";
                    using (SqlCommand komut = new SqlCommand(hesaplamaSqlkodu, connect))
                    {
                        int etkilenenSatırSayisi = komut.ExecuteNonQuery();
                        if (etkilenenSatırSayisi > 0)
                        {
                            guncelle();
                            MessageBox.Show("Sonuçlar hesaplandı");

                        }
                        else
                        {
                            guncelle();
                            MessageBox.Show("bir sorun oluştu");

                        }

                    }
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("bir hata meydana geldi" + hata.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
